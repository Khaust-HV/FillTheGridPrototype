using DG.Tweening;
using GameConfigs;
using UnityEngine;
using Zenject;

namespace MainMenuUI {
    public sealed class MainMenuUILevelsViewController : MonoBehaviour, IControlLevelsMainMenuUIViewOutput {
        [SerializeField] private RectTransform _rtLevelsMenu;
        [SerializeField] private RectTransform _rtButtonFirstLevel;
        [SerializeField] private RectTransform _rtButtonSecondLevel;
        [SerializeField] private RectTransform _rtButtonThirdLevel;
        [SerializeField] private RectTransform _rtButtonBackToRootMenu;

        private bool _isButtonFirstLevelAnimationActive;
        private bool _isButtonSecondLevelAnimationActive;
        private bool _isButtonThirdLevelAnimationActive;
        private bool _isButtonCloseLevelsMenuAnimationActive;

        #region DI
            private MainMenuUIConfigs _mainMenuUIConfigs;
            private IControlLevelsMainMenuUIViewInput _iControlLevelsMainMenuUIViewInput;
        #endregion

        [Inject]
        private void Construct(MainMenuUIConfigs mainMenuUIConfigs, IControlLevelsMainMenuUIViewInput iControlLevelsMainMenuUIViewInput) {
            // Set DI
            _mainMenuUIConfigs = mainMenuUIConfigs;
            _iControlLevelsMainMenuUIViewInput = iControlLevelsMainMenuUIViewInput;

            _rtLevelsMenu.localScale = Vector3.zero;
            _rtLevelsMenu.gameObject.SetActive(false);
        }

        private void Awake() {
            GetComponent<Canvas>().worldCamera = Camera.main;
        }

        public void SetLevelsMenuActive(bool isActive) {
            if (isActive) {
                _rtLevelsMenu.gameObject.SetActive(true);

                _rtLevelsMenu.DOScale(Vector3.one, _mainMenuUIConfigs.DefaultMenuAnimationDuration);
            } else {
                _rtLevelsMenu
                    .DOScale(Vector3.zero, _mainMenuUIConfigs.DefaultMenuAnimationDuration)
                    .OnComplete(() => _rtLevelsMenu.gameObject.SetActive(false))
                ;
            }
        }

        public void ButtonFirstLevelAnimationEnable() {
            _isButtonFirstLevelAnimationActive = true;

            _rtButtonFirstLevel
                .DOPunchScale(_rtButtonFirstLevel.localScale * 0.25f, _mainMenuUIConfigs.DefaultButtonAnimationDuration)
                .OnComplete(() => _isButtonFirstLevelAnimationActive = false)
            ;
        }

        public void ButtonSecondLevelAnimationEnable() {
            _isButtonSecondLevelAnimationActive = true;

            _rtButtonSecondLevel
                .DOPunchScale(_rtButtonSecondLevel.localScale * 0.25f, _mainMenuUIConfigs.DefaultButtonAnimationDuration)
                .OnComplete(() => _isButtonSecondLevelAnimationActive = false)
            ;
        }

        public void ButtonThirdLevelAnimationEnable() {
            _isButtonThirdLevelAnimationActive = true;

            _rtButtonThirdLevel
                .DOPunchScale(_rtButtonThirdLevel.localScale * 0.25f, _mainMenuUIConfigs.DefaultButtonAnimationDuration)
                .OnComplete(() => _isButtonThirdLevelAnimationActive = false)
            ;
        }

        public void ButtonCloseLevelsMenuAnimationEnable() {
            _isButtonCloseLevelsMenuAnimationActive = true;

            _rtButtonBackToRootMenu
                .DOPunchScale(_rtButtonBackToRootMenu.localScale * 0.25f, _mainMenuUIConfigs.DefaultButtonAnimationDuration)
                .OnComplete(() => _isButtonCloseLevelsMenuAnimationActive = false)
            ;
        }

        #region Button events
            public void ButtonFirstLevelClicked() {
                if (!_isButtonFirstLevelAnimationActive) _iControlLevelsMainMenuUIViewInput.ButtonFirstLevelClicked();
            }

            public void ButtonSecondLevelClicked() {
                if (!_isButtonSecondLevelAnimationActive) _iControlLevelsMainMenuUIViewInput.ButtonSecondLevelClicked();
            }

            public void ButtonThirdLevelClicked() {
                if (!_isButtonThirdLevelAnimationActive) _iControlLevelsMainMenuUIViewInput.ButtonThirdLevelClicked();
            }

            public void ButtonCloseLevelsMenuClicked() {
                if (!_isButtonCloseLevelsMenuAnimationActive) _iControlLevelsMainMenuUIViewInput.ButtonCloseLevelsMenuClicked();
            }
        #endregion
    }
}

public interface IControlLevelsMainMenuUIViewOutput {
    public void SetLevelsMenuActive(bool isActive);
    public void ButtonFirstLevelAnimationEnable();
    public void ButtonSecondLevelAnimationEnable();
    public void ButtonThirdLevelAnimationEnable();
    public void ButtonCloseLevelsMenuAnimationEnable();
}