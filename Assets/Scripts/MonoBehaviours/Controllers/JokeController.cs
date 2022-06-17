using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class JokeController : MonoBehaviour
{
    [ContextMenu("Test Get Joke")]
    public async void GetJoke()
    {
        var url = "https://icanhazdadjoke.com/";

        using var www = UnityWebRequest.Get(url);
        www.SetRequestHeader("User-Agent", "Arcade Shooter by Korner Games");
        www.SetRequestHeader("Accept", "application/json");

        var operation = www.SendWebRequest();

        while (!operation.isDone)
        {
            await Task.Yield();
        }

        var jsonResponse = www.downloadHandler.text;

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Failed: {www.error}");
        }

        try
        {
            var result = JsonUtility.FromJson<JokeInfo>(jsonResponse);
            Debug.Log($"Success: {jsonResponse}");
            Debug.Log($"Success: {result.joke}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"{this} Could not parse response {jsonResponse}. {ex.Message}");
        }
    }
}
