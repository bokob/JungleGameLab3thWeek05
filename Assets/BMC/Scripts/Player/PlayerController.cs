using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMove _playerMove;
    PlayerDash _playerDash;

    void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
        _playerDash = GetComponent<PlayerDash>();
    }

    void FixedUpdate()
    {
        if (_playerMove == null)
            return;

        _playerMove.Move();
    }
}