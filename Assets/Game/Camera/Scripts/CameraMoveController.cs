using System.Collections;
using GameConfigs;
using UnityEngine;
using Zenject;

namespace CameraControllers {
    public sealed class CameraMoveController : MonoBehaviour, IControlMoveTheCamera {
        private Transform _trTarget;

        private CameraMoveState _cameraMoveState;

        private bool isMoveActive;
        private IEnumerator moveStarted;

        #region DI
            private CameraConfigs _cameraConfigs;
        #endregion

        [Inject]
        private void Construct(CameraConfigs cameraConfigs) {
            // Set DI
            _cameraConfigs = cameraConfigs;
        }

        public void SetLookedTarget(Transform trTarget) {
            _trTarget = trTarget;
        }

        public void SetMoveState(CameraMoveState cameraMoveState) {
            _cameraMoveState = cameraMoveState;
        }

        public void SetMovingActive(bool isActive) {
            if (isActive) if (!isMoveActive) StartCoroutine(moveStarted = MoveStarted());
            else if (isMoveActive) {
                StopCoroutine(moveStarted);

                isMoveActive = false;
            }
        }

        private IEnumerator MoveStarted() {
            isMoveActive = true;
            
            float movingSmoothSpeed = _cameraConfigs.MovingSmoothSpeed;

            while (true) {
                switch (_cameraMoveState) {
                    case CameraMoveState.MovingAfterTheTarget:
                        var stepPosition = Vector3.Lerp(transform.position, _trTarget.position, movingSmoothSpeed);

                        var positionConsideringHeight = new Vector3(stepPosition.x, transform.position.y, stepPosition.z);

                        transform.position = positionConsideringHeight;
                    break;
                }

                yield return null;
            }
        }
    }
}

public interface IControlMoveTheCamera {
    public void SetLookedTarget(Transform trTarget);
    public void SetMoveState(CameraMoveState cameraMoveState);
    public void SetMovingActive(bool isActive);
}

public enum CameraMoveState {
    Idle,
    MovingAfterTheTarget,
}