using System;
using System.Runtime.InteropServices;
using UnityEngine;


public class YaAdv : MonoBehaviour
{
    public static event Action FullscreenAdOpened;
    public static event Action<bool> FullscreenAdClosed;
    public static event Action<string> FullscreenAdError;
    public static event Action FullscreenAdOffline;

    public static event Action<int> RewardAdOpened;
    public static event Action<int> AdRewarded;
    public static event Action<int> RewardAdClosed;
    public static event Action<string> RewardAdError;

    public static bool FSAdOpen = false;


    [DllImport("__Internal")]
    public static extern void ShowFullscreenAdv();
    [DllImport("__Internal")]
    public static extern void ShowRewardAd(int id);

    private void FullscreenAdOpenCallback()
    {
        FullscreenAdOpened?.Invoke();
        FSAdOpen = true;
    }

    private void FullscreenAdCloseCallback(int wasShown)
    {
        FullscreenAdClosed?.Invoke(Convert.ToBoolean(wasShown));
        FSAdOpen = false;
    }

    private void FullscreenAdErrorCallback(string error)
    {
        FullscreenAdError?.Invoke(error);
    }

    private void FullscreenAdOfflineCallback()
    {
        FullscreenAdOffline?.Invoke();

    }

    private void RewardAdOpenCallback(int id)
    {
        RewardAdOpened?.Invoke(id);
    }

    private void RewardAdRewardedCallback(int id)
    {
        AdRewarded?.Invoke(id);
    }

    private void RewardAdCloseCallback(int id)
    {
        RewardAdClosed?.Invoke(id);

    }

    private void RewardAdErrorCallback(string error)
    {
        RewardAdError?.Invoke(error);
    }
}
