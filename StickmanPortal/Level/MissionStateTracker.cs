using System.Collections;
using UnityEngine;
using System;

namespace StickmanPortal
{
    public class MissionStateTracker : MonoBehaviour
    {
        [Space]
        [SerializeField] private VictoryLose victoryLose = null;

        public float timeToLoseLevel;
        public float gameTimer;

        public int numberEnemiesKilled;
        private int numberTreasuresCollected;

        private IEnumerator timerToLose;

        public static Action PlayWinAnimationHeroEvent;

        private void OnEnable()
        {
            Enemy.KillEnemyEvent += CountingEnemiesKilled;
            Treasure.TakeTreasureEvent += CountingTakeTrasure;
            Enemy.StartTimerToLoseEvent += StartTimerToLose;
            Bullet.StartTimerToLoseEvent += StartTimerToLose;
            Treasure.StartTimerToLoseEvent += StartTimerToLose;
            VictoryLose.StopTimerToLoseEvent += StopTimerToLose;
        }

        private void OnDisable()
        {
            Enemy.KillEnemyEvent -= CountingEnemiesKilled;
            Treasure.TakeTreasureEvent -= CountingTakeTrasure;
            Enemy.StartTimerToLoseEvent -= StartTimerToLose;
            Bullet.StartTimerToLoseEvent -= StartTimerToLose;
            Treasure.StartTimerToLoseEvent -= StartTimerToLose;
            VictoryLose.StopTimerToLoseEvent -= StopTimerToLose;
        }

        private void Awake()
        {
            timeToLoseLevel = GameConfig.Instance.GetTimeToLoseLevel();
        }

        private void Start()
        {
            timerToLose = TimerToLose();
        }

        private void TrackMissionState()
        {
            int correctConditionsForMission = 0;

            switch (GameConfig.Instance.missionType)
            {
                case LevelData.MissionType.KILL_ENEMY:
                    CheckKillingEnemy(GameConfig.Instance.levelsDatas[GameConfig.Instance.currentMissionIndex].data, ref correctConditionsForMission);
                    break;
                case LevelData.MissionType.SAVE_PRINCESS:
                    CheckKillingEnemy(GameConfig.Instance.levelsDatas[GameConfig.Instance.currentMissionIndex].data, ref correctConditionsForMission);
                    break;
                case LevelData.MissionType.KILL_ENEMY_GET_TREASURE:
                    CheckKillingEnemyGetTreasure(GameConfig.Instance.levelsDatas[GameConfig.Instance.currentMissionIndex].data, ref correctConditionsForMission);
                    break;
            }

            if (correctConditionsForMission == 1)
            {
                PlayWinAnimationHeroEvent?.Invoke();
                gameTimer = -10;
                StopTimerToLose();
            }
        }

        private void CountingEnemiesKilled()
        {
            numberEnemiesKilled++;
            TrackMissionState();
        }

        private void CountingTakeTrasure()
        {
            numberTreasuresCollected++;
            TrackMissionState();
        }

        private void CheckKillingEnemy(LevelData data, ref int correctConditionsForMission)
        {
            if (numberEnemiesKilled >= data.numberEnemies)
            {
                correctConditionsForMission++;
            }
        }

        private void CheckKillingEnemyGetTreasure(LevelData data, ref int correctConditionsForMission)
        {
            if (numberTreasuresCollected == data.numberTreasure && numberEnemiesKilled == data.numberEnemies)
            {
                correctConditionsForMission++;
            }
        }

        private void StartTimerToLose()
        {
            StartCoroutine(timerToLose);
        }

        private IEnumerator TimerToLose()
        {
            gameTimer = 0;

            while (gameTimer <= timeToLoseLevel)
            {
                gameTimer += Time.deltaTime;

                if (gameTimer >= timeToLoseLevel && !victoryLose.isVictory && !victoryLose.isLose)
                {
                    victoryLose.OpenLosePanel();

                    AudioManager.Play("Lose");
                }

                yield return null;
            }
        }

        private void StopTimerToLose()
        {
            StopCoroutine(timerToLose);
        }
    }
}
