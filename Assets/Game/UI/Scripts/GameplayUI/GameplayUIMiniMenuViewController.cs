using UnityEngine;
using Zenject;
using GameConfigs;
using DG.Tweening;

namespace GameplayUI {
    public sealed class GameplayUIMiniMenuViewController : MonoBehaviour, IControlMiniMenuGameplayUIViewOutput {
        [SerializeField] private RectTransform _rtMiniMenu;
        [SerializeField] private RectTransform _rtButtonLevelReset;
        [SerializeField] private RectTransform _rtButtonBackToMainMenu;

        private bool _isButtonLevelResetAnimationActive;
        private bool _isButtonBackToMainMenuAnimationActive;

        #region DI
            private IControlMiniMenuGameplayUIViewInput _iControlMiniMenuGameplayUIViewInput;
            private GameplayUIConfigs _gameplayUIConfigs;
        #endregion

        [Inject]
        private void Construct(IControlMiniMenuGameplayUIViewInput iControlMiniMenuGameplayUIViewInput, GameplayUIConfigs gameplayUIConfigs) {
            // Set DI
            _iControlMiniMenuGameplayUIViewInput = iControlMiniMenuGameplayUIViewInput;
            _gameplayUIConfigs = gameplayUIConfigs;

            _rtMiniMenu.localScale = Vector3.zero;
            _rtMiniMenu.gameObject.SetActive(false);
        }

        private void Awake() {
            GetComponent<Canvas>().worldCamera = Camera.main;
        }

        public void SetMiniMenuActive(bool isActive) {
            if (isActive) {
                _rtMiniMenu.gameObject.SetActive(true);

                _rtMiniMenu.DOScale(Vector3.one, _gameplayUIConfigs.MiniMenuAnimationDuration);
            } else {
                _rtMiniMenu
                    .DOScale(Vector3.zero, _gameplayUIConfigs.MiniMenuAnimationDuration)
                    .OnComplete(() => _rtMiniMenu.gameObject.SetActive(false))
                ;
            }
        }

        public void ButtonLevelResetAnimationEnable() {
            _isButtonLevelResetAnimationActive = true;

            _rtButtonLevelReset
                .DOPunchScale(_rtButtonLevelReset.localScale * 0.25f, _gameplayUIConfigs.DefaultButtonAnimationDuration)
                .OnComplete(() => _isButtonLevelResetAnimationActive = false)
            ;
        }

        public void ButtonBackToMainMenuAnimationEnable() {
            _isButtonBackToMainMenuAnimationActive = true;

            _rtButtonBackToMainMenu
                .DOPunchScale(_rtButtonBackToMainMenu.localScale * 0.25f, _gameplayUIConfigs.DefaultButtonAnimationDuration)
                .OnComplete(() => _isButtonBackToMainMenuAnimationActive = false)
            ;
        }

        #region Button events
            public void ButtonLevelResetClicked() {
                if (!_isButtonLevelResetAnimationActive) _iControlMiniMenuGameplayUIViewInput.ButtonLevelResetClicked();
            }

            public void ButtonBackToMainMenuClicked() {
                if (!_isButtonBackToMainMenuAnimationActive) _iControlMiniMenuGameplayUIViewInput.ButtonBackToMainMenuClicked();
            }
        #endregion
    }
}

public interface IControlMiniMenuGameplayUIViewOutput {
    public void SetMiniMenuActive(bool isActive);
    public void ButtonLevelResetAnimationEnable();
    public void ButtonBackToMainMenuAnimationEnable();
    
}