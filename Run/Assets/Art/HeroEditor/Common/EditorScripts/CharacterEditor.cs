using System;
using System.Linq;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.Common.Examples;
using HeroEditor.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.HeroEditor.Common.EditorScripts
{
    /// <summary>
    /// Character editor UI and behaviour.
    /// </summary>
    public class CharacterEditor : CharacterEditorBase
    {
        [Header("Other")]
        public FirearmCollection FirearmCollection;
        public bool UseEditorColorField = true;
        public string PrefabFolder;
        public string TestRoomSceneName;

        private static CharacterBase _temp;

        /// <summary>
        /// Called automatically on app start.
        /// </summary>
        public void Awake()
        {
            RestoreTempCharacter();
        }

        #if UNITY_EDITOR

        /// <summary>
        /// Save character to prefab.
        /// </summary>
        public void Save()
        {
            PrefabFolder = UnityEditor.EditorUtility.SaveFilePanel("Save character prefab", PrefabFolder, "New character", "prefab");

            if (PrefabFolder.Length > 0)
            {
                Save("Assets" + PrefabFolder.Replace(Application.dataPath, null));
            }
        }

        /// <summary>
        /// Load character from prefab.
        /// </summary>
        public void Load()
        {
            Debug.LogWarning("This feature is available only in PRO asset version: http://u3d.as/14qb");
            Debug.LogWarning("Please upgrade the asset and get our special bonus: https://goo.gl/YTcEsa");
        }

        public override void Save(string path)
        {
            Character.transform.localScale = Vector3.one;
            UnityEditor.PrefabUtility.CreatePrefab(path, Character.gameObject);
            Debug.LogFormat("Prefab saved as {0}", path);
        }

        public override void Load(string path)
        {
            Load(UnityEditor.AssetDatabase.LoadAssetAtPath<CharacterBase>(path));
        }

        #else

        public override void Save(string path)
        {
            throw new NotSupportedException();
        }

        public override void Load(string path)
        {
            throw new NotSupportedException();
        }

        #endif

        /// <summary>
        /// Test character with demo setup.
        /// </summary>
        public void Test()
        {
			Debug.LogWarning("This feature is available only in PRO asset version: http://u3d.as/14qb");
	        Debug.LogWarning("Please upgrade the asset and get our special bonus: https://goo.gl/YTcEsa");
		}

        protected override void BuildFirearms(SpriteGroupEntry entry)
        {
            if (entry == null) return;

            if (FirearmCollection.Firearms.Count(i => i.Name == entry.Name) != 1) throw new Exception("Please check firearms params for: " + entry.Name);

            var f = FirearmCollection.Firearms.Single(i => i.Name == entry.Name);
            var c = (Character) Character;

            c.Firearm.Params = f; // TODO:
            c.Firearm.SlideTransform.localPosition = f.SlidePosition;
            c.Firearm.MagazineTransform.localPosition = f.MagazinePosition;
            c.Firearm.FireTransform.localPosition = f.FireMuzzlePosition;
            c.Firearm.AmmoShooted = 0;
        }

        protected override void OpenPalette(GameObject palette, Color selected)
        {
            #if UNITY_EDITOR

            if (UseEditorColorField)
            {
                EditorColorField.Open(selected);
            }
            else

            #endif

            {
                Editor.SetActive(false);
                palette.SetActive(true);
            }
        }

        private void RestoreTempCharacter()
        {
            if (_temp == null) return;

            Load(_temp);
            Destroy(_temp.gameObject);
            _temp = null;
        }
    }
}