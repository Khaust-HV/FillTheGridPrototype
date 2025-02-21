using UnityEngine;
using Zenject;

namespace CameraControllers {
    public sealed class CameraController : MonoBehaviour, IControlMoveTheCamera, IControlZoomTheCamera {
        private IControlMoveTheCamera _iControlMoveTheCamera;
        private IControlZoomTheCamera _iControlZoomTheCamera;

        [Inject]
        private void Construct() {
            _iControlMoveTheCamera = GetComponent<CameraMoveController>();
            _iControlZoomTheCamera = GetComponent<CameraZoomController>();
        }

        public void SetLookedTarget(Transform trTarget) {
            _iControlMoveTheCamera.SetLookedTarget(trTarget);
        }

        public void SetMoveState(CameraMoveState cameraMoveState) {
            _iControlMoveTheCamera.SetMoveState(cameraMoveState);
        }

        public void SetMovingActive(bool isActive) {
            _iControlMoveTheCamera.SetMovingActive(isActive);
        }

        public void SetZoomingActive(bool isActive) {
            _iControlZoomTheCamera.SetZoomingActive(isActive);
        }

        public void SetDoubleInputDistance(float distance) {
            _iControlZoomTheCamera.SetDoubleInputDistance(distance);
        }
    }
}