using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StickmanPortal
{
    public class LevelsUpdater : MonoBehaviour
    {
        [SerializeField] private bool isOpenAllLevels = false;

        [Header("Level On Icons")]
        [SerializeField] private Sprite killEnemyOn = null;
        [SerializeField] private Sprite getTreasureOn = null;
        [SerializeField] private Sprite savePrincessOn = null;

        [Header("Level Off Icons")]
        [SerializeField] private Sprite killEnemyOff = null;
        [SerializeField] private Sprite getTreasureOff = null;
        [SerializeField] private Sprite savePrincessOff = null;

        [Space]
        public List<Level> levels = new List<Level>();

        private int levelsUnlocked;

        private void Start()
        {
            if (isOpenAllLevels)
            {
                PlayerPrefs.SetInt("LevelsUnlocked", levels.Count);
            }

            levelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked");

            UpdateStatusLevel();
            UpdateLevelIcon();
        }    

        private void UpdateStatusLevel()
        { 
            for (int i = 0; i < levels.Count; i++)
            {
                levels[i].GetOpenLevel().SetActive(false);
                levels[i].GetClosedLevel().SetActive(true);

                levels[i].GetLevelNumberText().text = "" + (SearchIndexLevel(levels[i].keyLevel) + 1);
            }

            for (int i = 0; i < levelsUnlocked; i++)
            {
                levels[i].GetOpenLevel().SetActive(true);
                levels[i].GetClosedLevel().SetActive(false);
            }
        }

        public int SearchIndexLevel(string _key)
        {
            foreach (Level level in levels)
            {
                if (level.keyLevel == _key)
                {
                    return levels.IndexOf(level);
                }
            }

            return 0;
        }

        private void UpdateLevelIcon()
        {
            for (int i = 0; i < levels.Count; i++)
            {
                for (int y = 0; y < GameConfig.Instance.levelsDatas.Count; y++)
                {
                    if (levels[i].keyLevel == GameConfig.Instance.levelsDatas[y].data.keyLevel)
                    {
                        if (GameConfig.Instance.levelsDatas[y].data.missionType == LevelData.MissionType.KILL_ENEMY)
                        {
                            levels[i].GetLevelOnIcon().sprite = killEnemyOn;
                            levels[i].GetLevelOffIcon().sprite = killEnemyOff;
                        }
                        else if (GameConfig.Instance.levelsDatas[y].data.missionType == LevelData.MissionType.KILL_ENEMY_GET_TREASURE)
                        {
                            levels[i].GetLevelOnIcon().sprite = getTreasureOn;
                            levels[i].GetLevelOffIcon().sprite = getTreasureOff;
                        }
                        else if (GameConfig.Instance.levelsDatas[y].data.missionType == LevelData.MissionType.SAVE_PRINCESS)
                        {
                            levels[i].GetLevelOnIcon().sprite = savePrincessOn;
                            levels[i].GetLevelOffIcon().sprite = savePrincessOff;
                        }
                    }
                }
            }
        }
    }
}
