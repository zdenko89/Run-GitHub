using System;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.MilitaryHeroes.Scripts.Enums;
using UnityEngine;

namespace Assets.HeroEditor.Common.Data
{
    /// <summary>
    /// Firearm parameters. Full list is stored in FirearmCollection instance.
    /// </summary>
    [Serializable]
    public class FirearmParams
    {
        public string Name;
        public FirearmType Type;
        public HoldType HoldType;
        public MagazineType MagazineType;
        public bool AutomaticFire;
        public bool AutomaticLoad;
        public int FireRateInMinute;
        public int MagazineCapacity;

        /// <summary>
        /// Arm recoil (offset in local space)
        /// </summary>
        [Range(0, 0.25f)] public float Recoil = 0.05f;

        /// <summary>
        /// 0 = max spreading angle (45 degree), 1 = 100% accuracy (zero spreading).
        /// </summary>
        [Range(0, 1)] public float Accuracy = 0.95f;

        /// <summary>
        /// Muzzle velocity in m/s.
        /// </summary>
        [Range(500, 5000)]
        public float MuzzleVelocity = 1500f;

        [Header("Positions")]
        public Vector2 SlidePosition;
        public Vector2 MagazinePosition;
        public Vector2 FireMuzzlePosition;

        [Header("Components")]
        public ParticleSystem FireMuzzlePrefab;
        public Projectile ProjectilePrefab;

        [Header("Sounds")]
        public AudioClip SoundFire;
        public AudioClip SoundClipIn;
        public AudioClip SoundClipOut;
        public AudioClip SoundLoad;
        public AudioClip SoundPump;
        
        [Header("Animation")]
        public AnimationClip HoldAnimation;
        public AnimationClip LoadAnimation;
        public AnimationClip ReloadAnimation;

        /// <summary>
        /// Store specific weapon params here.
        /// </summary>
        [Header("Meta")]
        public string Meta;
    }
}