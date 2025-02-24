using System.Collections.Generic;
using CellControllers;
using GameConfigs;
using UnityEngine;
using Zenject;

namespace Managers {
    public sealed class LevelManager : IControlTheLevel {
        #region Level Configs
            private LayerMask _cellLayer;
            private float _rayCheckCellDistance;
        #endregion

        #region Level Data
            private List<int> _paintedCellsIndexList = new();
            private List<int> _coinsOnCellsIndexList = new();
            private int _coinNumberOnLevel;
            private int _numberOfCoinsCollected;
        #endregion

        private IControlInteractionTheCell[] _iControlInteractionTheCells;

        #region DI
            private LevelConfigs _levelConfigs;
            private IControlTheLastAction _iControlTheLastAction;
            private IControlBlackWindowView _iControlBlackWindowView;
        #endregion

        [Inject]
        private void Construct (
            LevelConfigs levelConfigs, 
            IControlTheLastAction iControlTheLastAction,
            IControlBlackWindowView iControlBlackWindowView
            ) {
            // Set DI
            _levelConfigs = levelConfigs;
            _iControlTheLastAction = iControlTheLastAction;
            _iControlBlackWindowView = iControlBlackWindowView;

            // Set configurations
            _cellLayer = _levelConfigs.CellLayer;
            _rayCheckCellDistance = _levelConfigs.RayCheckCellDistance;
        }

        public bool IsMovementPossible(Vector3 position) {
            Ray ray = new Ray(position, Vector3.down);

            if (Physics.Raycast (
                ray, out RaycastHit hit, _rayCheckCellDistance, _cellLayer) &&
                !hit.collider.GetComponent<CellInteractionController>().DoesThisCellColored()
            ) return true;

            return false;
        }

        public bool DoesThisCellHaveCoin(Vector3 position) {
            Ray ray = new Ray(position, Vector3.down);

            if (Physics.Raycast (
                ray, 
                out RaycastHit hit, 
                _rayCheckCellDistance, 
                _cellLayer
            )) return hit.collider.GetComponent<CellInteractionController>().DoesThisCellHaveCoin();

            return false;
        }

        public void ColorTheCell(Vector3 position) {
            Ray ray = new Ray(position, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, _rayCheckCellDistance, _cellLayer)) {
                IControlInteractionTheCell cell = hit.collider.GetComponent<CellInteractionController>();
                cell.SetCellColoredActive(true);

                if (cell.DoesThisCellHaveCoin()) {
                    cell.SetCoinActive(false);

                    _numberOfCoinsCollected++;
                }

                SaveLevelProgress();

                LevelComplete();
            }
        }

        private void LevelComplete() {
            if (_paintedCellsIndexList.Count >= _iControlInteractionTheCells.Length) {
                Debug.Log("Level complete!");

                PlayerData playerData = SaveAndLoadController.LoadPlayerData();

                playerData = new PlayerData (
                    playerData.numberOfCoinsPlayer + _numberOfCoinsCollected,
                    playerData.currentBallSkinType,
                    playerData.availableSkins
                );

                Debug.Log(playerData.numberOfCoinsPlayer);

                SaveAndLoadController.Save(playerData);

                _iControlBlackWindowView.SetBlackWindowActive(true, LoadNextLevel);
            }
        }

        private void LoadNextLevel() {
            LevelReset();

            SceneLoadController.LoadScene(_levelConfigs.NextLevel);
        }

        private void SaveLevelProgress() {
            _paintedCellsIndexList.Clear();
            _coinsOnCellsIndexList.Clear();

            for (int i = 0; i < _iControlInteractionTheCells.Length; i++) {
                if (_iControlInteractionTheCells[i].DoesThisCellColored()) _paintedCellsIndexList.Add(i);

                if (_iControlInteractionTheCells[i].DoesThisCellHaveCoin()) _coinsOnCellsIndexList.Add(i);
            }

            _iControlTheLastAction.SaveLevelData (
                _paintedCellsIndexList.ToArray(),
                _coinsOnCellsIndexList.ToArray(),
                _coinNumberOnLevel,
                _numberOfCoinsCollected
            );
        }

        public void ReturnToDefaultTheCell(Vector3 position, bool _returnCoin) {
            Ray ray = new Ray(position, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, _rayCheckCellDistance, _cellLayer)) {
                IControlInteractionTheCell cell = hit.collider.GetComponent<CellInteractionController>();
                cell.SetCellColoredActive(false);

                if (_returnCoin) {
                    cell.SetCoinActive(true);

                    _numberOfCoinsCollected--;
                }
            }
        }

        public void SetCells(IControlInteractionTheCell[] iControlInteractionTheCells) {
            _iControlInteractionTheCells = iControlInteractionTheCells;
        }

        public void SetLevelData(LevelData levelData) {
            foreach (var paintedCellIndex in levelData.paintedCellsIndexStorage) {
                _iControlInteractionTheCells[paintedCellIndex].SetCellColoredActive(true);
            }

            foreach (var coinOnCellIndex in levelData.coinsOnCellsIndexStorage) {
                _iControlInteractionTheCells[coinOnCellIndex].SetCoinActive(true);
            }

            _coinNumberOnLevel = levelData.coinNumberOnLevel;
            _numberOfCoinsCollected = levelData.numberOfCoinsCollected;

            _iControlTheLastAction.SetLevelData(levelData);
        }

        public void LevelReset() {
            foreach (var cell in _iControlInteractionTheCells) {
                cell.SetCellColoredActive(false);
                cell.SetCoinActive(false);
            }

            int coinNumber = _levelConfigs.CoinNumberOnLevel;

            _coinNumberOnLevel = coinNumber;
            _numberOfCoinsCollected = 0;

            while (coinNumber > 0) {
                var cell = _iControlInteractionTheCells[Random.Range(0, _iControlInteractionTheCells.Length)];

                if (cell.DoesThisCellHaveCoin()) continue;

                cell.SetCoinActive(true);

                coinNumber--;
            }

            _iControlTheLastAction.LastActionReset();

            SaveLevelProgress();
        }

        public void ShowCells() {
            foreach (var cell in _iControlInteractionTheCells) {
                cell.SpawnEffectEnable();
            }
        }
    }
}

public interface IControlTheLevel {
    public bool IsMovementPossible(Vector3 position);
    public bool DoesThisCellHaveCoin(Vector3 position);
    public void ColorTheCell(Vector3 position);
    public void ReturnToDefaultTheCell(Vector3 position, bool _returnCoin);
    public void SetCells(IControlInteractionTheCell[] iControlInteractionTheCells);
    public void SetLevelData(LevelData levelData);
    public void ShowCells();
    public void LevelReset();
}