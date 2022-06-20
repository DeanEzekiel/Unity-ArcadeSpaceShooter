using UnityEngine;
using System.Threading.Tasks;
using KornerGames.Analytics;

public class AnalyticsHelper : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private bool _isInitialized = false;

    private AnalyticsSystem _analytics;
    #endregion

    #region Init
    public void AwakeInit()
    {
        _analytics = GetComponent<AnalyticsSystem>();
    }
    #endregion

    #region Implementation
    // Game Manager calls this
    public async Task Initialize()
    {
        var isConnectionOn = Connection.Check();

        if (isConnectionOn)
        {
            await _analytics.Initialize();

            var status = _analytics.GetInitializationState();
            if (status == "Initialized")
            {
                _isInitialized = true;
                Debug.Log($"Analytics status {status}");
            }
        }
    }

    public async void SetCustomEvent(string eventName)
    {
        await CheckReInitialize();

        var isConnectionOn = Connection.Check();
        if (_isInitialized && isConnectionOn)
        {
            Debug.Log($"Custom Event: {eventName}");

            _analytics.SetCustomEvent(eventName);
            _analytics.Flush();
        }
    }

    public async void SetCustomEvent(string eventName, string paramName,
        int currentLevel)
    {
        await CheckReInitialize();

        var isConnectionOn = Connection.Check();
        if (_isInitialized && isConnectionOn)
        {
            Debug.Log($"Custom Event: {eventName} " +
                $"with param {paramName} - {currentLevel}");

            _analytics.SetCustomEvent(eventName, paramName, currentLevel);
            _analytics.Flush();
        }
    }

    public async void SetCustomEvent(string eventName, string paramName,
        string shopItem)
    {
        await CheckReInitialize();

        var isConnectionOn = Connection.Check();
        if (_isInitialized && isConnectionOn)
        {
            Debug.Log($"Custom Event: {eventName} " +
                $"with param {paramName} - {shopItem}");

            _analytics.SetCustomEvent(eventName, paramName, shopItem);
            _analytics.Flush();
        }
    }

    private async Task CheckReInitialize()
    {
        // if mid-game, the connection is enabled and not yet initialized
        var isConnectionOn = Connection.Check();
        if ((!_isInitialized) && (isConnectionOn))
        {
            await Initialize();
        }
    }
    #endregion
}