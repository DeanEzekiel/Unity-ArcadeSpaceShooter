using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Offerings",
    menuName = "ScriptableObjects/New Shop Offerings")]
public class ShopOfferings : ScriptableObject
{
    public List<ShopItem> Items = new List<ShopItem>();
}
