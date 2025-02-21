using DG.Tweening;
using GameConfigs;
using UnityEngine;
using Zenject;

namespace BallControllers {
    public sealed class BallRenderController : MonoBehaviour, IControlRenderTheBall {
        private MeshRenderer _mrBall;

        private Vector3 _currentBallScale;

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
            _mpbBall.SetColor("_BaseColor", _visualEffectsConfigs.DefaultSkinColor);

            _mrBall.SetPropertyBlock(_mpbBall);

            _currentBallScale = transform.localScale;

            transform.localScale = Vector3.zero;
        }

        public Color GetBallBaseColor() {
            return _mpbBall.GetColor("_BaseColor");
        }

        public void SpawnEffectEnable() {
            transform.DOScale(_currentBallScale, _visualEffectsConfigs.SpawnDuration);
        }
    }
}

public interface IControlRenderTheBall {
    public Color GetBallBaseColor();
    public void SpawnEffectEnable();
}