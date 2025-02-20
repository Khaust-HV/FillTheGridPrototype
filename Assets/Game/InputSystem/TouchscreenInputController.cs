using System;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class TouchscreenInputController  {
    private InputMap _inputMap;

    public event Action<Vector2> SingleTouchStartPosition;
    public event Action<Vector2, Vector2> SwipeDelta;
    public event Action<bool> SecondTouchActive;
    public event Action<Vector2, Vector2> DoublePressPosition;

    public TouchscreenInputController(InputMap inputMap) {
        _inputMap = inputMap;
    }

    public void SetTouchscreenInputActive(bool isActive) {
        if (isActive) {
            _inputMap.GameplayActions.FirstTouchContact.started += _ => FirstTouchStarted();
            _inputMap.GameplayActions.Swipe.performed += SwipeOnScreen;
            _inputMap.GameplayActions.SecondTouchContact.started += _ => SecondTouchStarted();
            _inputMap.GameplayActions.SecondTouchContact.canceled += _ => SecondTouchCanceled();
            _inputMap.GameplayActions.SecondTouchPosition.performed += DoublePressPositions;
            _inputMap.Enable();
        } else {
            _inputMap.GameplayActions.FirstTouchContact.started -= _ => FirstTouchStarted();
            _inputMap.GameplayActions.Swipe.performed -= SwipeOnScreen;
            _inputMap.GameplayActions.SecondTouchContact.started -= _ => SecondTouchStarted();
            _inputMap.GameplayActions.SecondTouchContact.canceled -= _ => SecondTouchCanceled();
            _inputMap.GameplayActions.SecondTouchPosition.performed -= DoublePressPositions;
            _inputMap.Disable();
        }
    }

    private void FirstTouchStarted() {
        SingleTouchStartPosition?.Invoke(_inputMap.GameplayActions.FirstTouchPosition.ReadValue<Vector2>());
    }

    private void SwipeOnScreen(InputAction.CallbackContext context) {
        SwipeDelta?.Invoke(context.ReadValue<Vector2>(), _inputMap.GameplayActions.FirstTouchPosition.ReadValue<Vector2>());
    }

    private void SecondTouchStarted() {
        SecondTouchActive?.Invoke(true);
    }

    private void SecondTouchCanceled() {
        SecondTouchActive?.Invoke(false);
    }

    private void DoublePressPositions(InputAction.CallbackContext context) {
        DoublePressPosition?.Invoke(_inputMap.GameplayActions.FirstTouchPosition.ReadValue<Vector2>(), context.ReadValue<Vector2>());
    }
}