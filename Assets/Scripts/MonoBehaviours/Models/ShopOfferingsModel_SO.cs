using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Offerings",
    menuName = "ScriptableObjects/New Shop Offerings")]
public class ShopOfferingsModel_SO : ScriptableObject
{
    public List<ShopItem> Items = new List<ShopItem>();
}
