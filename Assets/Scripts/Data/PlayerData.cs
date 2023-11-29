using System;

[Serializable]
public struct PlayerData
{
    public int Coins;
    public int Speed;
    public int TimeSeconds;
    public int ActiveSkinID;
    public int[] PurchasedSkinsId;
    public string Language;
    public bool TutorialPassed;

    public PlayerData(int coins, int speed, int seconds, int activeSkinId, int[] skins, string lang, bool tutorial)
    {
        Coins = coins;
        Speed = speed;
        TimeSeconds = seconds;
        ActiveSkinID = activeSkinId;
        PurchasedSkinsId = skins;
        Language = lang;
        TutorialPassed = tutorial;
    }
}