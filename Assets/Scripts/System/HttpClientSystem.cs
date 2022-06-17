using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace KornerGames.HttpClient
{
    public class HttpClientSystem : MonoBehaviour
    {
        private UnityWebRequest www;

        //public static event Action<string> ShowMessage;

        public async Task<TResultType> Get<TResultType>(string url, string userAgent, string accept)
        {
            www = UnityWebRequest.Get(url);
            www.SetRequestHeader("User-Agent", userAgent);
            www.SetRequestHeader("Accept", accept);

            var operation = www.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            var jsonResponse = www.downloadHandler.text;

            if (www.result != UnityWebRequest.Result.Success)
            {
                var message = $"{GetType().Name} Failed: {www.error}";
                Debug.Log(message);
                //ShowMessage?.Invoke(jsonResponse);
                return default;
            }

            try
            {
                var result = JsonUtility.FromJson<TResultType>(jsonResponse);
                var message = $"{GetType().Name} Success: {jsonResponse}";
                Debug.Log(message);
                //ShowMessage?.Invoke(message);
                return result;
            }
            catch (Exception ex)
            {
                var message = $"{GetType().Name}  Could not parse response {jsonResponse}. {ex.Message}";
                Debug.Log(message);
                //ShowMessage?.Invoke(message);
                return default;
            }
        }

        public void Abort()
        {
            if (www != null && !www.isDone)
            {
                www.Abort();
                var message = $"{GetType().Name} Aborted.";
                Debug.Log(message);
                //ShowMessage?.Invoke(message);
            }
        }
    }
}