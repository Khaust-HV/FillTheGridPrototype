using System;
using UnityEngine;
using Zenject;

namespace Managers {
    public sealed class InputManager : IControlGameplayInput, IDisposable {
        private TouchscreenInputController _touchscreenInputController;

        private bool _isDoubleInputActive;

        private Vector2 _singleInputStartPosition;

        #region DI
            private IControlMoveTheBall _iControlMoveTheBall;
            private IControlZoomTheCamera _iControlZoomTheCamera;
        #endregion

        [Inject]
        private void Construct(IControlMoveTheBall iControlMoveTheBall, IControlZoomTheCamera iControlZoomTheCamera) {
            // Set DI
            _iControlMoveTheBall = iControlMoveTheBall;
            _iControlZoomTheCamera = iControlZoomTheCamera;

            // Set other fields
            _touchscreenInputController = new TouchscreenInputController(new InputMap());
        }

        public void Dispose() {
            SetAllGameplayActive(false);
            SetGameplayInputActionMapActive(false);
        }

        public void SetGameplayInputActionMapActive(bool isActive) {
            _touchscreenInputController.SetTouchscreenInputActive(isActive);
        }

        public void SetAllGameplayActive(bool isActive) {
            if (isActive) {
                _touchscreenInputController.SingleTouchStartPosition += SetSingleInputStartPosition;
                _touchscreenInputController.SwipeDelta += SendInputDirectionToController;
                _touchscreenInputController.SecondTouchActive += SetDoubleInputActive;
                _touchscreenInputController.DoublePressPosition += SendDoubleInputDistanceToController;
            } else {
                _touchscreenInputController.SingleTouchStartPosition -= SetSingleInputStartPosition;
                _touchscreenInputController.SwipeDelta -= SendInputDirectionToController;
                _touchscreenInputController.SecondTouchActive -= SetDoubleInputActive;
                _touchscreenInputController.DoublePressPosition -= SendDoubleInputDistanceToController;
            }
        }

        private void SetSingleInputStartPosition(Vector2 position) {
            _singleInputStartPosition = position;
        }

        private void SendInputDirectionToController(Vector2 inputDelay, Vector2 position) {
            if (_isDoubleInputActive || Vector2.Distance(_singleInputStartPosition, position) < 500f) return;

            _singleInputStartPosition = position;

            if (Mathf.Abs(inputDelay.x) > Mathf.Abs(inputDelay.y)) {
                _iControlMoveTheBall.MoveBallToNewPosition(inputDelay.x > 0 ? Vector3.right : Vector3.left);
            } else {
                _iControlMoveTheBall.MoveBallToNewPosition(inputDelay.y > 0 ? Vector3.forward : Vector3.back);
            }
        }

        private void SetDoubleInputActive(bool isActive) {
            _isDoubleInputActive = isActive;
        }

        private void SendDoubleInputDistanceToController(Vector2 firstPosition, Vector2 secondPosition) {
            _iControlZoomTheCamera.SetZoomingActive(true);

            _iControlZoomTheCamera.SetDoubleInputDistance(Vector2.Distance(firstPosition, secondPosition));
        }
    }
}

public interface IControlGameplayInput {
    public void SetGameplayInputActionMapActive(bool isActive);
    public void SetAllGameplayActive(bool isActive);
}