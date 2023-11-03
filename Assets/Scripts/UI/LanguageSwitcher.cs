using UnityEngine;
using UnityEngine.UI;

public class LanguageSwitcher : MonoBehaviour
{
    [SerializeField] private Image _langImage;
    [SerializeField] private Sprite _ruIcon;
    [SerializeField] private Sprite _enIcon;
    private LocalizationLoader _loader;
    private DataHandler _data;

    private void Awake()
    {
        _loader = FindObjectOfType<LocalizationLoader>();
        _data = FindObjectOfType<DataHandler>();

        if (_data.PlayerData.Language.ToLower() == "ru")
            LoadRU();
        else LoadEN();
    }



    public void SwitchLanguage()
    {
        if (_data.PlayerData.Language.ToLower() == "ru")
            LoadEN();
        else LoadRU();
    }

    public void LoadRU()
    {
        _langImage.sprite = _ruIcon;
        _loader.LoadRULanguage();
        _data.SetLanguage("ru");
        _data.SavePlayerData();
    }

    public void LoadEN()
    {
        _langImage.sprite = _enIcon;
        _loader.LoadENLanguage();
        _data.SetLanguage("en");
        _data.SavePlayerData();
    }
}
