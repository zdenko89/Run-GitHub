﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.MilitaryHeroes.Scripts.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.HeroEditor.Common.CharacterScripts
{
    /// <summary>
    /// Firearm fire process.
    /// </summary>
    public class FirearmFire : MonoBehaviour
    {
        public Character Character;
        public FingerTrigger Finger;
        public Transform Slide;
        public Transform Forestock;
        public Transform ArmL;
        public Transform ArmR;
        public GameObject Shell;
        public ParticleSystem FireMuzzle;

        private bool _fire;
        private float _fireTime;
        private Dictionary<Transform, Vector3> _origins;

        /// <summary>
        /// Should be set outside (by input manager or AI).
        /// </summary>
        [HideInInspector] public bool FireButtonDown;
        [HideInInspector] public bool FireButtonPressed;
        [HideInInspector] public bool FireButtonUp;

        public void Start()
        {
            _origins = new Dictionary<Transform, Vector3> { { ArmR, ArmR.localPosition } };
        }

        public void Update()
        {
            if (Character.Firearm.Params.AutomaticFire ? FireButtonPressed : FireButtonDown)
            {
                StartCoroutine(Fire());
            }
            else if (FireButtonUp)
            {
                Finger.Pressed = AngryFace = false;
            }
        }

        public void CreateShell()
        {
            Instantiate(Shell, Shell.transform.position, Shell.transform.rotation, Shell.transform.parent).SetActive(true);
        }

        private IEnumerator Fire()
        {
            if (_fire || Time.time - _fireTime < 60f / Character.Firearm.Params.FireRateInMinute || Character.Firearm.Reload.Reloading) yield break;

            if (Character.Firearm.AmmoShooted == Character.Firearm.Params.MagazineCapacity)
            {
                Finger.Pressed = AngryFace = false;
                Slide.localPosition = Vector3.zero;
                yield return StartCoroutine(Character.Firearm.Reload.Reload());
                Finger.Pressed = AngryFace = FireButtonPressed;
                yield break; 
            }

            if (FireButtonDown)
            {
                Finger.Pressed = AngryFace = true;
            }

            _fire = true;
            _fireTime = Time.time;

            Character.Firearm.AmmoShooted++;
            CreateBullet();
            FireMuzzlePlay();
            GetComponent<AudioSource>().PlayOneShot(Character.Firearm.Params.SoundFire, 0.5f);
            
            var offset = -Character.Firearm.Params.Recoil * ArmR.parent.InverseTransformDirection(Character.Firearm.FireTransform.right);

            StartCoroutine(AnimateOffset(ArmR, offset, _origins[ArmR], spring: true));
            
            if (!Character.Firearm.Params.AutomaticLoad)
            {
                yield return new WaitForSeconds(0.5f);
            }

            if (Character.Firearm.Params.Type == FirearmType.Revolver)
            {
                StartCoroutine(RotateRevolverDrum());
            }
            else
            {
                StartCoroutine(ShellOut(Time.time));
            }

            if (!Character.Firearm.Params.AutomaticLoad)
            {
                Character.Animator.CrossFade(Character.Firearm.Params.LoadAnimation.name, 0.1f, 0);
            }
        }

        private void FireMuzzlePlay()
        {
            if (FireMuzzle != null && FireMuzzle.name != Character.Firearm.Params.FireMuzzlePrefab.name)
            {
                Destroy(FireMuzzle.gameObject);
                FireMuzzle = null;
            }

            if (FireMuzzle == null)
            {
                FireMuzzle = Instantiate(Character.Firearm.Params.FireMuzzlePrefab, Character.Firearm.FireTransform);
                FireMuzzle.name = Character.Firearm.Params.FireMuzzlePrefab.name;
                FireMuzzle.transform.localPosition = Vector3.zero;
                FireMuzzle.transform.localRotation = Quaternion.Euler(0, -90, 0);

                var rifleSortingOrder = Character.FirearmsRenderers.Single(i => i.name == "Rifle").sortingOrder;

                foreach (var fx in Character.Firearm.FireTransform.GetComponentsInChildren<Renderer>())
                {
                    fx.sortingOrder = rifleSortingOrder - 1;
                }
            }

            FireMuzzle.Play(true);
        }

        private IEnumerator ShellOut(float startTime)
        {
            CreateShell();

            var state = 0f;
            var duration = Character.Firearm.Params.AutomaticLoad ? 60f / Mathf.Max(600, Character.Firearm.Params.FireRateInMinute) : Character.Firearm.Params.LoadAnimation.length;

            while (state < 1)
            {
                state = (Time.time - startTime) / duration;

                if (state <= 1)
                {
                    Slide.localPosition = Vector3.zero - 0.2f * new Vector3(Mathf.Sin(state * Mathf.PI), 0);
                    yield return null;
                }
                else
                {
                    _fire = false;

                    if (Character.Firearm.Params.AutomaticFire && FireButtonPressed) break;

                    Slide.localPosition = Vector3.zero;

                    break;
                }
            }
        }

        private void CreateBullet() // TODO: Preload and caching prefabs is recommended to improve game performance
        {
            if (SceneManager.GetActiveScene().name.Contains("CharacterEditor")) return; // Don't create bullets in editor scene

            var iterations = Character.Firearm.Params.Type == FirearmType.Shotgun ? int.Parse(Character.Firearm.Params.Meta) : 1;

            for (var i = 0; i < iterations; i++)
            {
                var bullet = Instantiate(Character.Firearm.Params.ProjectilePrefab, Character.Firearm.FireTransform);
                var spread = Character.Firearm.FireTransform.up * Random.Range(-1f, 1f) * (1 - Character.Firearm.Params.Accuracy);

                bullet.transform.localPosition = Vector3.zero;
                bullet.transform.localRotation = Quaternion.identity;
                bullet.transform.SetParent(null);
                bullet.GetComponent<SpriteRenderer>().sprite = Character.Firearms.Single(j => j.name == "Bullet");
                bullet.GetComponent<Rigidbody>().velocity = Character.Firearm.Params.MuzzleVelocity * (Character.Firearm.FireTransform.right + spread)
                    * Mathf.Sign(Character.transform.lossyScale.x) * Random.Range(0.85f, 1.15f);

                var sortingOrder = Character.FirearmsRenderers.Single(j => j.name == "Rifle").sortingOrder;

                foreach (var r in bullet.Renderers)
                {
                    r.sortingOrder = sortingOrder;
                }

                var ignoreCollider = Character.GetComponent<Collider>();

                if (ignoreCollider != null)
                {
                    Physics.IgnoreCollision(bullet.GetComponent<Collider>(), ignoreCollider);
                }

                bullet.gameObject.layer = 31; // TODO: Create layer in your project and disable collision for it (in psysics settings)
                Physics.IgnoreLayerCollision(31, 31, true);
            }
        }

        private IEnumerator RotateRevolverDrum()
        {
            var duration = 60f / Mathf.Max(600, Character.Firearm.Params.FireRateInMinute);

            Slide.GetComponent<SpriteRenderer>().sprite = Character.Firearms[7];

            yield return new WaitForSeconds(duration / 2);

            Slide.GetComponent<SpriteRenderer>().sprite = Character.Firearms[6];

            _fire = false;
        }

        private bool AngryFace
        {
            set { Character.Animator.SetBool("Angry", value); }
        }

        private static IEnumerator AnimateOffset(Transform target, Vector3 offset, Vector3 origin, bool spring = false, float duration = 0.05f)
        {
            var state = 0f;
            var startTime = Time.time;

            while (state < 1)
            {
                state = (Time.time - startTime) / duration;

                if (state <= 1)
                {
                    target.localPosition = origin + offset * (spring ? Mathf.Sin(state * Mathf.PI) : state);
                    yield return null;
                }
                else
                {
                    target.localPosition = spring ? origin : origin + offset;
                    break;
                }
            }
        }
    }
}