using TMPro;
using UnityEngine;

public class CoinsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    private DataHandler _data;

    private void Awake()
    {
        _data = FindObjectOfType<DataHandler>();
    }

    private void OnEnable()
    {
        _coinsText.text = _data.PlayerData.Coins.ToString();
        _data.CoinsValueChanged += _data_CoinsValueChanged;
    }

    private void OnDisable()
    {
        _data.CoinsValueChanged -= _data_CoinsValueChanged;

    }

    private void _data_CoinsValueChanged()
    {
        _coinsText.text = _data.PlayerData.Coins.ToString();
    }
}
