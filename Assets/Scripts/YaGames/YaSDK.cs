using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;


public enum Device
{
    DESKTOP,
    MOBILE,
    TABLET,
    TV,
    ERROR
}


public class YaSDK : MonoBehaviour
{
    public UnityEvent SDKInitialized;
    public bool IsSDKInitialized() => _isSDKInitialized;

    [DllImport("__Internal")]
    private static extern void SDKInitialize();
    [DllImport("__Internal")]
    public static extern void SetData(string key, string value);
    [DllImport("__Internal")]
    public static extern string GetData(string key);
    [DllImport("__Internal")]
    private static extern string GetDeviceInformation();
    [DllImport("__Internal")]
    private static extern string GetEnvironmentData();

    private bool _isSDKInitialized = false;

    public static Device GetDeviceInfo()
    {
#if UNITY_EDITOR
        return Device.DESKTOP;
#else
        switch (GetDeviceInformation())
        {
            case "desktop":
                return Device.DESKTOP;
            case "mobile":
                return Device.MOBILE;
            case "tablet":
                return Device.TABLET;
            case "tv":
                return Device.TV;
            default:
                return Device.ERROR;
        }
#endif
    }

    public static Environment GetEnvironment()
    {
        return JsonUtility.FromJson<Environment>(GetEnvironmentData());
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
#if UNITY_EDITOR
        SDKInitializedCallback();
#else
        YaSDK.SDKInitialize();
#endif
    }

    private void SDKInitializedCallback()
    {
        _isSDKInitialized = true;
        YaAdv.ShowFullscreenAdv();
        SDKInitialized?.Invoke();
    }
}

[Serializable]
public struct App
{
    public string id;
}
[Serializable]
public struct Browser
{
    public string lang;
}
[Serializable]
public struct Data
{
    public string baseUrl;
    public string secondDomain;
}
[Serializable]
public struct I18n
{
    public string lang;
    public string tld;
}
[Serializable]
public struct Environment
{
    public App app;
    public Browser browser;
    public Data data;
    public I18n i18n;
}
