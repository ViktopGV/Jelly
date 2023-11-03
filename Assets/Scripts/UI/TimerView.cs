using System;
using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        UpdateText();
        _timer.TimerTick += _timer_TimerTick;
    }

    public void UpdateText() => _text.text = TimeSpan.FromSeconds(_timer.Seconds).ToString(@"m\:ss");

    private void _timer_TimerTick(int obj)
    {
        UpdateText();
    }

    private void OnDisable()
    {
        _timer.TimerTick -= _timer_TimerTick;
    }
}
