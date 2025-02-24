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

            _currentBallScale = transform.localScale;

            transform.localScale = Vector3.zero;
        }

        public void SetBallBaseColorFromPlayerData(PlayerData playerData) {
            Color color = playerData.currentBallSkinType switch {
                BallSkinType.Default => _visualEffectsConfigs.DefaultSkinColor,
                BallSkinType.Green => _visualEffectsConfigs.GreenSkinColor,
                BallSkinType.Orange => _visualEffectsConfigs.OrangeSkinColor,
                _ => throw new System.Exception("The skin was not found")
            };

            _mpbBall.SetColor("_BaseColor", color);

            _mrBall.SetPropertyBlock(_mpbBall);
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
    public void SetBallBaseColorFromPlayerData(PlayerData playerData);
    public Color GetBallBaseColor();
    public void SpawnEffectEnable();
}