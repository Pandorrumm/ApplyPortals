using UnityEngine;

namespace StickmanPortal
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "GameData/LevelData")]
    public class LevelConfig : ScriptableObject
    {
        public LevelData data;
    }
}
