using System;
using UnityEngine;

public class AnyButtonWaiter : MonoBehaviour
{
    public event Action Clicked;
    void Update()
    {
        
        if (Input.anyKeyDown)
        {
            Clicked?.Invoke();
        }
    }
}
