using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action TimerComplited;
    public event Action<int> TimerTick;
    public int Seconds => _seconds;
    public bool IsPause() => _isPause;

    private int _seconds;
    private bool _isPause = false;

    public void StartTimer(int seconds)
    {
        _seconds= seconds;
        StartCoroutine(TimerSecondTick());
    }

    public void SetPause(bool pause) => _isPause = pause;

    private IEnumerator TimerSecondTick()
    {
        while(_seconds > 0)
        {
            yield return new WaitForSeconds(1);
            if (_isPause == false)
            {
                _seconds -= 1;
                TimerTick?.Invoke(_seconds);
            }
            
        }
        TimerComplited?.Invoke();
    }
}
