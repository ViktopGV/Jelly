using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


[RequireComponent(typeof(YaSDK))]
public class YaLeaderboard : MonoBehaviour
{
    public static event Action LeaderboardInitialized;
    public static event Action<Leaderboard> GotLeaderboardDescription;
    public static event Action<PlayerEntry> GotLeaderboardPlayerEntry;
    public static event Action<LeaderboardEntries> GotLeaderboardEntries;

    [DllImport("__Internal")]
    public static extern void LeaderboardInitialize();
    [DllImport("__Internal")]
    public static extern void GetLeaderboardDescription(string leaderboard);
    [DllImport("__Internal")]
    public static extern void SetLeaderboardScore(string leaderboard, int score, string extraData = "NullExtraData");
    [DllImport("__Internal")]
    public static extern void GetLeaderboardPlayerEntry(string leaderboard);
    [DllImport("__Internal")]
    public static extern void GetLeaderboardEntries(string leaderboard, int quantityTop = 5, int quantityAround = 5, bool includeUser = true);

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
        LeaderboardInitialize();
    }

    private void LeaderboardInitializedCallback()
    {
        LeaderboardInitialized?.Invoke();
    }

    private void GetLeaderboardDescriptionCallback(string result)
    {
        Leaderboard description = JsonUtility.FromJson<Leaderboard>(result);
        GotLeaderboardDescription?.Invoke(description);
    }

    private void GetLeaderboardPlayerEntryCallback(string result)
    {
        PlayerEntry player = JsonUtility.FromJson<PlayerEntry>(result);
        GotLeaderboardPlayerEntry?.Invoke(player);
    }

    private void GetLeaderboardEntriesCallback(string result)
    {
        LeaderboardEntries entries = JsonUtility.FromJson<LeaderboardEntries>(result);
        GotLeaderboardEntries?.Invoke(entries);
    }
}

[Serializable]
public struct Description
{
    public bool invert_sort_order;
    public ScoreFormat score_format;
}
[Serializable]
public struct Options
{
    public int decimal_offset;
}
[Serializable]
public struct Leaderboard
{
    public int appID;
    public string name;
    public bool @default;
    public Description description;
    public Title title;
}
[Serializable]
public struct ScoreFormat
{
    public string type;
    public Options options;
}
[Serializable]
public struct Title
{
    public string en;
    public string ru;
    public string be;
    public string uk;
    public string kk;
    public string uz;
    public string tr;
}

///PlayerEntry
[Serializable]
public struct Player
{
    public string avatarSrc;
    public string avatarSrcSet;
    public string lang;
    public string publicName;
    public ScopePermissions scopePermissions;
    public string uniqueID;
}
[Serializable]
public struct PlayerEntry
{
    public int score;
    public string extraData;
    public int rank;
    public Player player;
    public string formattedScore;
}
[Serializable]
public struct ScopePermissions
{
    public string avatar;
    public string public_name;
}

//Entry
[Serializable]
public struct Entry
{
    public int score;
    public string extraData;
    public int rank;
    public Player player;
    public string formattedScore;
}
[Serializable]
public struct Range
{
    public int start;
    public int size;
}
[Serializable]
public struct LeaderboardEntries
{
    public List<Entry> entries;
    public Leaderboard leaderboard;
    public List<Range> ranges;
    public int userRank;
}
