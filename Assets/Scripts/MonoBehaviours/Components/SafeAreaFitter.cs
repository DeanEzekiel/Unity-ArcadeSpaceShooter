using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaFitter : MonoBehaviour
{
    private DeviceOrientation cachedOrientation = DeviceOrientation.Unknown;
    private ScreenOrientation screenOrientation = ScreenOrientation.Landscape;

    [SerializeField]
    private GameObject indicator;

    private void Start()
    {
        //indicator.SetActive(false);
    }

    private void Update()
    {
#if (UNITY_IOS || UNITY_ANDROID)
        // USING DEVICE ORIENTATION
        if ((Input.deviceOrientation == DeviceOrientation.LandscapeLeft) &&
            (cachedOrientation != DeviceOrientation.LandscapeLeft))
        {
            ChangedOrientation();
            cachedOrientation = DeviceOrientation.LandscapeLeft;

            //StartCoroutine(C_Indicator());
        }
        else if ((Input.deviceOrientation == DeviceOrientation.LandscapeRight) &&
            (cachedOrientation != DeviceOrientation.LandscapeRight))
        {
            ChangedOrientation();
            cachedOrientation = DeviceOrientation.LandscapeRight;

            //StartCoroutine(C_Indicator());
        }
#endif

        //// USING SCREEN ORIENTATION
        //if ((Screen.orientation == ScreenOrientation.LandscapeLeft) &&
        //    (screenOrientation != ScreenOrientation.LandscapeLeft))
        //{
        //    ChangedOrientation();
        //    screenOrientation = ScreenOrientation.LandscapeLeft;
        //}
        //else if ((Screen.orientation == ScreenOrientation.LandscapeRight) &&
        //    (screenOrientation != ScreenOrientation.LandscapeRight))
        //{
        //    ChangedOrientation();
        //    screenOrientation = ScreenOrientation.LandscapeRight;
        //}

        //// JUST Check if the screen orientation changed every frame for smoother checking
        //ChangedOrientation();
    }

    private void ChangedOrientation()
    {
        var rectTransform = GetComponent<RectTransform>();
        var safeArea = Screen.safeArea;
        var anchorMin = safeArea.position;
        var anchorMax = anchorMin + safeArea.size;

        print($"Before Min {anchorMin.x} & {anchorMin.y}");
        print($"Before Max {anchorMax.x} & {anchorMax.y}");

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        print($"After Min {anchorMin.x} & {anchorMin.y}");
        print($"After Max {anchorMax.x} & {anchorMax.y}");

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }

    private IEnumerator C_Indicator()
    {
        indicator.SetActive(true);
        yield return new WaitForSeconds(3);

        indicator.SetActive(false);
    }
}
