using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private LocalizationLoader _loader;

    private void OnEnable()
    {
        if (_loader.IsLanguageLoaded)
            _dataHandler_DataLoaded();
        else 
            _loader.LanguageChanged += _dataHandler_DataLoaded;
    }

    private void _dataHandler_DataLoaded()
    {
        SceneManager.LoadScene(1);
        YaSDK.LoadingApiReady();
    }

    private void OnDisable()
    {
        _loader.LanguageChanged -= _dataHandler_DataLoaded;
    }
}
