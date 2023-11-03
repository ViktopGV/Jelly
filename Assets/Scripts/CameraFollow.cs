using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 _offset;
    public Transform _target;
    [SerializeField] private float _smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        Vector3 desiredPosition = _target.position + _offset * _target.localScale.x;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(_target.position);
    }
}
