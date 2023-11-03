using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadImg : MonoBehaviour
{
    [SerializeField] private Image _img;

    void Update()
    {
        if(_img.fillClockwise == true)
        {
            if (_img.fillAmount < 1)
                _img.fillAmount += Time.deltaTime;
            else
                _img.fillClockwise = false;
        }
        else
        {
            if(_img.fillAmount > 0)
                _img.fillAmount -= Time.deltaTime;
            else
                _img.fillClockwise = true;

        }
    }
}
