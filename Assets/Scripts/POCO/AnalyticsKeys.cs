public struct AnalyticsKeys
{
    public static readonly string eGameLaunched = "Game_Launched";
    public static readonly string eGameClosed = "Game_Closed";
    /// <summary>
    /// Needs an int parameter. Name of the parameter is stored in pRound
    /// </summary>
    public static readonly string eRoundCleared = "Round_Cleared"; // w/ param
    /// <summary>
    /// Needs an int parameter. Name of the parameter is stored in pRound
    /// </summary>
    public static readonly string eGameOver = "Game_Over";
    public static readonly string eAdFinished = "Ad_Finished";
    public static readonly string eAdFailed = "Ad_Failed";
    /// <summary>
    /// Needs a string parameter. Name of the parameter is stored in pItem
    /// Values are saved: vLife, vRechargeRockets, vUpgradeRocket, vUpgradeShieldRecharge, vUpgradeShieldCap
    /// </summary>
    public static readonly string eShopPurchase = "Shop_Purchase"; // w/ param

    public static readonly string pRound = "Round";
    public static readonly string pItem = "Item";

    public static readonly string vLife = "Plus 1 Life";
    public static readonly string vRechargeRockets = "Recharge Rockets";
    public static readonly string vUpgradeRocketMax = "Upgrade Rocket Ammo Max";
    public static readonly string vUpgradeShieldRecharge = "Upgrade Shield Recharge Rate";
    public static readonly string vAddShieldPoints = "Add Shield Points";
    public static readonly string vAddCoins = "Add Coins";
}
