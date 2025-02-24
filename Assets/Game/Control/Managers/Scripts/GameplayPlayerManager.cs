using GameConfigs;
using UnityEngine;
using Zenject;

namespace Managers {
    public sealed class GameplayPlayerManager : IControlTheLastAction {
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
            private LevelConfigs _levelConfigs;
        #endregion

        [Inject]
        private void Construct (
            IControlTheLevel iControlTheLevel, 
            IControlMoveTheBall iControlMoveTheBall,
            LevelConfigs levelConfigs
            ) {
            // Set DI
            _iControlTheLevel = iControlTheLevel;
            _iControlMoveTheBall = iControlMoveTheBall;
            _levelConfigs = levelConfigs;
        }

        public void SetNewActionData(Vector3 direction, bool isCoinTaked) {
            _direction = direction;
            _isCoinTaked = isCoinTaked;
            _isAbilityToReturn = true;
        }

        public bool BackBeforeAction() {
            if (!_isAbilityToReturn || _iControlMoveTheBall.IsBallMoveing()) return false;

            _isAbilityToReturn = false;

            _iControlTheLevel.ReturnToDefaultTheCell(_iControlMoveTheBall.GetTransformBall().position, _isCoinTaked);

            _iControlMoveTheBall.MoveBallToNewPosition(_direction * -1, true);

            return true;
        }

        public void SaveLevelData (
            int[] paintedCellsIndexStorage, 
            int[] coinsOnCellsIndexStorage, 
            int coinNumberOnLevel,
            int numberOfCoinsCollected
        ){
            LevelData levelData = new LevelData(
                paintedCellsIndexStorage,
                coinsOnCellsIndexStorage,
                coinNumberOnLevel,
                numberOfCoinsCollected,
                _iControlMoveTheBall.GetTransformBall().position,
                _isAbilityToReturn,
                _direction,
                _isCoinTaked
            );

            SaveAndLoadController.Save(levelData, _levelConfigs.LevelName);
        }

        public void LastActionReset() {
            _isAbilityToReturn = false;

            _iControlMoveTheBall.SetStartPosition(_levelConfigs.BallStartPosition);
        }

        public void SetLevelData(LevelData levelData) {
            _isAbilityToReturn = levelData.isAbilityToReturn;
            _direction = levelData.direction;
            _isCoinTaked = levelData.isCoinTaked;

            _iControlMoveTheBall.SetStartPosition(levelData.ballPosition);
        }
    }   
}

public interface IControlTheLastAction {
    public void SetNewActionData(Vector3 direction, bool isCoinTaked);
    public bool BackBeforeAction();
    public void SaveLevelData (
        int[] paintedCellsIndexStorage, 
        int[] coinsOnCellsIndexStorage, 
        int coinNumberOnLevel,
        int numberOfCoinsCollected
    );
    public void LastActionReset();
    public void SetLevelData(LevelData levelData);
}