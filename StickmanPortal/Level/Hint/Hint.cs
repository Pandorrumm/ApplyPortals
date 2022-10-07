using UnityEngine;

namespace StickmanPortal
{
    public class Hint : MonoBehaviour
    {     
        public void EnableHints()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
