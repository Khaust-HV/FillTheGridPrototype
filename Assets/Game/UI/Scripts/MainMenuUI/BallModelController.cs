using DG.Tweening;
using GameConfigs;
using UnityEngine;
using Zenject;

namespace MainMenuUI {
    public sealed class BallModelController : MonoBehaviour, IControlTheBallModel {
        private MeshRenderer _mrBall;

        private MaterialPropertyBlock _mpbBall;

        #region DI
            private VisualEffectsConfigs _visualEffectsConfigs;
        #endregion

        [Inject]
        private void Construct(VisualEffectsConfigs visualEffectsConfigs) {
            // Set DI
            _visualEffectsConfigs = visualEffectsConfigs;

            // Set components
            _mrBall = GetComponent<MeshRenderer>();
            _mrBall.material = _visualEffectsConfigs.DefaultSkinMaterial;

            // Set other fields
            _mpbBall = new MaterialPropertyBlock();

            HideBallModel();
        }

        public void SetBallColor(Color color) {
            _mpbBall.SetColor("_BaseColor", color);

            _mrBall.SetPropertyBlock(_mpbBall);
        }

        public void SpawnAnimationEnable() {
            _mpbBall.SetFloat("_CutoffHeight", _visualEffectsConfigs.BallStartCutoffHeight);

            DOTween.To(
                () => _mpbBall.GetFloat("_CutoffHeight"), 
                x => {
                    _mpbBall.SetFloat("_CutoffHeight", x);
                    _mrBall.SetPropertyBlock(_mpbBall);
                }, 
                _visualEffectsConfigs.BallFinishCutoffHeight, 
                _visualEffectsConfigs.BallDissolveSpawnDuration
            );
        }

        public void HideBallModel() {
            _mpbBall.SetFloat("_CutoffHeight", _visualEffectsConfigs.BallStartCutoffHeight);

            _mrBall.SetPropertyBlock(_mpbBall);
        }
    }
}

public interface IControlTheBallModel {
    public void SetBallColor(Color color);
    public void SpawnAnimationEnable();
    public void HideBallModel();
}