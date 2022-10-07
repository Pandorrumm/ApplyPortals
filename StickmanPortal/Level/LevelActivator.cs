using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System;

namespace StickmanPortal
{
    public class LevelActivator : MonoBehaviour
    {
        [SerializeField] private List<AssetReference> levelsPrefabs = new List<AssetReference>();

        UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> level;

        public static Action DisapperingScreenEvent;

        private void OnEnable()
        {
            ScreenVictory.ReleaseLevelEvent += ReleaseLevel;
            ScreenLose.ReleaseLevelEvent += ReleaseLevel;
            GameUI.ReleaseLevelEvent += ReleaseLevel;
            ScreenEternalLiveForAd.ReleaseLevelEvent += ReleaseLevel;
        }

        private void OnDisable()
        {
            ScreenVictory.ReleaseLevelEvent -= ReleaseLevel;
            ScreenLose.ReleaseLevelEvent -= ReleaseLevel;
            GameUI.ReleaseLevelEvent -= ReleaseLevel;
            ScreenEternalLiveForAd.ReleaseLevelEvent -= ReleaseLevel;
        }

        private void Awake()
        {
            level = Addressables.InstantiateAsync(levelsPrefabs[GameConfig.Instance.currentMissionIndex].RuntimeKey);

            level.Completed += (operation) =>
            {
                DisapperingScreenEvent?.Invoke();
            };
        }

        private void ReleaseLevel()
        {            
            if (level.IsValid())
            {
                Addressables.Release(level);
            }           
        }
    }
}
