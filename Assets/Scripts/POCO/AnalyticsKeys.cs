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
    /// The value will be the enum ItemPurpose
    /// </summary>
    public static readonly string eShopPurchase = "Shop_Purchase"; // w/ param
    /// <summary>
    /// Needs a string parameter. Name of the parameter is stored in pError
    /// </summary>
    public static readonly string eErrorCaught = "Error_Caught"; // w/ param

    public static readonly string pRound = "Round";
    public static readonly string pItem = "Item";
    public static readonly string pError = "Error_Message";
}
