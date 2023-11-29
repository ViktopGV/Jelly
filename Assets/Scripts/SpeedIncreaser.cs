using System;
using TMPro;
using UnityEngine;


public class SpeedIncreaser : MonoBehaviour
{
    public event Action PriceUpdated;

    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private int _startPrice;
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
        if(obj == 1)
            Improve(true);
    }

    private void OnDisable()
    {
        _data.CoinsValueChanged -= UpdatePriceView;
        YaAdv.AdRewarded -= SuccessfullyRewarded;

    }

    public int GetPrice() => _data.PlayerData.Speed * _startPrice;

    public bool HasMoney()
    {
        int price = GetPrice();
        if (_data.PlayerData.Coins >= price) return true;
        else return false;
    }

    public void UpdatePriceView()
    {
        if(HasMoney())
        {
            _freeContainer.SetActive(false);
            _priceContainer.SetActive(true);
            _priceText.text = (_data.PlayerData.Speed * _startPrice).ToString();
        }
        else
        {
            _freeContainer.SetActive(true);
            _priceContainer.SetActive(false);
        }
        PriceUpdated?.Invoke();
    }

    public void Improve(bool isReward)
    {        
        if(isReward)
        {
            _data.IncreaseSpeed(1);
            _data.SavePlayerData();
            UpdatePriceView();
            _playerMovement.Speed = _data.PlayerData.Speed;
        }
        if (HasMoney())
        {
            int price = _data.PlayerData.Speed * _startPrice;
            _data.ReduceCoins(price);
            _data.IncreaseSpeed(1);
            _data.SavePlayerData();
            UpdatePriceView();
            _playerMovement.Speed = _data.PlayerData.Speed;
        }
        else
        {
            YaAdv.ShowRewardAd(1);
        }
    }
}
