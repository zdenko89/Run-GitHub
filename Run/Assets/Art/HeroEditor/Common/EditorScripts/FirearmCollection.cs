using System.Collections.Generic;
using System.Linq;
using Assets.HeroEditor.Common.Data;
using HeroEditor.Common;
using UnityEngine;

namespace Assets.HeroEditor.Common.EditorScripts
{
    /// <summary>
    /// Represents all firearms and params, instance is always located on the main scene.
    /// </summary>
    public class FirearmCollection : MonoBehaviour
    {
        public List<FirearmParams> Firearms;

        public static FirearmCollection Instance;

        public void Awake()
        {
            Instance = this;
        }

        public void OnValidate()
        {
            //Firearms = Firearms.OrderBy(i => i.Name).ToList();

            var spriteCollection = FindObjectOfType<SpriteCollection>();

            if (spriteCollection == null) return;

            var sprites = spriteCollection.Firearms1H.Union(spriteCollection.Firearms2H).ToList();

            foreach (var entry in sprites)
            {
                if (Firearms.All(i => i.Name != entry.Name))
                {
                    Debug.LogWarningFormat("Firearm params missed for: {0}", entry.Name);
                }
            }

            foreach (var p in Firearms)
            {
                if (sprites.All(i => i.Name != p.Name))
                {
                    Debug.LogWarningFormat("Excess params found: {0}", p.Name);
                }
            }
        }
    }
}