using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    Rigidbody2D _rb;
    float _dashSpeed = 12.5f;
    public bool IsDashing => _isDashing;
    bool _isDashing = false;
    float _dashCoolTime = 0.75f;

    Silhouette _silhouette;
    Coroutine _dashCoroutine;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _silhouette = GetComponent<Silhouette>();
        Manager.Input.dashAction += Dash;
    }

    IEnumerator DashCoroutine(Vector2 direction)
    {
        _isDashing = true;
        _silhouette.IsActive = true;                        // 대시 중 실루엣 활성화
        _rb.linearVelocity = direction * _dashSpeed;
        Camera.main.GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(_dashCoolTime);     // 대시 쿨타임
        _isDashing = false;
        _silhouette.IsActive = false;                        // 대시 중 실루엣 비활성화
        _dashCoroutine = null;
    }

    public void Dash(Vector2 direction)
    {
        if(_dashCoroutine == null)
        {
            _dashCoroutine = StartCoroutine(DashCoroutine(direction));
        }
        //_rb.linearVelocity = direction * _dashSpeed;
    }
}