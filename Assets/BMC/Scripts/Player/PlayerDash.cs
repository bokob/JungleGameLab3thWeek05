using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    Rigidbody2D _rb;
    float _dashSpeed = 10f;
    bool _isDashing = false;
    public bool IsDashing => _isDashing;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Manager.Input.dashAction += Dash;
    }

    IEnumerator DashCoroutine(Vector2 direction)
    {
        _isDashing = true;
        _rb.linearVelocity = direction * _dashSpeed;
        yield return new WaitForSeconds(0.5f);      // 대시 쿨타임
        _isDashing = false;
    }

    public void Dash(Vector2 direction)
    {
        StartCoroutine(DashCoroutine(direction));
        //_rb.linearVelocity = direction * _dashSpeed;
    }
}