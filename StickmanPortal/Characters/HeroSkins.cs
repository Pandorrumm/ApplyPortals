using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StickmanPortal
{
    public class HeroSkins : MonoBehaviour
    {
        public string saveKey;
        [SerializeField] private List<GameObject> skins = new List<GameObject>();

        private void OnEnable()
        {
            ItemUIView.UpdateSkinEvent += AssignSkin;
        }

        private void OnDisable()
        {
            ItemUIView.UpdateSkinEvent -= AssignSkin;
        }

        private void Awake()
        {
            CheckSkin();
        }

        private void CheckSkin()
        {
            if (!PlayerPrefs.HasKey("Selected" + saveKey))
            {
                AssignSkin(saveKey, 0);
            }
            else
            {
                AssignSkin(saveKey, PlayerPrefs.GetInt("Selected" + saveKey));
            }
        }

        private void AssignSkin(string _key, int _index)
        {
            if (_key == saveKey)
            {
                EnableSkin(_index);
            }
        }

        private void EnableSkin(int _index)
        {
            for (int i = 0; i < skins.Count; i++)
            {
                skins[i].SetActive(false);
            }

            skins[_index].SetActive(true);
        }
    }
}
