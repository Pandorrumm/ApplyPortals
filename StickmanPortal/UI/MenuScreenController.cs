using System.Collections.Generic;
using UnityEngine;

namespace StickmanPortal
{
    public class MenuScreenController : MonoBehaviour
    {
        public static MenuScreenController instance;

        [SerializeField] private GameObject backgroundOverlay = null;

        [System.Serializable]
        public class ScreenData
        {
            public string key;
            public GameObject screenPanel;
            public bool isActiveOverlay;
        }

        public List<ScreenData> screensData = new List<ScreenData>();

        [Header("Open Roulette Screen")]
        [SerializeField] private string keyRouletteScreen = null;

        [Header("Open GameMenu Screen")]
        [SerializeField] private string keyGameMenuScreen = null;

        private void Awake()
        {
            instance = this;

            backgroundOverlay.SetActive(false);

            if (PlayerPrefs.GetInt("ShowRouletteScreen") == 1)
            {
                OpenScreen(keyRouletteScreen);
            }
            else
            {
                OpenScreen(keyGameMenuScreen);
            }
        }

        public void OpenScreen(string _key)
        {
            foreach (ScreenData screenData in screensData)
            {
                screenData.screenPanel.SetActive(false);

                if (screenData.key == _key)
                {
                    if (screenData.isActiveOverlay)
                    {
                        backgroundOverlay.SetActive(true);
                    }
                    else
                    {
                        backgroundOverlay.SetActive(false);
                    }

                    screenData.screenPanel.SetActive(true);
                }
            }
        }
    
        public void CloseAllScreen()
        {
            foreach (ScreenData screenData in screensData)
            {
                screenData.screenPanel.SetActive(false);
            }
        }
    }
}
