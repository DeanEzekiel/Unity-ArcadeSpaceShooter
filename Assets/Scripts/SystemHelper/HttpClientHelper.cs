using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using KornerGames.HttpClient;
using UnityEngine;

public class HttpClientHelper : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private string _userAgent = "Arcade Shooter by Korner Games";
    [SerializeField]
    private string _accept = "application/json";

    private HttpClientSystem _httpClient;
    #endregion // Fields

    #region Init
    public void AwakeInit()
    {
        _httpClient = GetComponent<HttpClientSystem>();
    }
    #endregion

    #region Implementation
    public async Task<TResultType> Get<TResultType>(string url)
    {
        return await _httpClient.Get<TResultType>(url, _userAgent, _accept);
    }

    public void Abort()
    {
        _httpClient.Abort();
    }
    #endregion // Implementation
}
