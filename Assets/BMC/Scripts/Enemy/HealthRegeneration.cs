using UnityEngine;
using System.Collections;

public class HealthRegeneration : MonoBehaviour
{
    int _necromancerKillCount = 0; // 네크로맨서 처치 카운트
    Coroutine _regenerationCoroutine; // 현재 실행 중인 재생 코루틴
    Status _playerStatus; // 플레이어의 Status 참조
    float _lastHP; // 마지막으로 확인한 체력 값
    bool _isRegenerationActive = false; // 재생 효과가 활성화 중인지 여부
    [SerializeField] ParticleSystem _healingParticle; // 회복 파티클 (private 선언)
    UI_HealthBarCanvas _healthBarCanvas;

    void Start()
    {
        // 플레이어의 Status 컴포넌트 찾기
        _playerStatus = GetComponent<Status>();
        _lastHP = _playerStatus.HP; // 초기 체력 저장
        _healthBarCanvas = FindAnyObjectByType<UI_HealthBarCanvas>();
    }

    void Update()
    {
        if (_playerStatus == null || _playerStatus.IsDead) return;

        // 체력이 감소했는지 확인
        if (_playerStatus.HP < _lastHP && _playerStatus.HP < _playerStatus.MaxHP && _necromancerKillCount > 0 && !_isRegenerationActive)
        {
            // 체력이 떨어졌고, 재생 효과가 비활성화 상태라면 재시작
            RestartRegeneration();
        }

        _lastHP = _playerStatus.HP; // 현재 체력 갱신
    }

    // 네크로맨서 처치 시 호출
    public void OnNecromancerKilled()
    {
        if (_playerStatus == null) return;

        _necromancerKillCount++;
        //Debug.Log($"네크로맨서 처치! 현재 처치 횟수: {_necromancerKillCount}");

        // 기존 재생 코루틴 중지
        if (_regenerationCoroutine != null)
        {
            StopCoroutine(_regenerationCoroutine);
        }

        // 새로운 재생 효과 시작
        StartRegeneration();
    }

    // 재생 효과 시작
    private void StartRegeneration()
    {
        float amountPerTick = 0f; // 기본 회복량
        if (_necromancerKillCount == 1)
        {
            amountPerTick = 5f; // 2마리 처치 시 회복량 증가
        }
        else if (_necromancerKillCount >= 2)
        {
            amountPerTick = 10; // 3마리 이상 처치 시 최대 회복량
        }
        else if (_necromancerKillCount >= 3)
        {
            amountPerTick = 15; // 3마리 이상 처치 시 최대 회복량
        }
        else if (_necromancerKillCount >= 4)
        {
            amountPerTick = 20; // 3마리 이상 처치 시 최대 회복량
        }

        _regenerationCoroutine = StartCoroutine(RegenerateHP(amountPerTick, 5f));
    }

    // 재생 효과 재시작
    private void RestartRegeneration()
    {
        if (_regenerationCoroutine != null)
        {
            StopCoroutine(_regenerationCoroutine);
        }
        StartRegeneration();
    }

    // 체력 재생 코루틴
    private IEnumerator RegenerateHP(float amountPerTick, float tickInterval)
    {
        _isRegenerationActive = true;
        float elapsedTime = 0f;
        while (true)
        {
            if (_playerStatus == null || _playerStatus.IsDead)
            {
                _isRegenerationActive = false;
                yield break;
            }

            // 체력이 최대치 미만일 때만 회복
            if (_playerStatus.HP < _playerStatus.MaxHP)
            {
                _playerStatus.HP = Mathf.Clamp(_playerStatus.HP + amountPerTick, 0, _playerStatus.MaxHP);
                _healthBarCanvas.SetHealthBar(_playerStatus.HP);
                TriggerPlayHeal();
                Debug.Log($"체력 재생: {amountPerTick} 회복, 현재 HP: {_playerStatus.HP}");
            }

            yield return new WaitForSeconds(tickInterval);
            elapsedTime += tickInterval;
        }
    }
    void TriggerPlayHeal()
    {
        if (_healingParticle != null)
        {
            ParticleSystem heal = Instantiate(_healingParticle, transform.position, Quaternion.identity);
            heal.Play();
            Destroy(heal.gameObject, 2f);
        }
    }
}