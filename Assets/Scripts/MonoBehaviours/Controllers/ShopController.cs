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

    #region Public Actions
    public void CheckMaxAllowed()
    {
        // TODO e.g. if the lives is at MAX, DisablePurchasing of AddLife Purpose
        // use the _dictShopItem
    }

    // TODO Update the Texts of the Shop UI - Coins, Lives, Rockets, Shield Pnts
    #endregion // Public Actions

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
        Debug.Log($"Clicked {purpose} with a value of {value}");
    }
    #endregion // Class Implementation
}