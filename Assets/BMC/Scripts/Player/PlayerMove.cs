using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D _rb;
    Animator _anim;
    [SerializeField] float _moveSpeed = 30f;
    [SerializeField] Vector2 _moveDir;

    PlayerDash _playerDash;
    Status _status;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _playerDash = GetComponent<PlayerDash>();
        _status = GetComponent<Status>();
    }

    public void Move()
    {
        if(_playerDash == null || _status.IsDead)
            return;

        _moveDir = Manager.Input.MoveInput.normalized;
        if (!_playerDash.IsDashing)
        {
            if (_moveDir != Vector2.zero)
            {
                _status.Flip(transform, _moveDir.x);
                _rb.linearVelocity = _moveDir * _moveSpeed;
                //_rb.MovePosition(_rb.position + _moveDir * _moveSpeed * Time.fixedDeltaTime);
                _anim.SetBool("IsMove", true);
            }
            else
            {
                //Debug.Log("안움직임");
                _anim.SetBool("IsMove", false);
            }
        }
    }
}