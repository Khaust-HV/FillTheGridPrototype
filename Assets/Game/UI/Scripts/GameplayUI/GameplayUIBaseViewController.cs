using UnityEngine;
using Zenject;
using GameConfigs;
using DG.Tweening;

namespace GameplayUI {
    public sealed class GameplayUIBaseViewController : MonoBehaviour, IControlBaseGameplayUIViewOutput {
        [SerializeField] private RectTransform _rtButtonBackBeforeAction;
        [SerializeField] private RectTransform _rtButtonMiniMenu;

        private bool _isButtonBackBeforeActionAnimationActive;
        private bool _isButtonMiniMenuAnimationActive;

        #region DI
            private IControlBaseGameplayUIViewInput _iControlBaseGameplayUIViewInput;
            private GameplayUIConfigs _gameplayUIConfigs;
        #endregion

        [Inject]
        private void Construct(IControlBaseGameplayUIViewInput iControlBaseGameplayUIViewInput, GameplayUIConfigs gameplayUIConfigs) {
            // Set DI
            _iControlBaseGameplayUIViewInput = iControlBaseGameplayUIViewInput;
            _gameplayUIConfigs = gameplayUIConfigs;
        }

        private void Awake() {
            GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        public void ButtonBackBeforeActionAnimationEnable() {
            _isButtonBackBeforeActionAnimationActive = true;

            _rtButtonBackBeforeAction
                .DOLocalRotate(new Vector3(0f, 0f, -360f), _gameplayUIConfigs.ButtonBackBeforeActionAnimationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutCubic)
                .OnComplete(() => _isButtonBackBeforeActionAnimationActive = false)
            ;
        }

        public void ButtonMiniMenuAnimationEnable() {
            _isButtonMiniMenuAnimationActive = true;

            _rtButtonMiniMenu
                .DOPunchScale(_rtButtonMiniMenu.localScale * 0.25f, _gameplayUIConfigs.ButtonMiniMenuAnimationDuration)
                .OnComplete(() => _isButtonMiniMenuAnimationActive = false)
            ;
        }

        #region Button events
            public void ButtonBackBeforeActionClicked() {
                if (!_isButtonBackBeforeActionAnimationActive) _iControlBaseGameplayUIViewInput.ButtonBackBeforeActionClicked();
            }

            public void ButtonMiniMenuClicked() {
                if (!_isButtonMiniMenuAnimationActive) _iControlBaseGameplayUIViewInput.ButtonMiniMenuClicked();
            }
        #endregion
    }
}

public interface IControlBaseGameplayUIViewOutput {
    public void ButtonBackBeforeActionAnimationEnable();
    public void ButtonMiniMenuAnimationEnable();
}