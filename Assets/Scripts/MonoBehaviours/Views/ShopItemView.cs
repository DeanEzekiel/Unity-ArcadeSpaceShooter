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
    #endregion // Inspector Fields

    #region Private Fields
    private ItemPurpose _purpose;
    private float _value;
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

    #region Public Actions
    public void ActivatePurchasing()
    {
        _purchaseButton.interactable = true;
    }

    public void DeactivatePurchasing()
    {
        _purchaseButton.interactable = false;
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
    #endregion // Public Actions
}
