using System;
using UnityEngine;

public class LocalizationLoader : MonoBehaviour
{
    public bool IsLanguageLoaded = false;
    public event Action LanguageChanged;
    public LanguageDictionary Dict => _dict;
    [SerializeField] private DataHandler _data;
    private LanguageDictionary _dict;

    public void LoadRULanguage() => LoadLocalization("RU");
    public void LoadENLanguage() => LoadLocalization("EN");

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        if(_data.IsDataReady)
        {
            _data_DataLoaded();
        }
        _data.DataLoaded += _data_DataLoaded;
    }

    private void OnDisable()
    {
        _data.DataLoaded -= _data_DataLoaded;

    }

    private void _data_DataLoaded()
    {
        LoadLocalization(_data.PlayerData.Language.ToUpper());
    }

    private void LoadLocalization(string lang)
    {
        _dict = Resources.Load<LanguageDictionary>(lang);
        LanguageChanged?.Invoke();
        IsLanguageLoaded= true;
        _data.SetLanguage(lang);
        _data.SavePlayerData();
    }
}
