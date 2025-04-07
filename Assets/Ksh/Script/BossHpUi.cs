using UnityEngine;

using UnityEngine.UI;
public class BossHpUi : MonoBehaviour
{
    [SerializeField]
    private Slider hpbar;
    private Status _status; // Status 컴포넌트 참조
    private Canvas _canvas; // 부모 캔버스 참조

    void Start()
    {
        // Status 컴포넌트 찾기
        _status = GetComponentInParent<Status>();


        // 초기 HP 바 설정
        UpdateHealthBar();

        // 부모 캔버스 찾기
        _canvas = GetComponentInParent<Canvas>();


        transform.parent = Camera.main.transform;
        transform.localPosition = new Vector3(0, 6, 10);
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
    }

    private void UpdateHealthBar()
    {
        if (hpbar != null && _status != null)
        {
            hpbar.value = _status.HP / _status.MaxHP; // Status의 HP와 MaxHP로 슬라이더 값 계산
        }

    }
}

