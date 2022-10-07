using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StickmanPortal
{
    [System.Serializable]
    public class ItemData
    {
        public enum EnTypeLock
        {
            FREE,
            AD,
            CURRENCY,
            LEVEL_COMPLETE
        }

        public Sprite icon;

        public EnTypeLock typeLock;
        public int price;
    }
}
