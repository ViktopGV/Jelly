using TMPro;
using UnityEngine;


[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedText : MonoBehaviour
{
    [SerializeField] private string _key;
    [SerializeField] private string _additionalText;
    private TextMeshProUGUI _text;
    private LocalizationLoader _langLoader;

    public void UpdateText()
    {        
        _text.text = _langLoader.Dict[_key] + _additionalText;
    }

    private void Awake()
    {
        _langLoader = FindObjectOfType<LocalizationLoader>();
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateText();
    }

    private void OnEnable()
    {
        _langLoader.LanguageChanged += UpdateText;
        
    }

    private void OnDisable()
    {
        _langLoader.LanguageChanged -= UpdateText;
    }
}
