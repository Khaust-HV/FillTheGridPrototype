using DG.Tweening;
using GameConfigs;
using TMPro;
using UnityEngine;
using Zenject;

namespace MainMenuUI {
    public sealed class MainMenuUISkinStoreViewController : MonoBehaviour, IControlSkinStoreMainMenuUIViewOutput {
        [SerializeField] private GameObject _coinCountPanel;
        [SerializeField] private TextMeshProUGUI _coinCountText;
        [SerializeField] private RectTransform _rtStoreMenu;
        [SerializeField] private RectTransform _rtButtonRightChoice;
        [SerializeField] private RectTransform _rtButtonLeftChoice;
        [SerializeField] private RectTransform _rtButtonSelectSkin;
        [SerializeField] private RectTransform _rtButtonBuySkin;
        [SerializeField] private RectTransform _rtButtonCloseSkinStoreMenu;

        private bool _isButtonRightChoiceAnimationActive;
        private bool _isButtonLeftChoiceAnimationActive;
        private bool _isButtonSelectSkinAnimationActive;
        private bool _isButtonBuySkinAnimationActive;
        private bool _isButtonCloseSkinStoreMenuAnimationActive;

        #region DI
            private MainMenuUIConfigs _mainMenuUIConfigs;
            private IControlSkinStoreMainMenuUIViewInput _iControlSkinStoreMainMenuUIViewInput;
        #endregion

        [Inject]
        private void Construct(MainMenuUIConfigs mainMenuUIConfigs, IControlSkinStoreMainMenuUIViewInput iControlSkinStoreMainMenuUIViewInput) {
            // Set DI
            _mainMenuUIConfigs = mainMenuUIConfigs;
            _iControlSkinStoreMainMenuUIViewInput = iControlSkinStoreMainMenuUIViewInput;

            _rtStoreMenu.localScale = Vector3.zero;
            _rtStoreMenu.gameObject.SetActive(false);
        }

        private void Awake() {
            GetComponent<Canvas>().worldCamera = Camera.main;
        }

        public void BallSkinPriceCountUpdate(int coinNumber) {
            _coinCountText.text = coinNumber.ToString();
        }

        public void SetSkinStoreMenuActive(bool isActive) {
            if (isActive) {
                _rtStoreMenu.gameObject.SetActive(true);

                _rtStoreMenu.DOScale(Vector3.one, _mainMenuUIConfigs.DefaultMenuAnimationDuration);
            } else {
                _rtStoreMenu
                    .DOScale(Vector3.zero, _mainMenuUIConfigs.DefaultMenuAnimationDuration)
                    .OnComplete(() => _rtStoreMenu.gameObject.SetActive(false))
                ;
            }
        }

        public void ButtonRightChoiceAnimationEnable() {
            _isButtonRightChoiceAnimationActive = true;

            _rtButtonRightChoice
                .DOPunchScale(_rtButtonRightChoice.localScale * 0.25f, _mainMenuUIConfigs.DefaultButtonAnimationDuration)
                .OnComplete(() => _isButtonRightChoiceAnimationActive = false)
            ;
        }

        public void ButtonLeftChoiceAnimationEnable() {
            _isButtonLeftChoiceAnimationActive = true;

            _rtButtonLeftChoice
                .DOPunchScale(_rtButtonLeftChoice.localScale * 0.25f, _mainMenuUIConfigs.DefaultButtonAnimationDuration)
                .OnComplete(() => _isButtonLeftChoiceAnimationActive = false)
            ;
        }

        public void ButtonSelectSkinAnimationEnable() {
            _isButtonSelectSkinAnimationActive = true;

            _rtButtonSelectSkin
                .DOPunchScale(_rtButtonSelectSkin.localScale * 0.25f, _mainMenuUIConfigs.DefaultButtonAnimationDuration)
                .OnComplete(() => _isButtonSelectSkinAnimationActive = false)
            ;
        }

        public void ButtonBuySkinAnimationEnable() {
            _isButtonBuySkinAnimationActive = true;

            _rtButtonBuySkin
                .DOPunchScale(_rtButtonBuySkin.localScale * 0.25f, _mainMenuUIConfigs.DefaultButtonAnimationDuration)
                .OnComplete(() => _isButtonBuySkinAnimationActive = false)
            ;
        }

        public void ButtonCloseSkinStoreMenuAnimationEnable() {
            _isButtonCloseSkinStoreMenuAnimationActive = true;

            _rtButtonCloseSkinStoreMenu
                .DOPunchScale(_rtButtonCloseSkinStoreMenu.localScale * 0.25f, _mainMenuUIConfigs.DefaultButtonAnimationDuration)
                .OnComplete(() => _isButtonCloseSkinStoreMenuAnimationActive = false)
            ;
        }

        #region Button events 
            public void ButtonRightChoiceClicked() {
                if (!_isButtonRightChoiceAnimationActive) _iControlSkinStoreMainMenuUIViewInput.ButtonRightChoiceClicked();
            }

            public void ButtonLeftChoiceClicked() {
                if (!_isButtonLeftChoiceAnimationActive) _iControlSkinStoreMainMenuUIViewInput.ButtonLeftChoiceClicked();
            }

            public void ButtonSelectSkinClicked() {
                if (!_isButtonSelectSkinAnimationActive) _iControlSkinStoreMainMenuUIViewInput.ButtonSelectSkinClicked();
            }

            public void ButtonBuySkinClicked() {
                if (!_isButtonBuySkinAnimationActive) _iControlSkinStoreMainMenuUIViewInput.ButtonBuySkinClicked();
            }

            public void ButtonCloseSkinStoreMenuClicked() {
                if (!_isButtonCloseSkinStoreMenuAnimationActive) _iControlSkinStoreMainMenuUIViewInput.ButtonCloseSkinStoreMenuClicked();
            }
        #endregion
    }
}

public interface IControlSkinStoreMainMenuUIViewOutput {
    public void BallSkinPriceCountUpdate(int coinNumber);
    public void SetSkinStoreMenuActive(bool isActive);
    public void ButtonRightChoiceAnimationEnable();
    public void ButtonLeftChoiceAnimationEnable();
    public void ButtonSelectSkinAnimationEnable();
    public void ButtonBuySkinAnimationEnable();
    public void ButtonCloseSkinStoreMenuAnimationEnable();
}