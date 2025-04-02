using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    InputSystemActions _inputSystemActions;

    InputAction _moveInputAction;

    Vector2 _moveInput;
    public Vector2 MoveInput => _moveInput;

    public void Init()
    {
        _inputSystemActions = new InputSystemActions();
        _inputSystemActions.Enable();

        _moveInputAction = _inputSystemActions.Player.Move;

        _moveInputAction.performed += OnMove;
    }

    void OnMove(InputAction.CallbackContext context)
    {

    }

    void OnDash(InputAction.CallbackContext context)
    {

    }

    void OnAttack(InputAction.CallbackContext context)
    {

    }

    public void Clear()
    {
        _moveInputAction.Disable();

        _moveInputAction = null;

        _inputSystemActions.Disable();
        _inputSystemActions = null;
    }
}