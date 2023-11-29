using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public event Action TutorialEnd;
    public GameObject TimerTutorialPanel;
    public GameObject TimerTutorialArrow;

    public GameObject MoneyTutorialPanel;
    public GameObject ImproveTutorialPanel;

    public void StartTutorial()
    {
        StartCoroutine(TimerTutorial());
    }

    IEnumerator TimerTutorial()
    {
        TimerTutorialPanel.SetActive(true);
        TimerTutorialArrow.SetActive(true);
        yield return new WaitForSeconds(4);
        TimerTutorialPanel.SetActive(false);
        TimerTutorialArrow.SetActive(false);
        StartCoroutine(MoneyTutorial());

    }

    IEnumerator MoneyTutorial()
    {
        MoneyTutorialPanel.SetActive(true);
        yield return new WaitForSeconds(4);
        MoneyTutorialPanel.SetActive(false);
        StartCoroutine(ImproveTutorial());

    }

    IEnumerator ImproveTutorial()
    {
        ImproveTutorialPanel.SetActive(true);
        yield return new WaitForSeconds(4);
        ImproveTutorialPanel.SetActive(false);
        TutorialEnd?.Invoke();
    }

}
