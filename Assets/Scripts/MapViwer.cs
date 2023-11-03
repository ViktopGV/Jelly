using UnityEngine;

public class MapViwer : MonoBehaviour
{
    [SerializeField] private Transform _mapTarget;
    [SerializeField] private CameraFollow _camera;
    [SerializeField] private Vector3 _offset;

    private Vector3 _oldCameraOffset;
    private Transform _oldTarget;
    private bool _isMapViewMode = false;

    private void Awake()
    {
        _oldCameraOffset = _camera._offset;
        _oldTarget = _camera._target;
    }

    public void SwitchMode()
    {
        if (_isMapViewMode)
            ExitMapViewMode();
        else 
            EnterMapViewMode();
    }

    public void EnterMapViewMode()
    {
        _camera._target = _mapTarget;
        _camera._offset = _offset;
        _isMapViewMode = true;
    }

    public void ExitMapViewMode()
    {
        _camera._target = _oldTarget;
        _camera._offset = _oldCameraOffset;
        _isMapViewMode = false;
    }
}
