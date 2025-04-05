using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider hpbar;

    private Status _status; // Status 컴포넌트 참조

    void Start()
    {
        // Status 컴포넌트 찾기
        _status = GetComponentInParent<Status>();
        

        // 초기 HP 바 설정
        UpdateHealthBar();
    }

    void Update()
    {
        if (_status != null)
        {
            UpdateHealthBar();
        }
    }

    private void UpdateHealthBar()
    {
        if (hpbar != null && _status != null)
        {
            hpbar.value = _status.HP / _status.MaxHP; // Status의 HP와 MaxHP로 슬라이더 값 계산
        }
    }
}