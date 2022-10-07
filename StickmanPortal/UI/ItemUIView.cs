using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

namespace StickmanPortal
{
    [RequireComponent(typeof(Button))]
    public class ItemUIView : MonoBehaviour
    {
        [SerializeField] private Image background = null;
        [SerializeField] private Image icon = null;
        [SerializeField] private GameObject selectionIndicator = null;
        [SerializeField] private Button buttonBuy = null;

        [Header("Free")]
        [SerializeField] private Sprite freeBackground = null;

        [Header("Ad")]
        [SerializeField] private GameObject priceAdBackground = null;
        [SerializeField] private Sprite adBackground = null;

        [Header("Money")]
        [SerializeField] private GameObject priceMoneyBackground = null;
        [SerializeField] private Sprite moneyBackground = null;
        [SerializeField] private TMP_Text moneyView = null;

        [Header("Level Complete")]
        [SerializeField] private GameObject priceLevelCompleteBackground = null;
        [SerializeField] private Sprite levelCompleteBackground = null;
        [SerializeField] private TMP_Text numberLevel = null;
        

        private string saveKey;
        private int index;

        private ItemData.EnTypeLock currentLock;
        private int price;

        private bool lockBuy = false;

        private ScreenShop screenImprovements;

        private bool isAvailable;

        public static Action<int> SetIndexItemEvent;
        public static Action<string, int> UpdateSkinEvent;
        public static Action<int> DecreaseCurrencyEvent;
        public static Action<bool> OpenNoAdsConnectionPanelEvent;

        private void Awake()
        {
            buttonBuy.onClick.AddListener(ButtonBuy);

            screenImprovements = GetComponentInParent<ScreenShop>();
        }

        private void Start()
        {
            isAvailable = AdsManager.IsRewardedAvailable();
        }

        public void Init(string _saveKey, int _index)
        {
            saveKey = _saveKey;
            index = _index;
        }

        private void ButtonBuy()
        {
            if (lockBuy)
                return;

            if (currentLock == ItemData.EnTypeLock.AD)
            {
                if (isAvailable)
                {
                    AudioManager.PlayDefault(AudioManager.TAP_BUTTON);
                    AdsManager.ShowRewarded(LockItem);
                }
                else
                {
                    AudioManager.PlayDefault(AudioManager.ACTION_NOT_POSSIBLE);
                    OpenNoAdsConnectionPanelEvent?.Invoke(true);
                }
                   
                void LockItem()
                {
                    SetLock(ItemData.EnTypeLock.FREE);
                    PlayerPrefs.SetInt(saveKey + index, 1);
                }
            }
            else if (currentLock == ItemData.EnTypeLock.CURRENCY)
            {
                if (CurrencySystem.Instance.Check(price))
                {
                    AudioManager.Play("Purchase");

                    PlayerPrefs.SetInt(saveKey + index, 1);

                    //SetIndexItemEvent(index);

                    DecreaseCurrencyEvent?.Invoke(price);

                    SetLock(ItemData.EnTypeLock.FREE);
                }
                else
                {
                    AudioManager.PlayDefault(AudioManager.ACTION_NOT_POSSIBLE);
                    screenImprovements.OpenPurchases();
                }
            }
            else if (currentLock == ItemData.EnTypeLock.LEVEL_COMPLETE)
            {
                AudioManager.PlayDefault(AudioManager.ACTION_NOT_POSSIBLE);
            }
            else if (currentLock == ItemData.EnTypeLock.FREE)
            {
                AudioManager.Play("ChangeSkin");
                SetIndexItemEvent(index);
                UpdateSkinEvent(saveKey, index);

                PlayerPrefs.SetInt("Selected" + saveKey, index);
            }          
        }

        public void SetIcon(Sprite _icon)
        {
            icon.sprite = _icon;
        }

        public void SetSelect(bool _value)
        {
            selectionIndicator.SetActive(_value);
        }

        public void SetPrice(int _price)
        {
            price = _price;
        }

        public void SetLock(ItemData.EnTypeLock _typeLock)
        {
            if (_typeLock == ItemData.EnTypeLock.LEVEL_COMPLETE && PlayerPrefs.GetInt(saveKey + index) == 1)
                currentLock = ItemData.EnTypeLock.FREE;
            else if ((_typeLock == ItemData.EnTypeLock.AD || _typeLock == ItemData.EnTypeLock.CURRENCY) && PlayerPrefs.GetInt(saveKey + index) == 1)
                currentLock = ItemData.EnTypeLock.FREE;
            else
                currentLock = _typeLock;

           // buttonBuy.enabled = currentLock != ItemData.EnTypeLock.FREE;

            if (currentLock == ItemData.EnTypeLock.FREE)
                HidePrice();
            else if (currentLock == ItemData.EnTypeLock.AD)
                ShowPriceAd();
            else if (currentLock == ItemData.EnTypeLock.CURRENCY)
            {
                ShowPriceMoney(price);
            }
            else if (currentLock == ItemData.EnTypeLock.LEVEL_COMPLETE)
                ShowPriceLevelComplete();
        }

        public void SetNumberLevel(int _number)
        {
            numberLevel.text = "" + _number;
        }

        private void ShowPriceMoney(int _money)
        {
            HidePrice();

            background.sprite = moneyBackground;
            moneyView.text = _money.ToString();
            priceMoneyBackground.SetActive(true);
        }

        private void ShowPriceAd()
        {
            HidePrice();

            background.sprite = adBackground;
            priceAdBackground.SetActive(true);
        }

        private void ShowPriceLevelComplete()
        {
            HidePrice();

            background.sprite = levelCompleteBackground;
            priceLevelCompleteBackground.SetActive(true);
        }

        private void HidePrice()
        {
            background.sprite = freeBackground;

            priceAdBackground.SetActive(false);
            priceMoneyBackground.SetActive(false);
            priceLevelCompleteBackground.SetActive(false);
        }
    }
}
