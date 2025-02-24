using DG.Tweening;
using GameConfigs;
using TMPro;
using UnityEngine;
using Zenject;

namespace MainMenuUI {
    public sealed class MainMenuUIBaseViewController : MonoBehaviour, IControlBaseMainMenuUIViewOutput {
        [SerializeField] private TextMeshProUGUI _coinCountText;
        [SerializeField] private RectTransform _rtRootMenu;
        [SerializeField] private RectTransform _rtButtonPlay;
        [SerializeField] private RectTransform _rtButtonSkinSettings;
        [SerializeField] private RectTransform _rtButtonSettings;

        private bool _isButtonPlayAnimationActive;
        private bool _isButtonSkinStoreAnimationActive;
        private bool _isButtonSettingsAnimationActive;

        #region DI
            private MainMenuUIConfigs _mainMenuUIConfigs;
            private IControlBaseMainMenuUIViewInput _iControlBaseMainMenuUIViewInput;
        #endregion

        [Inject]
        private void Construct(MainMenuUIConfigs mainMenuUIConfigs, IControlBaseMainMenuUIViewInput iControlBaseMainMenuUIViewInput) {
            // Set DI
            _mainMenuUIConfigs = mainMenuUIConfigs;
            _iControlBaseMainMenuUIViewInput = iControlBaseMainMenuUIViewInput;

            _rtRootMenu.localScale = Vector3.zero;
            _rtRootMenu.gameObject.SetActive(false);
        }

        private void Awake() {
            GetComponent<Canvas>().worldCamera = Camera.main;
        }

        public void CoinCountUpdate(int coinNumber) {
            _coinCountText.text = coinNumber.ToString();
        }

        public void SetRootMenuActive(bool isActive) {
            if (isActive) {
                _rtRootMenu.gameObject.SetActive(true);

                _rtRootMenu.DOScale(Vector3.one, _mainMenuUIConfigs.DefaultMenuAnimationDuration);
            } else {
                _rtRootMenu
                    .DOScale(Vector3.zero, _mainMenuUIConfigs.DefaultMenuAnimationDuration)
                    .OnComplete(() => _rtRootMenu.gameObject.SetActive(false))
                ;
            }
        }

        public void ButtonPlayAnimationEnable() {
            _isButtonPlayAnimationActive = true;

            _rtButtonPlay
                .DOPunchScale(_rtButtonPlay.localScale * 0.25f, _mainMenuUIConfigs.DefaultButtonAnimationDuration)
                .OnComplete(() => _isButtonPlayAnimationActive = false)
            ;
        }

        public void ButtonSkinStoreAnimationEnable() {
            _isButtonSkinStoreAnimationActive = true;

            _rtButtonSkinSettings
                .DOPunchScale(_rtButtonSkinSettings.localScale * 0.25f, _mainMenuUIConfigs.DefaultButtonAnimationDuration)
                .OnComplete(() => _isButtonSkinStoreAnimationActive = false)
            ;
        }

        public void ButtonSettingsAnimationEnable() {
            _isButtonSkinStoreAnimationActive = true;

            _rtButtonSettings
                .DOPunchScale(_rtButtonSettings.localScale * 0.25f, _mainMenuUIConfigs.DefaultButtonAnimationDuration)
                .OnComplete(() => _isButtonSkinStoreAnimationActive = false)
            ;
        }

        #region Button events
            public void ButtonPlayClicked() {
                if (!_isButtonPlayAnimationActive) _iControlBaseMainMenuUIViewInput.ButtonPlayClicked();
            }

            public void ButtonSkinStoreClicked() {
                if (!_isButtonSkinStoreAnimationActive) _iControlBaseMainMenuUIViewInput.ButtonSkinStoreClicked();
            }

            public void ButtonSettingsClicked() {
                if (!_isButtonSettingsAnimationActive) _iControlBaseMainMenuUIViewInput.ButtonSettingsClicked();
            }
        #endregion
    }
}

public interface IControlBaseMainMenuUIViewOutput {
    public void CoinCountUpdate(int coinNumber);
    public void SetRootMenuActive(bool isActive);
    public void ButtonPlayAnimationEnable();
    public void ButtonSkinStoreAnimationEnable();
    public void ButtonSettingsAnimationEnable();
}