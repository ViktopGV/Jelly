using UnityEngine;
using UnityEngine.UI;

public class ProgressView : MonoBehaviour
{
    [SerializeField] private Image _progressImage;
    [SerializeField] private ProgressController _controller;

    private void OnEnable()
    {
        _controller.ProgressChanged += _controller_ProgressChanged;
    }

    private void _controller_ProgressChanged(float obj)
    {
        _progressImage.fillAmount = obj;
    }

    private void OnDisable()
    {
        _controller.ProgressChanged -= _controller_ProgressChanged;
    }

}
