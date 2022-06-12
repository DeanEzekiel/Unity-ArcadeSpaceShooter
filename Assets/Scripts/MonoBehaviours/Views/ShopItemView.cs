using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemView : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private TextMeshProUGUI _title;
    [SerializeField]
    private TextMeshProUGUI _description;
    [SerializeField]
    private TextMeshProUGUI _cost;
    [SerializeField]
    private Image _costIcon;
    [SerializeField]
    private Button _purchaseButton;
    [SerializeField]
    private TextMeshProUGUI _purchaseButtonText;
    #endregion // Inspector Fields

    #region Private Fields
    private ItemPurpose _purpose;
    private float _value;

    private readonly string _adCostText = "Ad";
    private bool _isAd = false;
    #endregion // Private Fields

    #region Events
    /// <summary>
    /// Holds the Purpose & Value of the Purchase.
    /// </summary>
    public static event Action<ItemPurpose, float, int, bool> PurchaseClick;
    #endregion // Events

    #region Accessors
    public ItemPurpose Purpose => _purpose;
    public bool IsAd => _isAd;
    public int Cost;
    public bool IsAdWatched = false;
    #endregion // Accessors

    #region Unity Callbacks
    private void Start()
    {
        _purchaseButton.onClick.AddListener(OnPurchase);
    }
    #endregion // Unity Callbacks

    #region Class Implementation
    private void OnPurchase()
    {
        PurchaseClick?.Invoke(_purpose, _value, Cost, _isAd);
    }
    #endregion // Class Implementation

    #region Public Methods
    public void EnablePurchasing(string purchaseText)
    {
        _purchaseButton.interactable = true;
        _purchaseButtonText.text = purchaseText;

    }

    public void DisablePurchasing(string reason)
    {
        _purchaseButton.interactable = false;
        _purchaseButtonText.text = reason;
    }

    public void SetText(string title, string description)
    {
        _title.text = title;
        _description.text = description;
    }

    public void SetText(string title, string description, int cost)
    {
        _title.text = title;
        _description.text = description;
        Cost = cost;
        _cost.text = cost.ToString();
    }

    public void SetIcon(Sprite sprite)
    {
        _icon.sprite = sprite;
    }

    public void SetIcon(Sprite mainIcon, Sprite costIcon)
    {
        _icon.sprite = mainIcon;
        _costIcon.sprite = costIcon;
    }

    public void SetAd()
    {
        _cost.text = _adCostText;
        _isAd = true;
    }

    public void SetButtonPurpose(ItemPurpose purpose, float value)
    {
        _purpose = purpose;
        _value = value;
    }
    #endregion // Public Methods
}
