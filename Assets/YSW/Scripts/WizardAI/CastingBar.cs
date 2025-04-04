using UnityEngine;
using UnityEngine.UI;

public class CastingBar : MonoBehaviour
{
    private Slider _slider;

    private Canvas _canvas; // 부모 캔버스 참조

    void Awake()
    {
       
        // 부모 캔버스 찾기
        _canvas = GetComponentInParent<Canvas>();

        Hide();
    }

    void Start()
    {

        
    }

    public void UpdateCastingBar(float progress)
    {
        if (_slider != null)
        {
            _slider.value = progress;
            //Debug.Log($"슬라이더 값 업데이트: {_slider.value}");
            Show();
        }
        else
        {
            //Debug.LogWarning("Slider가 null입니다!");
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