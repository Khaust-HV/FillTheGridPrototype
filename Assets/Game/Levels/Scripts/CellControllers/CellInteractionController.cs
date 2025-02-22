using UnityEngine;
using Zenject;

namespace CellControllers {
    public sealed class CellInteractionController : MonoBehaviour, IControlInteractionTheCell {
        [SerializeField] private SpriteRenderer _spCoin;

        private bool _isCellColored;
        private bool _isCellHaveCoin;

        private IControlRenderTheCell _iControlRenderTheCell;

        [Inject]
        private void Construct() {
            // Set components
            _iControlRenderTheCell = GetComponent<CellRenderController>();
        }

        public bool DoesThisCellColored() {
            return _isCellColored;
        }

        public void SetCellColoredActive(bool isActive) {
            if (isActive) {
                _isCellColored = true;
                _iControlRenderTheCell.ColorTheCell(CellPaintType.BallColor);
            } else {
                _isCellColored = false;
                _iControlRenderTheCell.ColorTheCell(CellPaintType.Default);
            }
        }

        public bool DoesThisCellHaveCoin() {
            return _isCellHaveCoin;
        }

        public void SetCoinActive(bool isActive) {
            _spCoin.enabled = isActive;
            _isCellHaveCoin = isActive;
        }

        public void SpawnEffectEnable() {
            _iControlRenderTheCell.SpawnEffectEnable();
        }
    }
}

public interface IControlInteractionTheCell {
    public bool DoesThisCellColored();
    public void SetCellColoredActive(bool isActive);
    public bool DoesThisCellHaveCoin();
    public void SetCoinActive(bool isActive);
    public void SpawnEffectEnable();
}