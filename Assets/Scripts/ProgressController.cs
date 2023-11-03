using System;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    public event Action<float> ProgressChanged;
    [SerializeField] private Eating _eating;

    private int _eatableObjectsCount;
    private float _procent = 0;
    private float _onePiece;

    private void Awake()
    {
        _eatableObjectsCount = FindObjectsByType<Eaten>(FindObjectsSortMode.None).Length;
        _onePiece = 1f / _eatableObjectsCount;
    }

    private void OnEnable()
    {
        _eating.Ate += _eating_Ate;
    }

    private void OnDisable()
    {
        _eating.Ate -= _eating_Ate;
    }
    private void _eating_Ate()
    {
        _procent += _onePiece;
        ProgressChanged?.Invoke(_procent);
    }
}
