using UnityEngine;
using DG.Tweening;

namespace StickmanPortal
{
    public class HintPortalAnimation : MonoBehaviour
    {
        private Sequence sequence;

        private void OnEnable()
        {
            sequence = DOTween.Sequence();

            sequence.Append(transform.DOScale(1.4f, 0.5f));
            sequence.Append(transform.DOScale(1f, 0.5f));
            sequence.Append(transform.DOScale(1.4f, 0.5f));
            sequence.Append(transform.DOScale(1f, 0.5f)).OnComplete(() => gameObject.SetActive(false));
        }
    }
}
