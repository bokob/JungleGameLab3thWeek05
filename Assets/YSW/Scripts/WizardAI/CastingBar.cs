using UnityEngine;
using UnityEngine.UI;

public class CastingBar : MonoBehaviour
{
    private Slider _slider;

    void Awake()
    {
        _slider = GetComponent<Slider>();
        if (_slider == null)
        {
            Debug.LogError("CastingBar에 Slider 컴포넌트가 없습니다!");
        }
        Hide();
    }

    public void UpdateCastingBar(float progress)
    {
        if (_slider != null)
        {
            _slider.value = progress;
            Debug.Log($"슬라이더 값 업데이트: {_slider.value}");
            Show();
        }
        else
        {
            Debug.LogWarning("Slider가 null입니다!");
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}