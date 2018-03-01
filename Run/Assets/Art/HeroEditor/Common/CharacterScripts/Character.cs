﻿using System;
using System.Collections.Generic;
using System.Linq;
using HeroEditor.Common;
using HeroEditor.Common.Enums;
using UnityEngine;

namespace Assets.HeroEditor.Common.CharacterScripts
{
    /// <summary>
    /// Character presentation in editor. Contains sprites, renderers, animation and so on.
    /// </summary>
    public class Character : CharacterBase
    {
        [Header("Weapons")]
        public MeleeWeapon MeleeWeapon;
        public Firearm Firearm;
        public BowShooting BowShooting;

        /// <summary>
        /// Called automatically when something was changed.
        /// </summary>
        public void OnValidate()
        {
            if (Head == null) return;

            Initialize();
        }
        
        /// <summary>
        /// Called automatically when object was started.
        /// </summary>
        public void Start()
        {
            HairMask.isCustomRangeActive = true;
            HairMask.frontSortingOrder = HairRenderer.sortingOrder;
            HairMask.backSortingOrder = HairRenderer.sortingOrder - 1;
            UpdateAnimationParams();
        }

        public override void UpdateAnimationParams()
        {
            Animator.SetInteger("WeaponType", (int) WeaponType);
            Animator.SetInteger("MagazineType", (int) Firearm.Params.MagazineType);
            Animator.SetInteger("HoldType", (int) Firearm.Params.HoldType);
        }

        public override int GetAnimationState()
        {
            return 100 * (int) WeaponType + (int) Firearm.Params.HoldType;
        }

        public override void ResetAnimation()
        {
            Animator.SetBool("Ready", true);
            Animator.SetBool("Stand", true);
            Animator.Play(WeaponType == WeaponType.Firearms1H || WeaponType == WeaponType.Firearms2H ? "IdleFirearm" : "IdleMelee", 0); // Upper body
            Animator.Play("Stand", 1); // Lower body
        }

        /// <summary>
        /// Initializes character renderers with selected sprites.
        /// </summary>
        public override void Initialize()
        {
            try
            {
                TryInitialize();
            }
            catch (Exception e)
            {
                Debug.LogWarningFormat("Unable to initialize character {0}: {1}", name, e.Message);
            }
        }

        /// <summary>
        /// Initializes character renderers with selected sprites.
        /// </summary>
        private void TryInitialize()
        {
            HeadRenderer.sprite = Head;
            HairRenderer.sprite = Hair;
            HairRenderer.maskInteraction = HelmetMask == null ? SpriteMaskInteraction.None : SpriteMaskInteraction.VisibleOutsideMask;
            HairMask.sprite = HelmetMask;
            EarsRenderer.sprite = Ears;
            EyebrowsRenderers[0].sprite = Eyebrows;
            EyesRenderers[0].sprite = Eyes;
            MouthRenderers[0].sprite = Mouth;
            BeardRenderer.sprite = Beard;
            EarsRenderer.sprite = Ears;
            MapSprites(BodyRenderers, Body);
            HelmetRenderer.sprite = Helmet;
            GlassesRenderer.sprite = Glasses;
            MaskRenderer.sprite = Mask;
            MapSprites(ArmorRenderers, Armor);
            CapeRenderer.sprite = Cape;
            BackRenderer.sprite = Back;
            PrimaryMeleeWeaponRenderer.sprite = PrimaryMeleeWeapon;
            SecondaryMeleeWeaponRenderer.sprite = SecondaryMeleeWeapon;
            BowRenderers.ForEach(i => i.sprite = Bow.SingleOrDefault(j => j != null && i.name.Contains(j.name)));
            FirearmsRenderers.ForEach(i => i.sprite = Firearms.SingleOrDefault(j => j != null && i.name.Contains(j.name)));
            ShieldRenderer.sprite = Shield;

            PrimaryMeleeWeaponRenderer.enabled = WeaponType != WeaponType.Bow;
            SecondaryMeleeWeaponRenderer.enabled = WeaponType == WeaponType.MeleePaired;
            BowRenderers.ForEach(i => i.enabled = WeaponType == WeaponType.Bow);
            
            switch (HairMaskType)
            {
                case HairMaskType.None:
                    HairRenderer.maskInteraction = SpriteMaskInteraction.None;
                    HairMask.sprite = null;
                    break;
                case HairMaskType.HelmetMask:
                    HairRenderer.maskInteraction = HelmetMask == null ? SpriteMaskInteraction.None : SpriteMaskInteraction.VisibleOutsideMask;
                    HairMask.sprite = HelmetMask;
                    break;
                case HairMaskType.HeadMask:
                    HairRenderer.maskInteraction = Head == null ? SpriteMaskInteraction.None : SpriteMaskInteraction.VisibleInsideMask;
                    HairMask.sprite = HeadMask;
                    break;
            }

            if (Hair != null && Hair.name.Contains("[HideEars]"))
            {
                EarsRenderer.sprite = null;
            }

            if (Hair != null && Hair.name.Contains("[HideHelm]"))
            {
                HairRenderer.maskInteraction = SpriteMaskInteraction.None;
                HelmetRenderer.sprite = null;
            }

            switch (WeaponType)
            {
                case WeaponType.Firearms1H:
                case WeaponType.Firearms2H:
                    Firearm.AmmoShooted = 0;
                    break;
            }
        }

        private void MapSprites(List<SpriteRenderer> spriteRenderers, List<Sprite> sprites)
        {
            foreach (var part in spriteRenderers)
            {
                part.sprite = sprites.SingleOrDefault(i => i != null && i.name == part.name.Split('[')[0]);
            }
        }
    }
}