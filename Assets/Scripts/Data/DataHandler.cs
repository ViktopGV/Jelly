using System;
using System.Linq;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    //This is a crutch. If the flag is True, then we will hide the entire UI in Game Manager and leave only "Touch to start"
    public bool IsReplay = false;
    public PlayerData PlayerData => _data;
    public event Action DataLoaded;
    public event Action CoinsValueChanged;
    public bool IsDataReady => _isDataReady;

    [SerializeField] private YaPlayer _yaPlayer;
    private PlayerData _data;
    private bool _isDataReady = false;

    public void SetActiveSkinId(int id) => _data.ActiveSkinID = id;
    public void AddCoins(int amount)
    {
        _data.Coins += amount;
        CoinsValueChanged?.Invoke();
    }
    public void ReduceCoins(int amount)
    {
        _data.Coins -= amount;
        CoinsValueChanged?.Invoke();
    }
    public void AddTime(int seconds) => _data.TimeSeconds += seconds;
    public void IncreaseSpeed(int amount) => _data.Speed += amount;
    public void SetLanguage(string lang) => _data.Language = lang;
    public void AddSkin(int id)
    {
        if (_data.PurchasedSkinsId.Contains(id)) return;
        int new_size = _data.PurchasedSkinsId.Length + 1;
        int[] new_list = new int[new_size];

        for(int i = 0; i < _data.PurchasedSkinsId.Length; i++)
        {
            new_list[i] = _data.PurchasedSkinsId[i];
        }
        new_list[new_size - 1] = id;
        _data.PurchasedSkinsId = new_list;
    }

    public void SavePlayerData()
    {
#if UNITY_EDITOR
        print("Данные сохраняются");
#else
        YaPlayer.SetPlayerData(JsonUtility.ToJson(_data));
#endif
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
#if UNITY_EDITOR
        _data = new PlayerData(0, 3, 60, 6, new int[] { 6 }, "en");
        DataLoaded?.Invoke();
        _isDataReady = true;
#elif UNITY_WEBGL
        if (_yaPlayer.IsYaPlayerInitialized)
            YaPlayer_PlayerInitialized();
        else 
            YaPlayer.PlayerInitialized += YaPlayer_PlayerInitialized;
        YaPlayer.PlayerGotData += YaPlayer_PlayerGotData;
#endif
    }

    private void YaPlayer_PlayerGotData(string obj)
    {
        if (obj == "NullValue")
        {
            // 6 - is ID for default skin
            _data = new PlayerData(0, 3, 60, 6,new int[] { 6 }, YaSDK.GetEnvironment().i18n.lang);
            YaPlayer.SetPlayerData(JsonUtility.ToJson(_data));
        }
        else
        {
            _data = JsonUtility.FromJson<PlayerData>(obj);
        }
        DataLoaded?.Invoke();
        _isDataReady = true;

    }

    private void YaPlayer_PlayerInitialized()
    {
        YaPlayer.GetPlayerData();
    }
}
