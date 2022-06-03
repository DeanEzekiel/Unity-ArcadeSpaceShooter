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
        // TODO e.g. if the lives is at MAX, DisablePurchasing of AddLife Purpose
        // use the _dictShopItem
    }

    // TODO call this after purchase is made and after the values are updated
    // to force the texts to refresh

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
        return GameMaster.Instance.playerSettings.shieldReplenishTimeInSec;
    }
    #endregion // Public Methods

    #region Class Implementation
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

    private void OnPurchase(ItemPurpose purpose, float value)
    {
        // TODO Clamp the values there should be a max of allowable upgrades
        Debug.Log($"Clicked {purpose} with a value of {value}");
    }
    #endregion // Class Implementation
}