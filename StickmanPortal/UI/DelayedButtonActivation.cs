using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace StickmanPortal
{
    public class DelayedButtonActivation : MonoBehaviour
    {
        [SerializeField] private float delayActivation = 0.6f;

        private Button activeButton;

        private void OnEnable()
        {
            activeButton = GetComponent<Button>();

            DelayActivation();
        }

        private void DelayActivation()
        {
            activeButton.interactable = false;

            DOTween.Sequence()
                     .AppendInterval(delayActivation)
                     .AppendCallback(() => activeButton.interactable = true);
        }
    }
}
