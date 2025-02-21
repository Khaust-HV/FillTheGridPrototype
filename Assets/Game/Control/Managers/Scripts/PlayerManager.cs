using UnityEngine;
using Zenject;

namespace Managers {
    public sealed class PlayerManager : IControlTheLastAction {
        #region Last Action Data
            private bool _isAbilityToReturn;
            private Vector3 _direction;
            private bool _isCoinTaked;
        #endregion

        #region Coin Count Data
            private int _currentCoinCountData;
        #endregion

        #region DI
            private IControlTheLevel _iControlTheLevel;
            private IControlMoveTheBall _iControlMoveTheBall;
        #endregion

        [Inject]
        private void Construct(IControlTheLevel iControlTheLevel, IControlMoveTheBall iControlMoveTheBall) {
            // Set DI
            _iControlTheLevel = iControlTheLevel;
            _iControlMoveTheBall = iControlMoveTheBall;
        }

        public void SetNewActionData(Vector3 direction, bool isCoinTaked) {
            _direction = direction;
            _isCoinTaked = isCoinTaked;
            _isAbilityToReturn = true;

            /* Save data */
        }

        public bool BackBeforeAction() {
            if (!_isAbilityToReturn || _iControlMoveTheBall.IsBallMoveing()) return false;

            _isAbilityToReturn = false;

            _iControlTheLevel.ReturnToDefaultTheCell(_iControlMoveTheBall.GetTransformBall().position, _isCoinTaked);

            _iControlMoveTheBall.MoveBallToNewPosition(_direction * -1, true);

            return true;
        }
    }   
}

public interface IControlTheLastAction {
    public void SetNewActionData(Vector3 direction, bool isCoinTaked);
    public bool BackBeforeAction();
}