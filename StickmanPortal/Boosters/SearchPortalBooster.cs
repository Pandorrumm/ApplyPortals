using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

namespace StickmanPortal
{
    public class SearchPortalBooster : MonoBehaviour
    {
        [SerializeField] private Button searchPortalButton = null;
        [SerializeField] private TMP_Text numberHintText = null;

        [Space]
        [SerializeField] private GameObject adsIcon = null;

        private int numberHint;
        private Hint hint;
        private bool isClick = true;

        private bool isAvailable;

        public static Action<bool> OpenNoAdsConnectionPanelEvent;

        private void OnEnable()
        {
            Tutorial.UpdateHintTextEvent += UpdateHintText;
        }

        private void OnDisable()
        {
            Tutorial.UpdateHintTextEvent -= UpdateHintText;
        }

        private void Start()
        {
            UpdateHintText();
            searchPortalButton.onClick.AddListener(ButtonHint);

            isAvailable = AdsManager.IsRewardedAvailable();
        }

        private void ButtonHint()
        {
            if (isClick)
            {
                if (numberHint > 0)
                {
                    adsIcon.SetActive(false);
                    ShowHint();
                }
                else
                {
                    if (isAvailable)
                    {
                        AdsManager.ShowRewarded(ShowHint);
                    }
                    else
                    {
                        AudioManager.PlayDefault(AudioManager.ACTION_NOT_POSSIBLE);
                        OpenNoAdsConnectionPanelEvent?.Invoke(true);
                    }
                }
            }
            else
            {
                AudioManager.PlayDefault(AudioManager.ACTION_NOT_POSSIBLE);
                return;
            }
        }

        private void ShowHint()
        {
            isClick = false;

            AudioManager.PlayDefault(AudioManager.TAP_BUTTON);

            hint = FindObjectOfType<Hint>();
            hint.EnableHints();

            numberHint--;

            if (numberHint <= 0)
            {
                adsIcon.SetActive(true);
                numberHint = 0;
            }

            numberHintText.text = "" + numberHint;

            PlayerPrefs.SetInt("NumberHint", numberHint);

            DOTween.Sequence()
                 .AppendInterval(2f)
                 .AppendCallback(() => isClick = true);
        }

        private void UpdateHintText()
        {
            numberHint = PlayerPrefs.GetInt("NumberHint");
            numberHintText.text = "" + numberHint;

            if (numberHint == 0)
            {
                adsIcon.SetActive(true);
            }
        }
    }
}
