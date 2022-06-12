using System;
using UnityEngine;

[Serializable]
public class ShopItem
{
    public string ItemName;
    public string ItemDescription;
    public Sprite ItemIcon;
    public int ItemCost;
    public Sprite CostIcon;
    public ItemPurpose ItemPurpose;
    public float ItemPurposeValue;

    public bool WatchAd;
}
