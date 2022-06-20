using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Analytics;

using Unity.Services.Core;
using Unity.Services.Analytics;
using System.Threading.Tasks;

namespace KornerGames.Analytics
{
    public class AnalyticsSystem : MonoBehaviour
    {
        List<string> consentIdentifiers;
        public void SetCustomEvent(string eventName)
        {
            // Legacy Analytics
            //Analytics.CustomEvent(eventName);
            AnalyticsService.Instance.CustomData(eventName,
                new Dictionary<string, object> { });

        }

        public void SetCustomEvent(string eventName, string paramName,
            int currentLevel)
        {
            // Legacy Analytics
            //Analytics.CustomEvent(eventName, new Dictionary<string, object> {
            //    { paramName, currentLevel},
            //});

            AnalyticsService.Instance.CustomData(eventName,
                new Dictionary<string, object> {
                    { paramName, currentLevel }
                });
        }

        public void SetCustomEvent(string eventName, string paramName,
            string shopItem)
        {
            AnalyticsService.Instance.CustomData(eventName,
                new Dictionary<string, object> {
                    { paramName, shopItem }
                });
        }

        public async Task Initialize()
        {
            try
            {
                await UnityServices.InitializeAsync();
                consentIdentifiers =
                    await AnalyticsService.Instance.CheckForRequiredConsents();
            }
            catch (ConsentCheckException e)
            {
                Debug.Log("Something went wrong when checking the GeoIP, " +
                    "check the e.Reason and handle appropriately");
                Debug.Log(e.Reason);
            }
        }

        public void Flush()
        {
            AnalyticsService.Instance.Flush();
        }

        public string GetInitializationState()
        {
            string state = UnityServices.State.ToString();

            Debug.Log(state);
            return state;
        }
    }
}