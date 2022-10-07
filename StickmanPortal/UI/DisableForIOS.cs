using System.Collections;
using System.Collections.Generic;
using UnityEngine.iOS;
using UnityEngine;
using UnityEngine.UI;

namespace StickmanPortal
{
    public class DisableForIOS : MonoBehaviour
    {
        [SerializeField] private List<GameObject> iapPanels = new List<GameObject>();

        private void Start()
        {
#if UNITY_IOS
            for (int i = 0; i < iapPanels.Count; i++)
            {
                iapPanels[i].SetActive(false);
            }
#endif
        }
    }
}
