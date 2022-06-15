using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace AdsImplementation
{
    public class AdsSystem : MonoBehaviour,
        IUnityAdsInitializationListener,
        IUnityAdsLoadListener,
        IUnityAdsShowListener
    {
        private string _gameId;
        private string _adUnitId;

        #region Events
        public static event Action AdInitializeComplete;
        public static event Action AdInitializeFailed;

        public static event Action AdLoadComplete;
        public static event Action AdLoadFailed;

        public static event Action AdShowStart;
        public static event Action AdShowClick;
        public static event Action AdShowComplete;
        public static event Action AdShowFailed;
        #endregion


        public void DeterminePlatform(string androidAdUnitId, string iOsAdUnitId)
        {
            // Get the Ad Unit ID for the current platform:
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? iOsAdUnitId
                : androidAdUnitId;
        }

        #region Initializer

        public void InitializeAds(string androidGameID, string iOSGameID, bool testMode)
        {
            _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? iOSGameID
                : androidGameID;
            Advertisement.Initialize(_gameId, testMode, this);
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
            AdInitializeComplete?.Invoke();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
            AdInitializeFailed?.Invoke();
        }

        #endregion

        #region Ad Loading and Showing

        // Load content to the Ad Unit:
        public void LoadAd()
        {
            // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }

        [ContextMenu("Show Ad")]
        // Show the loaded content in the Ad Unit:
        public void ShowAd()
        {
            // Note that if the ad content wasn't previously loaded, this method will fail
            Debug.Log("Showing Ad: " + _adUnitId);
            Advertisement.Show(_adUnitId, this);
        }

        // Implement Load Listener and Show Listener interface methods: 
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            // Optionally execute code if the Ad Unit successfully loads content.
            Debug.Log("Checking if this is called: OnUnityAdsAdLoaded");
            AdLoadComplete?.Invoke();
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
            AdLoadFailed?.Invoke();
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
            AdShowFailed?.Invoke();
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
            AdShowStart?.Invoke();
        }

        public void OnUnityAdsShowClick(string adUnitId)
        {
            AdShowClick?.Invoke();
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            AdShowComplete?.Invoke();
        }
        #endregion
    }
}