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
    private Button _purchaseButton;
    [SerializeField]
    private TextMeshProUGUI _purchaseButtonText;
    #endregion // Inspector Fields

    #region Private Fields
    private ItemPurpose _purpose;
    private float _value;

    private readonly string _purchaseButtonActive = "Buy";
    private readonly string _purchaseButtonInactive = "Full";
    #endregion // Private Fields

    #region Events
    /// <summary>
    /// Holds the Purpose & Value of the Purchase.
    /// </summary>
    public static event Action<ItemPurpose, float> PurchaseClick;
    #endregion // Events

    #region Accessors
    public ItemPurpose Purpose => _purpose;
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
        PurchaseClick?.Invoke(_purpose, _value);
    }
    #endregion // Class Implementation

    #region Public Methods
    public void EnablePurchasing()
    {
        _purchaseButton.interactable = true;
        _purchaseButtonText.text = _purchaseButtonActive;

    }

    public void DisablePurchasing()
    {
        _purchaseButton.interactable = false;
        _purchaseButtonText.text = _purchaseButtonInactive;
    }

    public void SetText(string title, string description, int cost)
    {
        _title.text = title;
        _description.text = description;
        _cost.text = cost.ToString();
    }

    public void SetIcon(Sprite sprite)
    {
        _icon.sprite = sprite;
    }

    public void SetButtonPurpose(ItemPurpose purpose, float value)
    {
        _purpose = purpose;
        _value = value;
    }
    #endregion // Public Methods
}
