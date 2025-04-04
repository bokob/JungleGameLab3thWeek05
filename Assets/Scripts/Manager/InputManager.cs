using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    InputSystemActions _inputSystemActions;

    #region InputAction
    InputAction _moveInputAction;
    InputAction _pointerMoveAction;
    InputAction _attackInputAction;
    InputAction _dashInputAction;
    #endregion

    #region 입력값
    public Vector2 MoveInput => _moveInput;
    public Vector2 PointerMoveInput => _pointerMoveInput;
    public bool AttackInput => _attackInput;
    public bool DashInput => _dashInput;

    Vector2 _moveInput;
    Vector2 _pointerMoveInput;
    bool _attackInput;
    bool _dashInput;
    #endregion

    #region Action
    public Action attackAction;
    public Action<Vector2> dashAction;
    #endregion

    public void Init()
    {
        _inputSystemActions = new InputSystemActions();
        _inputSystemActions.Enable();

        _moveInputAction = _inputSystemActions.Player.Move;
        _pointerMoveAction = _inputSystemActions.Player.PointerMove;
        _attackInputAction = _inputSystemActions.Player.Attack;
        _dashInputAction = _inputSystemActions.Player.Dash;

        _moveInputAction.performed += OnMove;
        _moveInputAction.canceled += OnMove;
        _pointerMoveAction.performed += OnPointerMove;
        _attackInputAction.performed += OnAttack;
        _attackInputAction.canceled += OnAttack;
        _dashInputAction.performed += OnDash;
        _dashInputAction.canceled += OnDash;
    }

    void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            _moveInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _moveInput = Vector2.zero;
        }
    }

    void OnPointerMove(InputAction.CallbackContext context)
    {
        Vector3 mousePos = context.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;         // 스크린 좌표에서 카메라까지의 거리인 nearClipPlane을 z축으로 설정
        _pointerMoveInput = Camera.main.ScreenToWorldPoint(mousePos);
    }

    void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _attackInput = true;
            attackAction.Invoke();
        }
        else if (context.canceled)
        {
            _attackInput = false;
        }
    }

    void OnDash(InputAction.CallbackContext context)
    {
        Debug.Log("입력이 왜 안들어올까?");

        if(context.performed)
        {
            _dashInput = true;
            dashAction.Invoke(_moveInput);
            Debug.LogError("플레이어 대시");
        }
        else if(context.canceled)
        {
            _dashInput = false;
            Debug.LogError("대시 취소");
        }
    }

    public void Clear()
    {
        _moveInputAction.Disable();
        _pointerMoveAction.Disable();
        _attackInputAction.Disable();
        _dashInputAction.Disable();

        _moveInputAction = null;
        _pointerMoveAction = null;
        _attackInputAction = null;
        _dashInputAction = null;

        _inputSystemActions.Disable();
        _inputSystemActions = null;
    }
}