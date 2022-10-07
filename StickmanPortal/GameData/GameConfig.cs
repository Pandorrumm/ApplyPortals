using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StickmanPortal
{
    public class GameConfig : Singleton<GameConfig>
    {     
        public LevelData.MissionType missionType;
        public LevelData.PortalType portalType;
        public int currentMissionIndex = -1;

        [HideInInspector]
        public string globalMissionName = "";
        
        public List<LevelConfig> levelsDatas = new List<LevelConfig>();

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

        private void Start()
        {
            LoadingScreen.Instance.ChangeAppearance();
        }

        public void ChangeLevel()
        {
            currentMissionIndex++;

            if (currentMissionIndex < levelsDatas.Count)
            {
                globalMissionName = levelsDatas[currentMissionIndex].data.keyLevel;
                missionType = levelsDatas[currentMissionIndex].data.missionType;
                portalType = levelsDatas[currentMissionIndex].data.portalType;

                LoadingScreen.Instance.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                LoadingScreen.Instance.LoadScene("MainMenu");
            }    
        }

        public LevelData.PortalType GetPortalType()
        {
            return levelsDatas[currentMissionIndex].data.portalType;
        }

        public LevelData.MissionType GetMissionType()
        {
            return levelsDatas[currentMissionIndex].data.missionType;
        }

        public int GetNumberEnemies()
        {
            return levelsDatas[currentMissionIndex].data.numberEnemies;
        }

        public string GetKeyLevel()
        {
            return levelsDatas[currentMissionIndex].data.keyLevel;
        }

        public int GetNumberPortals()
        {
            return levelsDatas[currentMissionIndex].data.numberPortals;
        }   
        
        public float GetTimeToLoseLevel()
        {
            return levelsDatas[currentMissionIndex].data.playingTimeAfterStartBullet;
        }

        public void AssignLevelConfig(string _key, int _levelIndex)
        {
            globalMissionName = _key;
            currentMissionIndex = _levelIndex;
            missionType = levelsDatas[_levelIndex].data.missionType;
            portalType = levelsDatas[_levelIndex].data.portalType;
        }
    }
}
