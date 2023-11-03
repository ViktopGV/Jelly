using System;
using System.Runtime.InteropServices;
using UnityEngine;


public enum PlayerScopes
{
    ONLY_ID,
    PHOTO_AND_NAME
}

public enum PhotoSize
{
    SMALL,
    MIDDLE,
    LARGE

}

[RequireComponent(typeof(YaSDK))]
public class YaPlayer : MonoBehaviour
{
    public bool IsYaPlayerInitialized { get; private set; }
    public static event Action PlayerInitialized;
    public static event Action PlayerLoggedIn;
    public static event Action PlayerRefusedAuth;
    /// <summary>
    /// If the value is not set , it will pass "NullValue" string
    /// </summary>
    public static event Action<string> PlayerGotData;

    [Tooltip("If you need player name and photo, scopes value should be true")]
    public PlayerScopes Scopes = PlayerScopes.ONLY_ID;
    [DllImport("__Internal")]
    public static extern void PlayerInitialize(PlayerScopes scopes = PlayerScopes.ONLY_ID);
    [DllImport("__Internal")]
    public static extern bool IsPlayerAuth();
    [DllImport("__Internal")]
    public static extern void AuthorizePlayer();
    [DllImport("__Internal")]
    public static extern void SetPlayerData(string data);
    [DllImport("__Internal")]
    public static extern void GetPlayerData();
    [DllImport("__Internal")]
    public static extern string GetPlayerUniqueID();
    [DllImport("__Internal")]
    public static extern string GetPlayerName();
    [DllImport("__Internal")]
    public static extern string GetPlayerPhoto(PhotoSize size);

    private YaSDK _sdk;

    private void Awake()
    {
        _sdk = GetComponent<YaSDK>();
        if (_sdk.IsSDKInitialized())
            OnYaSDKInitialized();
        else
            _sdk.SDKInitialized.AddListener(OnYaSDKInitialized);

    }

    private void OnYaSDKInitialized()
    {
#if UNITY_EDITOR
        PlayerInitializedCallback();
#else
        PlayerInitialize(Scopes);
#endif

    }

    private void PlayerInitializedCallback()
    {
        IsYaPlayerInitialized = true;
        PlayerInitialized?.Invoke();
    }

    private void PlayerHasLoggedInCallback()
    {
        PlayerLoggedIn?.Invoke();

    }

    private void PlayerRefusedAuthorizationCallback()
    {
        PlayerRefusedAuth?.Invoke();

    }

    private void GotPlayerDataCallback(string data)
    {
        PlayerGotData?.Invoke(data);
    }
}
