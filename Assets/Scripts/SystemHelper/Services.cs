using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Services : ASingleton<Services>
{
    #region Services
    [SerializeField]
    public AdsHelper Ads;
    #endregion // Services

    #region UI
    [SerializeField]
    private GameObject _popUp;
    [SerializeField]
    private TextMeshProUGUI _shortMessage;
    [SerializeField]
    private Button _btnClose;
    #endregion // UI

    #region Unity Callbacks
    protected override void Awake()
    {
        base.Awake();
        Ads.AwakeInit();
    }

    private void Start()
    {
        Ads.StartInit();
        ClosePopUp(); // initially set it to closed

        _btnClose.onClick.AddListener(ClosePopUp);
    }

    private void OnEnable()
    {
        AdsHelper.AdShowMessage += ShowMessage;
    }

    private void OnDisable()
    {
        AdsHelper.AdShowMessage -= ShowMessage;
    }
    #endregion // Unity Callbacks

    #region Implementation
    private void ShowMessage(string message)
    {
        _popUp.SetActive(true);
        _shortMessage.text = message;
    }

    private void ClosePopUp()
    {
        _popUp.SetActive(false);
    }
    #endregion // Implementation
}
