using UnityEngine;
using UnityEngine.UI;

public class CastingBar : MonoBehaviour
{
    Slider _slider;
    Canvas _canvas; // 부모 캔버스 참조

    void Awake()
    {       
        // 부모 캔버스 찾기
        _canvas = GetComponentInParent<Canvas>();
        _slider = GetComponent<Slider>();
        Hide();
    }

    public void UpdateCastingBar(float progress)
    {
        if (_slider != null)
        {
            _slider.value = progress;
            //Debug.Log($"슬라이더 값 업데이트: {_slider.value}");
            Show();
        }
    }

    public void Show()
    {
        _canvas.enabled = true;
    }

    public void Hide()
    {
        _canvas.enabled = false;
    }
}