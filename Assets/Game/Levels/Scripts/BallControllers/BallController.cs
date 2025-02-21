using UnityEngine;
using Zenject;

namespace BallControllers {
    public sealed class BallController : MonoBehaviour, IControlMoveTheBall, IControlRenderTheBall {
        private IControlMoveTheBall _iControlMoveTheBall;
        private IControlRenderTheBall _iControlRenderTheBall;

        [Inject]
        private void Construct() {
            _iControlMoveTheBall = GetComponent<BallMoveController>();
            _iControlRenderTheBall = GetComponent<BallRenderController>();
        }

        public void SetStartPosition(Vector3 position) {
            _iControlMoveTheBall.SetStartPosition(position);
        }

        public void MoveBallToNewPosition(Vector3 direction, bool isForceMove = false) {
            _iControlMoveTheBall.MoveBallToNewPosition(direction, isForceMove);
        }

        public bool IsBallMoveing() {
            return _iControlMoveTheBall.IsBallMoveing();
        }

        public Transform GetTransformBall() {
            return _iControlMoveTheBall.GetTransformBall();
        }

        public Color GetBallBaseColor() {
            return _iControlRenderTheBall.GetBallBaseColor();
        }

        public void SpawnEffectEnable() {
            _iControlRenderTheBall.SpawnEffectEnable();
        }
    }
}