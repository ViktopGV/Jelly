using TMPro;
using UnityEngine;

public class TimeIncreaser : MonoBehaviour
{
    [SerializeField] private int _startPrice;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private GameObject _priceContainer;
    [SerializeField] private GameObject _freeContainer;

    private DataHandler _data;

    private void Awake()
    {
        _data = FindObjectOfType<DataHandler>();
    }

    private void OnEnable()
    {
        _data.CoinsValueChanged += UpdatePriceView;
        YaAdv.AdRewarded += SuccessfullyRewarded;
        UpdatePriceView();
    }

    private void SuccessfullyRewarded(int obj)
    {
        Improve(true);
    }

    private void OnDisable()
    {
        _data.CoinsValueChanged -= UpdatePriceView;
        YaAdv.AdRewarded -= SuccessfullyRewarded;

    }
    public bool HasMoney()
    {
        int price = _data.PlayerData.TimeSeconds * _startPrice;
        if (_data.PlayerData.Coins >= price) return true;
        else return false;
    }

    public void UpdatePriceView()
    {
        if (HasMoney())
        {
            _freeContainer.SetActive(false);
            _priceContainer.SetActive(true);
            _priceText.text = (_data.PlayerData.TimeSeconds * _startPrice).ToString();
        }
        else
        {
            _freeContainer.SetActive(true);
            _priceContainer.SetActive(false);
        }
    }

    public void Improve(bool isReward)
    {
        if(isReward)
        {
            _data.AddTime(10);
            _data.SavePlayerData();
            UpdatePriceView();
        }
        if (HasMoney())
        {
            int price = _data.PlayerData.TimeSeconds * _startPrice;
            _data.ReduceCoins(price);
            _data.AddTime(10);
            _data.SavePlayerData();
            UpdatePriceView();
        }
        else
        {
            YaAdv.ShowRewardAd(0);
        }
    }
}
