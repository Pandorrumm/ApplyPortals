using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StickmanPortal
{
    public class ItemManager : MonoBehaviour
    {
        [Space]
        [SerializeField] private ItemUIView itemUIPrefab = null;
        [SerializeField] private Transform itemsParent = null;

        [SerializeField] private string saveKey = null;

        private List<int> levelsWithPrincess = new List<int>();

        private List<ItemData> allItems;
        private List<ItemUIView> uiViews = new List<ItemUIView>();

        private void OnEnable()
        {
            ItemUIView.SetIndexItemEvent += GetIndexTakenItem;
        }

        private void OnDisable()
        {
            ItemUIView.SetIndexItemEvent -= GetIndexTakenItem;
        }

        public ItemData GetItem(int _index)
        {
            return allItems[_index];
        }

        public void Init(List<ItemData> _items, string _key)
        {
            if (_key == ItemsDataBase.Instance.itemBackgroundMenuData.saveKey)
            {
                GetIndexLevelWithPrincess();
            }
            
            allItems = _items;

            for (int i = 0; i < uiViews.Count; i++)
            {
                Destroy(uiViews[i].gameObject);
            }
            uiViews.Clear();

            for (int i = 0; i < allItems.Count; i++)
            {
                int index = i;
                ItemUIView item = Instantiate(itemUIPrefab.gameObject, itemsParent).GetComponent<ItemUIView>();
                item.Init(_key, index);
                item.SetIcon(allItems[index].icon);
                item.SetPrice(allItems[index].price);
                item.SetLock(allItems[index].typeLock);
                uiViews.Add(item);

                if (_key == ItemsDataBase.Instance.itemBackgroundMenuData.saveKey)
                {
                    item.SetNumberLevel(levelsWithPrincess[index]);
                }
            }         

            GetIndexTakenItem(PlayerPrefs.GetInt("Selected" + saveKey));
        }

        private void GetIndexLevelWithPrincess()
        {
            for (int i = 0; i < GameConfig.Instance.levelsDatas.Count; i++)
            {
                if (GameConfig.Instance.levelsDatas[i].data.missionType == LevelData.MissionType.SAVE_PRINCESS)
                {
                    levelsWithPrincess.Add(i + 1);
                }
            }
        }

        private void GetIndexTakenItem(int _index)
        {
            for (int i = 0; i < uiViews.Count; i++)
            {
                uiViews[i].SetSelect(false);
            }

            uiViews[_index].SetSelect(true);
        }
    }
}
