using System.Collections;
using UnityEngine;
using AdsImplementation;
using System;

public class AdsHelper : MonoBehaviour
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;

    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";

    private AdsSystem _adsSystem;
    private bool _isInitialized = false;

    private bool _retryLoadingAd = false;
    private bool _isConnectionOn = false;

    [Space]
    [SerializeField]
    private string _successLoadMsg = "Congrats! You got free coins.";
    [SerializeField]
    private string _failLoadMsg = "Unable to load ad and cannot claim free coins.";
    [SerializeField]
    private string _failInitMsg = "Since this step failed, you may notice slowness later on when you claim free coins.";

    #region Events
    public static event Action AdShowSuccess;
    public static event Action<string> AdShowMessage;
    #endregion // Events

    #region Public API
    public void AwakeInit()
    {
        _adsSystem = GetComponent<AdsSystem>();

        _adsSystem.DeterminePlatform(_androidAdUnitId, _iOsAdUnitId);

        _isConnectionOn = Connection.Check();
    }

    public void StartInit()
    {
        if (_isConnectionOn)
        {
            _adsSystem.InitializeAds(_androidGameId, _iOSGameId, _testMode);
        }
        else
        {
            var message = "Initialization of Ads failed. Please check your Internet Connection. \n \n" +
                _failInitMsg;
            AdShowMessage?.Invoke(message);
        }
    }

    public void LoadAd()
    {
        var message = "Loading. Please wait.";
        AdShowMessage?.Invoke(message);

        _isConnectionOn = Connection.Check();

        if (_isInitialized && _isConnectionOn)
        {
            _adsSystem.LoadAd();
            _retryLoadingAd = false;
        }
        else if (_isConnectionOn)
        {
            _adsSystem.InitializeAds(_androidGameId, _iOSGameId, _testMode);
            _retryLoadingAd = true;
        }
        else
        {
            message = _failLoadMsg + " Please check your internet connection.";
            AdShowMessage?.Invoke(message);
        }
    }
    #endregion

    #region Unity Callbacks

    private void OnEnable()
    {
        AdsSystem.AdInitializeComplete += AdInitializeComplete;
        AdsSystem.AdInitializeFailed += AdInitializeFailed;

        AdsSystem.AdLoadComplete += AdLoadComplete;
        AdsSystem.AdLoadFailed += AdLoadFailed;

        AdsSystem.AdShowStart += AdShowStart;
        AdsSystem.AdShowClick += AdShowClick;
        AdsSystem.AdShowComplete += AdShowComplete;
        AdsSystem.AdShowFailed += AdShowFailed;
    }

    private void OnDisable()
    {
        AdsSystem.AdInitializeComplete -= AdInitializeComplete;
        AdsSystem.AdInitializeFailed -= AdInitializeFailed;

        AdsSystem.AdLoadComplete -= AdLoadComplete;
        AdsSystem.AdLoadFailed -= AdLoadFailed;

        AdsSystem.AdShowStart -= AdShowStart;
        AdsSystem.AdShowClick -= AdShowClick;
        AdsSystem.AdShowComplete -= AdShowComplete;
        AdsSystem.AdShowFailed -= AdShowFailed;
    }
    #endregion

    #region Implementation
    private void AdInitializeComplete()
    {
        _isInitialized = true;

        if (_retryLoadingAd)
        {
            LoadAd();
        }
    }

    private void AdInitializeFailed()
    {
        _isInitialized = false;
        Debug.Log("Ad Initialize Failed");

        if (_retryLoadingAd)
        {
            var message = "Ad Initialization failed. " +
                "Please check your internet connection.";
            AdShowMessage?.Invoke(message);
        }
    }

    private void AdLoadComplete()
    {
        ShowAd();
    }

    private void AdLoadFailed()
    {
        Debug.Log("Ad Load Failed");

        var message = "Cannot load Ad. " +
            "Please check your internet connection.";
        AdShowMessage?.Invoke(message);
    }

    private void AdShowStart()
    {
        Debug.Log("Ad Show Start");
    }

    private void AdShowClick()
    {
        Debug.Log("Ad Show Click");
    }

    private void AdShowComplete()
    {
        Debug.Log("Ad Show Complete");
        StartCoroutine(OnAdCompleted());
    }

    private void AdShowFailed()
    {
        Debug.Log("Ad Show Failed");
        var message = "Something went wrong upon showing Ad. " +
            "Please check your internet connection.";
        AdShowMessage?.Invoke(message);
        //GameUIView.Instance.ShowAdResult(message);
        //AnalyticsHelper.Instance.SetCustomEvent(AnalyticsKeys.eventAdFailed);
        Services.Instance.Analytics.SetCustomEvent(AnalyticsKeys.eAdFailed);
    }

    private void ShowAd()
    {
        _adsSystem.ShowAd();
    }

    private IEnumerator OnAdCompleted()
    {
        yield return new WaitForSeconds(2);
        AdShowMessage?.Invoke(_successLoadMsg);
        AdShowSuccess?.Invoke();
        //AnalyticsHelper.Instance.SetCustomEvent(AnalyticsKeys.eventAdSuccess);
        Services.Instance.Analytics.SetCustomEvent(AnalyticsKeys.eAdFinished);
    }
    #endregion
}