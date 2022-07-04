using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : ControllerHelper
{
    #region MVC
    [Header("MVC")]
    [SerializeField]
    private ShopItemView _shopItemViewPrefab;

    [SerializeField]
    private ShopView _shopView;

    [SerializeField]
    private ShopOfferingsModel_SO _shopModel;
    #endregion // MVC

    #region Inspector Fields
    [Header("UI")]
    [SerializeField]
    private Transform _shopItemsContainer;

    [Space]
    [SerializeField]
    private string REASON_FULL = "Full";
    [SerializeField]
    private string REASON_LOW_FUNDS = "Low funds";
    [SerializeField]
    private string REASON_DONE_AD = "Claimed";

    [SerializeField]
    private string BUY_TEXT = "Buy";
    [SerializeField]
    private string BUY_AD_TEXT = "Watch";
    #endregion // Inspector Fields

    #region Private Fields
    private ItemPurpose _cachedPurpose;
    private float _cachedValue;
    #endregion

    #region View Dictionary
    Dictionary<ItemPurpose, ShopItemView> _dictShopItem
        = new Dictionary<ItemPurpose, ShopItemView>();
    #endregion // View Dictionary

    #region Unity Callbacks
    private void Start()
    {
        ClearContainer();
        BuildShopItems();
    }

    private void OnEnable()
    {
        ShopItemView.PurchaseClick += OnPurchase;
        AdsHelper.AdShowSuccess += SetItemAdWatched;
    }

    private void OnDisable()
    {
        ShopItemView.PurchaseClick -= OnPurchase;
        AdsHelper.AdShowSuccess -= SetItemAdWatched;
    }

    #endregion // Unity Callbacks

    #region Public Methods
    public void CheckMaxAllowed()
    {
        SetItemAvailability(ItemPurpose.AddLife,
            Controller.Player.Life,
            Controller.Player.LifeMax);
        SetItemAvailability(ItemPurpose.AddRocketMax,
            Controller.Player.RocketMax,
            _shopModel.RocketMaxAllowed);
        SetItemAvailability(ItemPurpose.RefillRockets,
            Controller.Player.RocketCount,
            Controller.Player.RocketMax);
        SetItemAvailability(ItemPurpose.AddShieldPoints,
            Controller.Player.ShieldPoint,
            Controller.Player.ShieldMax);
        SetItemAvailability(ItemPurpose.ShortenShieldRegen,
            Controller.Player.ShieldReplenishSec,
            _shopModel.ShieldRegenSecMinAllowed);
        SetItemAvailability(ItemPurpose.AddCoins,
            Controller.Player.Coins,
            Controller.Player.CoinsMax);
    }

    /// <summary>
    /// To force the UI on the View to reflect the updated values.
    /// </summary>
    public void UpdateViewTexts()
    {
        _shopView.SetUITexts(this);
    }

    public int GetPlayerCoins()
    {
        return Controller.Player.Coins;
    }

    public int GetPlayerLifeCount()
    {
        return Controller.Player.Life;
    }

    public int GetMaxLifeSize()
    {
        return Controller.Player.LifeMax;
    }

    public int GetPlayerRocketCount()
    {
        return Controller.Player.RocketCount;
    }

    public int GetMaxRocketSize()
    {
        return Controller.Player.RocketMax;
    }

    public int GetPlayerShieldPoints()
    {
        return Mathf.RoundToInt(Controller.Player.ShieldPoint);
    }

    public int GetMaxShieldPoints()
    {
        return Mathf.RoundToInt(Controller.Player.ShieldMax);
    }

    public float GetShieldReplenish()
    {
        return Controller.Player.ShieldReplenishSec;
    }

    /// <summary>
    /// Resets the Is Ad Watched value of the Shop Items that are free when playing an Ad.
    /// Must be called when showing the Shop UI.
    /// </summary>
    public void CheckAdItems()
    {
        foreach (ItemPurpose key in _dictShopItem.Keys)
        {
            if (_dictShopItem[key].IsAd)
            {
                _dictShopItem[key].IsAdWatched = false;
                _dictShopItem[key].EnablePurchasing(BUY_AD_TEXT);
            }
        }
    }

    public void SetItemAdWatched()
    {
        var inShop = _dictShopItem.TryGetValue(_cachedPurpose, out ShopItemView item);

        if ((!item.IsAdWatched) && inShop)
        {
            item.IsAdWatched = true;
            item.DisablePurchasing(REASON_DONE_AD);

            ApplyUpgrade(_cachedPurpose, _cachedValue);
            UpdateViewTexts();
            CheckMaxAllowed();
        }
    }
    #endregion // Public Methods

    #region Class Implementation
    // FLOAT VALUE
    private void SetItemAvailability(ItemPurpose purpose, float value, float compareValue)
    {
        var inShop = _dictShopItem.TryGetValue(purpose, out ShopItemView item);
        if ((value == compareValue) && inShop)
        {
            item.DisablePurchasing(REASON_FULL);
        }
        else if (inShop && item.IsAd)
        {
            // handle Ads
            if (item.IsAdWatched)
            {
                item.DisablePurchasing(REASON_DONE_AD);
            }
            else
            {
                item.EnablePurchasing(BUY_AD_TEXT);
            }
        }
        else if (inShop && (Controller.Player.Coins < item.Cost))
        {
            item.DisablePurchasing(REASON_LOW_FUNDS);
        }
        else if (inShop)
        {
            item.EnablePurchasing(BUY_TEXT);
        }
    }

    // INT VALUE
    private void SetItemAvailability(ItemPurpose purpose, int value, int compareValue)
    {
        var inShop = _dictShopItem.TryGetValue(purpose, out ShopItemView item);
        if ((value == compareValue) && inShop)
        {
            item.DisablePurchasing(REASON_FULL);
        }
        else if (inShop && item.IsAd)
        {
            // handle Ads
            if (item.IsAdWatched)
            {
                item.DisablePurchasing(REASON_DONE_AD);
            }
            else
            {
                item.EnablePurchasing(BUY_AD_TEXT);
            }
        }
        else if (inShop && (Controller.Player.Coins < item.Cost))
        {
            item.DisablePurchasing(REASON_LOW_FUNDS);
        }
        else if (inShop)
        {
            item.EnablePurchasing(BUY_TEXT);
        }
    }


    private void ClearContainer()
    {
        foreach (Transform child in _shopItemsContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void BuildShopItems()
    {
        for (int i = 0; i < _shopModel.Items.Count; i++)
        {
            ShopItemView itemView = Instantiate(_shopItemViewPrefab);
            itemView.transform.SetParent(_shopItemsContainer);
            RectTransform rect = itemView.transform as RectTransform;
            rect.localScale = Vector3.one;

            ShopItem shopItem = _shopModel.Items[i];

            itemView.SetIcon(shopItem.ItemIcon, shopItem.CostIcon);
            itemView.SetButtonPurpose(shopItem.ItemPurpose, shopItem.ItemPurposeValue);

            if (shopItem.WatchAd)
            {
                // only add watch ad for Android and iOS
#if UNITY_ANDROID || UNITY_IOS
                itemView.SetText(shopItem.ItemName, shopItem.ItemDescription);
                itemView.SetAd();
                itemView.IsAdWatched = false;
                itemView.EnablePurchasing(BUY_AD_TEXT);

                // register the purpose of this Shop Item View
                _dictShopItem.Add(shopItem.ItemPurpose, itemView);
#endif
            }
            else
            {
                itemView.SetText(shopItem.ItemName, shopItem.ItemDescription, shopItem.ItemCost);
                itemView.EnablePurchasing(BUY_TEXT);

                // register the purpose of this Shop Item View
                _dictShopItem.Add(shopItem.ItemPurpose, itemView);
            }
        }
    }

    private void OnPurchase(ItemPurpose purpose, float value, int cost, bool isAd)
    {
        AudioController.Instance.PlaySFX(SFX.UIClick);
        if (isAd)
        {
            _cachedPurpose = purpose;
            _cachedValue = value;
            Services.Instance.Ads.LoadAd();
        }
        else
        {
            CollectPayment(cost);

            ApplyUpgrade(purpose, value);
            UpdateViewTexts();
            CheckMaxAllowed();
        }

        LogPurchase(purpose);
    }

    private void LogPurchase(ItemPurpose purpose)
    {
        Services.Instance.Analytics.SetCustomEvent(
            AnalyticsKeys.eShopPurchase,
            AnalyticsKeys.pItem,
            purpose.ToString());
    }

    private void CollectPayment(int cost)
    {
        Controller.Player.SpendCoins(cost);
    }

    private void ApplyUpgrade(ItemPurpose purpose, float value)
    {
        if (purpose == ItemPurpose.AddLife)
        {
            Controller.Player.AddLife((int)value);
        }
        else if (purpose == ItemPurpose.AddRocketMax)
        {
            Controller.Player.AddRocketMax((int)value,
                _shopModel.RocketMaxAllowed);
        }
        else if (purpose == ItemPurpose.RefillRockets)
        {
            Controller.Player.RefillRockets();
        }
        else if (purpose == ItemPurpose.AddShieldPoints)
        {
            Controller.Player.AddShieldPoints(value);
        }
        else if (purpose == ItemPurpose.ShortenShieldRegen)
        {
            Controller.Player.ShortenShieldRegen(value,
                _shopModel.ShieldRegenSecMinAllowed);
        }
        else if (purpose == ItemPurpose.AddCoins)
        {
            Controller.Player.AddCoins((int)value);
        }
        else
        {
            Debug.Log("Unregistered item purchased.");
        }
    }
    #endregion // Class Implementation
}