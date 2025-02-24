using DG.Tweening;
using GameConfigs;
using UnityEngine;
using Zenject;

namespace CellControllers {
    public sealed class CellRenderController : MonoBehaviour, IControlRenderTheCell {
        private MeshRenderer _mrCell;

        private Vector3 _currentCellScale;

        private MaterialPropertyBlock _mpbCell;

        #region DI
            private BallAndCellConfigs _ballAndCellConfigs;
            private VisualEffectsConfigs _visualEffectsConfigs;
            private IControlRenderTheBall _iControlRenderTheBall;
        #endregion

        [Inject]
        private void Construct (
            BallAndCellConfigs ballAndCellConfigs, 
            VisualEffectsConfigs visualEffectsConfigs,
            IControlRenderTheBall iControlRenderTheBall
            ) {
            // Set DI
            _ballAndCellConfigs = ballAndCellConfigs;
            _visualEffectsConfigs = visualEffectsConfigs;
            _iControlRenderTheBall = iControlRenderTheBall;

            // Set components
            _mrCell = GetComponent<MeshRenderer>();
            _mrCell.material = _visualEffectsConfigs.CellMaterial;

            // Set other fields
            _mpbCell = new MaterialPropertyBlock();
            _mpbCell.SetColor("_BaseColor", _visualEffectsConfigs.CellBaseColor);
            _mpbCell.SetFloat("_CutoffHeight", _visualEffectsConfigs.CellStartCutoffHeight);

            _mrCell.SetPropertyBlock(_mpbCell);

            _currentCellScale = transform.localScale;

            transform.localScale = Vector3.zero;
        }

        public void ColorTheCell(CellPaintType cellPaintType) {
            switch (cellPaintType) {
                case CellPaintType.BallColor:
                    _mpbCell.SetColor("_BallColor", _iControlRenderTheBall.GetBallBaseColor());
                    _mrCell.SetPropertyBlock(_mpbCell);

                    DOTween.To(
                        () => _mpbCell.GetFloat("_CutoffHeight"), 
                        x => {
                            _mpbCell.SetFloat("_CutoffHeight", x);
                            _mrCell.SetPropertyBlock(_mpbCell);
                        }, 
                        _visualEffectsConfigs.CellFinishCutoffHeight, 
                        _ballAndCellConfigs.PaintDuration
                    );
                break;

                case CellPaintType.Default:
                     DOTween.To(
                        () => _mpbCell.GetFloat("_CutoffHeight"), 
                        x => {
                            _mpbCell.SetFloat("_CutoffHeight", x);
                            _mrCell.SetPropertyBlock(_mpbCell);
                        }, 
                        _visualEffectsConfigs.CellStartCutoffHeight, 
                        _ballAndCellConfigs.PaintDuration
                    );
                break;
            }
        }

        public void SpawnEffectEnable() {
            transform.DOScale(_currentCellScale, _visualEffectsConfigs.SpawnDuration);
        }
    }
}

public interface IControlRenderTheCell {
    public void ColorTheCell(CellPaintType cellPaintType);
    public void SpawnEffectEnable();
}

public enum CellPaintType {
    Default,
    BallColor
}