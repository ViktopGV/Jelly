using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PriceView : MonoBehaviour
{
    public SpeedIncreaser SpeedIncreaser;
    public TimeIncreaser TimeIncreaser;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI SpeedText;

    private void OnEnable()
    {
        SpeedIncreaser.PriceUpdated += SetPriceText;
        TimeIncreaser.PriceUpdated += SetPriceText;
    }

    private void SetPriceText()
    {
        SpeedText.text = SpeedIncreaser.GetPrice().ToString();
        TimeText.text = TimeIncreaser.GetPrice().ToString();
    }

    private void OnDisable()
    {
        SpeedIncreaser.PriceUpdated -= SetPriceText;
        TimeIncreaser.PriceUpdated -= SetPriceText;
    }
}
