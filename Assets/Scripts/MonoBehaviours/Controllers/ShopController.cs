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

    [SerializeField]
    private string REASON_FULL = "Full";
    [SerializeField]
    private string REASON_LOW_FUNDS = "Low funds";
    #endregion // Inspector Fields

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
    }

    private void OnDisable()
    {
        ShopItemView.PurchaseClick -= OnPurchase;
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
    #endregion // Public Methods

    #region Class Implementation
    private void SetItemAvailability(ItemPurpose purpose, float value, float compareValue)
    {
        var inShop = _dictShopItem.TryGetValue(purpose, out ShopItemView item);
        if ((value == compareValue) && inShop)
        {
            item.DisablePurchasing(REASON_FULL);
        }
        else if (inShop && (Controller.Player.Coins < item.Cost))
        {
            item.DisablePurchasing(REASON_LOW_FUNDS);
        }
        else if (inShop)
        {
            item.EnablePurchasing();
        }
    }

    private void SetItemAvailability(ItemPurpose purpose, int value, int compareValue)
    {
        var inShop = _dictShopItem.TryGetValue(purpose, out ShopItemView item);
        if ((value == compareValue) && inShop)
        {
            item.DisablePurchasing(REASON_FULL);
        }
        else if (inShop && (Controller.Player.Coins < item.Cost))
        {
            item.DisablePurchasing(REASON_LOW_FUNDS);
        }
        else if (inShop)
        {
            item.EnablePurchasing();
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

            itemView.SetText(shopItem.ItemName, shopItem.ItemDescription, shopItem.ItemCost);
            itemView.SetIcon(shopItem.ItemIcon);
            itemView.SetButtonPurpose(shopItem.ItemPurpose, shopItem.ItemPurposeValue);
            itemView.EnablePurchasing();

            // register the purpose of this Shop Item View
            _dictShopItem.Add(shopItem.ItemPurpose, itemView);
        }
    }

    private void OnPurchase(ItemPurpose purpose, float value, int cost)
    {
        CollectPayment(cost);
        ApplyUpgrade(purpose, value);
        UpdateViewTexts();
        CheckMaxAllowed();
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
        else
        {
            Debug.Log("Unregistered item purchased.");
        }
    }
    #endregion // Class Implementation
}