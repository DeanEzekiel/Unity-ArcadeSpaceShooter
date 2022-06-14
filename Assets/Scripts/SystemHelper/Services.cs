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
    [SerializeField]
    public AnalyticsHelper Analytics;
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
        Analytics.AwakeInit();
    }

    private void Start()
    {
        // Ads Start
        Ads.StartInit();
        ClosePopUp(); // initially set it to closed

        _btnClose.onClick.AddListener(ClosePopUp);
        // End of Ads Start

        // Analytics Start
        StartAnalytics();
        // end of Analytics Start
    }

    private void OnEnable()
    {
        AdsHelper.AdShowMessage += ShowMessage;
    }

    private void OnDisable()
    {
        AdsHelper.AdShowMessage -= ShowMessage;
    }

    private void OnApplicationQuit()
    {
        Analytics.SetCustomEvent(AnalyticsKeys.eGameClosed);
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

    private async void StartAnalytics()
    {
        await Analytics.Initialize();
        Analytics.SetCustomEvent(AnalyticsKeys.eGameLaunched);
    }
    #endregion // Implementation
}