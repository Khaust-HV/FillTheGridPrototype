using DG.Tweening;
using GameConfigs;
using UnityEngine;
using Zenject;

namespace BallControllers {
    public sealed class BallMoveController : MonoBehaviour, IControlMoveTheBall {
        #region Ball Configs
            private float _stepSize;
            private float _moveDuration;
        #endregion

        private bool _isBallMoveing;
        private float _ballRadius;

        #region DI
            private IControlTheLevel _iControlTheLevel;
            private IControlTheLastAction _iControlTheLastAction;
        #endregion

        [Inject]
        private void Construct (
            IControlTheLevel iControlTheLevel, 
            BallAndCellConfigs ballAndCellConfigs,
            IControlTheLastAction iControlTheLastAction
            ) {
            // Set DI
            _iControlTheLevel = iControlTheLevel;
            _iControlTheLastAction = iControlTheLastAction;

            // Set configurations
            _stepSize = ballAndCellConfigs.StepSize;
            _moveDuration = ballAndCellConfigs.MoveDuration;

            // Set other fields
            _ballRadius = transform.localScale.x / 2;
        }

        public void SetStartPosition(Vector3 position) {
            transform.position = position;

            ColorTheCell();
        }

        public void MoveBallToNewPosition(Vector3 direction, bool isForceMove = false) {
            if (_isBallMoveing) return;

            Vector3 position = transform.position + direction * _stepSize;

            if (!_iControlTheLevel.IsMovementPossible(position) && !isForceMove) return;

            if (!isForceMove) _iControlTheLastAction.SetNewActionData(direction, _iControlTheLevel.DoesThisCellHaveCoin(position));

            _isBallMoveing = true;

            float angle = (_stepSize / (_ballRadius * Mathf.PI)) * 180f;

            Vector3 rotationAxis = Vector3.Cross(Vector3.down, direction);

            transform.DOMove(position, _moveDuration);

            transform.DORotateQuaternion(
                Quaternion.AngleAxis(angle, rotationAxis) * transform.rotation,
                _moveDuration
            ).OnComplete(MoveFinished);
        }

        private void MoveFinished() {
            _isBallMoveing = false;

            ColorTheCell();
        }

        public bool IsBallMoveing() {
            return _isBallMoveing;
        }

        private void ColorTheCell() {
            _iControlTheLevel.ColorTheCell(transform.position);
        }

        public Transform GetTransformBall() {
            return transform;
        }
    }
}

public interface IControlMoveTheBall {
    public void SetStartPosition(Vector3 position);
    public void MoveBallToNewPosition(Vector3 direction, bool isForceMove = false);
    public bool IsBallMoveing();
    public Transform GetTransformBall();
}