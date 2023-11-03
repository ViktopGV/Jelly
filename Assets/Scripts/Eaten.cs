using UnityEngine;

public class Eaten : MonoBehaviour
{
    [SerializeField] private float _value;
    private bool _isAlive = true;
    public float EatenValue => _value;
    public bool IsAlive => _isAlive;
    public Vector3 VectorValue => new Vector3(_value, _value, _value);
    public void Eat() => _isAlive = false;
}
