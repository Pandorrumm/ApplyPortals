using UnityEngine;

namespace StickmanPortal
{
    [System.Serializable]
    public class LevelData 
    {
        public string keyLevel;

        [Space]
        public int numberPortals;
        public int numberEnemies;
        public int numberTreasure;

        [Space]
        public float playingTimeAfterStartBullet;

        public enum MissionType
        {
            KILL_ENEMY,
            KILL_ENEMY_GET_TREASURE,
            SAVE_PRINCESS
        }

        public enum PortalType
        {
            PERPENDICULAR_BULLET_DEPARTURE,
            THROUGH_BULLET_DEPARTURE          
        }

        [Space]
        public MissionType missionType;
        public PortalType portalType;
    }
}
