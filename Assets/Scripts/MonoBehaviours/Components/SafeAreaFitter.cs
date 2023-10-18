using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaFitter : MonoBehaviour
{
    private DeviceOrientation cachedOrientation = DeviceOrientation.Unknown;
    // Unknown is obsolete in ScreenOrientation
    private ScreenOrientation screenOrientation = ScreenOrientation.LandscapeLeft;

    private void Update()
    {
#if (UNITY_IOS || UNITY_ANDROID)
        //// USING DEVICE ORIENTATION
        //if ((Input.deviceOrientation == DeviceOrientation.LandscapeLeft) &&
        //    (cachedOrientation != DeviceOrientation.LandscapeLeft))
        //{
        //    StartCoroutine(C_ChangedOrientation());
        //    cachedOrientation = DeviceOrientation.LandscapeLeft;
        //}
        //else if ((Input.deviceOrientation == DeviceOrientation.LandscapeRight) &&
        //    (cachedOrientation != DeviceOrientation.LandscapeRight))
        //{
        //    StartCoroutine(C_ChangedOrientation());
        //    cachedOrientation = DeviceOrientation.LandscapeRight;
        //}

        // USING SCREEN ORIENTATION
        if ((Screen.orientation == ScreenOrientation.LandscapeLeft) &&
            (screenOrientation != ScreenOrientation.LandscapeLeft))
        {
            StartCoroutine(C_ChangedOrientation());
            screenOrientation = ScreenOrientation.LandscapeLeft;
        }
        else if ((Screen.orientation == ScreenOrientation.LandscapeRight) &&
            (screenOrientation != ScreenOrientation.LandscapeRight))
        {
            StartCoroutine(C_ChangedOrientation());
            screenOrientation = ScreenOrientation.LandscapeRight;
        }
#endif
    }

    private IEnumerator C_ChangedOrientation()
    {
        yield return null;

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
}
