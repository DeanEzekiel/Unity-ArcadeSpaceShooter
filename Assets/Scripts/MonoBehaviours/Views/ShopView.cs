using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopView : MonoBehaviour
{
    #region Inspector Fields
    [Header("Shop UI Texts")]
    [SerializeField]
    private TextMeshProUGUI _coinsText;
    [SerializeField]
    private TextMeshProUGUI _livesText;
    [SerializeField]
    private TextMeshProUGUI _rocketsText;
    [SerializeField]
    private TextMeshProUGUI _shieldPointsText;
    [SerializeField]
    private TextMeshProUGUI _shieldRegenText;
    #endregion // Inspector Fields

    #region Private Fields
    private readonly string divider = "/";
    private readonly string strSec = " sec";
    #endregion // Private Fields

    #region Public Methods
    public void SetUITexts(ShopController controller)
    {
        _coinsText.text = controller.GetPlayerCoins().ToString();
        _livesText.text = controller.GetPlayerLifeCount().ToString()
            + divider
            + controller.GetMaxLifeSize().ToString();
        _rocketsText.text = controller.GetPlayerRocketCount().ToString()
            + divider
            + controller.GetMaxRocketSize().ToString();
        _shieldPointsText.text = controller.GetPlayerShieldPoints().ToString()
            + divider
            + controller.GetMaxShieldPoints().ToString();
        _shieldRegenText.text = controller.GetShieldReplenish().ToString()
            + strSec;
    }
    #endregion
}
