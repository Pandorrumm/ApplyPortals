using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StickmanPortal
{
    public class ItemsDataBase : Singleton<ItemsDataBase>
    {
        [System.Serializable]
        public class ItemCharacterData
        {
            public string saveKey; 
            public List<ItemData> character = new List<ItemData>();
        }        

        [System.Serializable]
        public class ItemPortalData
        {
            public string saveKey;
            public List<ItemData> portal = new List<ItemData>();
        }

        [System.Serializable]
        public class ItemBloodData
        {
            public string saveKey;
            public List<ItemData> blood = new List<ItemData>();
        }

        [System.Serializable]
        public class ItemPillowData
        {
            public string saveKey;
            public List<ItemData> pillow = new List<ItemData>();
        }

        [System.Serializable]
        public class ItemBackgroundMenuData
        {
            public string saveKey;
            public List<ItemData> backgroundMenu = new List<ItemData>();
        }

        public ItemCharacterData itemCharacterData;
        public ItemPortalData itemPortalData;
        public ItemBloodData itemBloodData;
        public ItemPillowData itemPillowData;
        public ItemBackgroundMenuData itemBackgroundMenuData;

        private Sprite icon;

        private void Awake()
        {
            if (HasInstance)
            {
                Destroy(gameObject);
            }
            else
            {
                InitInstance();
                DontDestroyOnLoad(gameObject);
            }
        }

        public void GetRandomSkin()
        {
            int index = UnityEngine.Random.Range(0, 4);

            Debug.Log(index);

            switch (index)
            {
                case 0:
                    GetItem(itemCharacterData.character, itemCharacterData.saveKey);
                    break;
                case 1:
                    GetItem(itemPortalData.portal, itemPortalData.saveKey);                  
                    break;
                case 2:
                    GetItem(itemBloodData.blood, itemBloodData.saveKey);
                    break;
                case 3:
                    GetItem(itemPillowData.pillow, itemPillowData.saveKey);
                    break;
                //case 4:
                //    GetItem(itemBackgroundMenuData.backgroundMenu, itemBackgroundMenuData.saveKey);
                //    break;
            }
        }

        private void GetItem(List<ItemData> _itemData, string _saveKey)
        {
            for (int i = 1; i < _itemData.Count; i++)
            {
                if (!PlayerPrefs.HasKey(_saveKey + i))
                {
                    PlayerPrefs.SetInt((_saveKey + i), 1);

                    icon = _itemData[i].icon;

                    return;
                }           
            }
        }

        public void GetThingSkin()
        {
            for (int i = 0; i < itemBackgroundMenuData.backgroundMenu.Count; i++)
            {
                if (!PlayerPrefs.HasKey(itemBackgroundMenuData.saveKey + i))
                {
                    PlayerPrefs.SetInt((itemBackgroundMenuData.saveKey + i), 1);

                    icon = itemBackgroundMenuData.backgroundMenu[i].icon;

                    return;
                }
            }
        }

        public Sprite GetIcon()
        {
            return icon;
        }
    }
}
