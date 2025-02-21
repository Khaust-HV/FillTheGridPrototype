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

        IControlInteractionTheCell[] _iControlInteractionTheCells;

        #region DI
            private LevelConfigs _levelConfigs;
        #endregion

        [Inject]
        private void Construct(LevelConfigs levelConfigs) {
            // Set DI
            _levelConfigs = levelConfigs;

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
                    // Save data

                    cell.SetCoinActive(false);
                }
            }
        }

        public void ReturnToDefaultTheCell(Vector3 position, bool _returnCoin) {
            Ray ray = new Ray(position, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, _rayCheckCellDistance, _cellLayer)) {
                IControlInteractionTheCell cell = hit.collider.GetComponent<CellInteractionController>();
                cell.SetCellColoredActive(false);

                if (_returnCoin) {
                    cell.SetCoinActive(true);

                    // --Coin
                }
            }
        }

        public void SetCells(IControlInteractionTheCell[] iControlInteractionTheCells) {
            _iControlInteractionTheCells = iControlInteractionTheCells;
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
    public void ShowCells();
}