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
            GameMaster.Instance.playerSettings.life,
            GameMaster.Instance.playerSettings.lifeMax);
        SetItemAvailability(ItemPurpose.AddRocketMax,
            GameMaster.Instance.playerSettings.rocketMax,
            _shopModel.RocketMaxAllowed);
        SetItemAvailability(ItemPurpose.RefillRockets,
            GameMaster.Instance.playerSettings.rocketCount,
            GameMaster.Instance.playerSettings.rocketMax);
        SetItemAvailability(ItemPurpose.AddShieldPoints,
            GameMaster.Instance.playerSettings.shieldPoint,
            GameMaster.Instance.playerSettings.shieldMax);
        SetItemAvailability(ItemPurpose.ShortenShieldRegen,
            GameMaster.Instance.playerSettings.shieldReplenishSec,
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
        return GameMaster.Instance.playerSettings.coins;
    }

    public int GetPlayerLifeCount()
    {
        return GameMaster.Instance.playerSettings.life;
    }

    public int GetMaxLifeSize()
    {
        return GameMaster.Instance.playerSettings.lifeMax;
    }

    public int GetPlayerRocketCount()
    {
        return GameMaster.Instance.playerSettings.rocketCount;
    }

    public int GetMaxRocketSize()
    {
        return GameMaster.Instance.playerSettings.rocketMax;
    }

    public int GetPlayerShieldPoints()
    {
        return Mathf.RoundToInt(GameMaster.Instance.playerSettings.shieldPoint);
    }

    public int GetMaxShieldPoints()
    {
        return Mathf.RoundToInt(GameMaster.Instance.playerSettings.shieldMax);
    }

    public float GetShieldReplenish()
    {
        return GameMaster.Instance.playerSettings.shieldReplenishSec;
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
        else if (inShop && (GameMaster.Instance.playerSettings.coins < item.Cost))
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
        else if (inShop && (GameMaster.Instance.playerSettings.coins < item.Cost))
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
        GameMaster.Instance.playerSettings.coins -= cost;
    }

    private void ApplyUpgrade(ItemPurpose purpose, float value)
    {
        if (purpose == ItemPurpose.AddLife)
        {
            GameMaster.Instance.playerSettings.life = Mathf.Clamp(
                GameMaster.Instance.playerSettings.life + (int)value,
                GameMaster.Instance.playerSettings.life,
                GameMaster.Instance.playerSettings.lifeMax
                );
        }
        else if (purpose == ItemPurpose.AddRocketMax)
        {
            GameMaster.Instance.playerSettings.rocketMax = Mathf.Clamp(
                GameMaster.Instance.playerSettings.rocketMax + (int)value,
                GameMaster.Instance.playerSettings.rocketMax,
                _shopModel.RocketMaxAllowed
                );
        }
        else if (purpose == ItemPurpose.RefillRockets)
        {
            GameMaster.Instance.playerSettings.rocketCount =
                GameMaster.Instance.playerSettings.rocketMax;
        }
        else if (purpose == ItemPurpose.AddShieldPoints)
        {
            GameMaster.Instance.playerSettings.shieldPoint = Mathf.Clamp(
                GameMaster.Instance.playerSettings.shieldPoint + value,
                GameMaster.Instance.playerSettings.shieldPoint,
                GameMaster.Instance.playerSettings.shieldMax
                );
        }
        else if (purpose == ItemPurpose.ShortenShieldRegen)
        {
            GameMaster.Instance.playerSettings.shieldReplenishSec = Mathf.Clamp(
                GameMaster.Instance.playerSettings.shieldReplenishSec - value,
                _shopModel.ShieldRegenSecMinAllowed,
                GameMaster.Instance.playerSettings.shieldReplenishSec
                );
        }
        else
        {
            Debug.Log("Unregistered item purchased.");
        }
    }
    #endregion // Class Implementation
}