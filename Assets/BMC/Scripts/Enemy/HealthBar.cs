using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider hpbar;
    private Status _status; // Status 컴포넌트 참조
    private Canvas _canvas; // 부모 캔버스 참조
    GameObject target;
    void Start()
    {
        _status = GetComponentInParent<Status>(); // Status 컴포넌트 찾기
        _canvas = GetComponentInParent<Canvas>(); // 부모 캔버스 찾기

        UpdateHealthBar(); // 초기 HP 바 설정

        target = transform.parent.gameObject;
    }

    void Update()
    {
        if (_status != null)
        {
            UpdateHealthBar();
        }

        // 체력이 0 이하일 때 슬라이더 비활성화
        if (_status.HP <= 0)
        {
            _canvas.enabled = false;
        }

        transform.position = target.transform.position;
    }

    void UpdateHealthBar()
    {
        if (hpbar != null && _status != null)
        {
            hpbar.value = _status.HP / _status.MaxHP; // Status의 HP와 MaxHP로 슬라이더 값 계산
        }
    }
}