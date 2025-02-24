using System;
using UnityEngine;

namespace MainMenuUI {
    public sealed class MainMenuUIController : MonoBehaviour, IControlBaseMainMenuUIViewOutput, IControlLevelsMainMenuUIViewOutput, IControlBlackWindowViewOutput, IControlSkinStoreMainMenuUIViewOutput {
        [SerializeField] private MainMenuUIBaseViewController _mainMenuUIBaseViewController;
        [SerializeField] private MainMenuUILevelsViewController _mainMenuUILevelsViewController;
        [SerializeField] private BlackWindowViewController _blackWindowUIViewController;
        [SerializeField] private MainMenuUISkinStoreViewController _mainMenuUISkinStoreViewController;

        #region Base Main Menu UI View 
            public void CoinCountUpdate(int coinNumber) {
                _mainMenuUIBaseViewController.CoinCountUpdate(coinNumber);
            }

            public void SetRootMenuActive(bool isActive) {
                _mainMenuUIBaseViewController.SetRootMenuActive(isActive);
            }

            public void ButtonPlayAnimationEnable() {
                _mainMenuUIBaseViewController.ButtonPlayAnimationEnable();
            }

            public void ButtonSkinStoreAnimationEnable() {
                _mainMenuUIBaseViewController.ButtonSkinStoreAnimationEnable();
            }

            public void ButtonSettingsAnimationEnable() {
                _mainMenuUIBaseViewController.ButtonSettingsAnimationEnable();
            }
        #endregion

        #region Levels Main Menu UI View 
            public void SetLevelsMenuActive(bool isActive) {
                _mainMenuUILevelsViewController.SetLevelsMenuActive(isActive);
            }

            public void ButtonFirstLevelAnimationEnable() {
                _mainMenuUILevelsViewController.ButtonFirstLevelAnimationEnable();
            }

            public void ButtonSecondLevelAnimationEnable() {
                _mainMenuUILevelsViewController.ButtonSecondLevelAnimationEnable();
            }

            public void ButtonThirdLevelAnimationEnable() {
                _mainMenuUILevelsViewController.ButtonThirdLevelAnimationEnable();
            }

            public void ButtonCloseLevelsMenuAnimationEnable() {
                _mainMenuUILevelsViewController.ButtonCloseLevelsMenuAnimationEnable();
            }
        #endregion

        #region Black Window View
            public void SetBlackWindowActive(bool isActive, Action completion) {
                _blackWindowUIViewController.SetBlackWindowActive(isActive, completion);
            }
        #endregion

        #region Skin Store Main Menu UI View 
             public void BallSkinPriceCountUpdate(int coinNumber) {
                _mainMenuUISkinStoreViewController.BallSkinPriceCountUpdate(coinNumber);
             }

            public void SetSkinStoreMenuActive(bool isActive) {
                _mainMenuUISkinStoreViewController.SetSkinStoreMenuActive(isActive);
            }

            public void ButtonRightChoiceAnimationEnable() {
                _mainMenuUISkinStoreViewController.ButtonRightChoiceAnimationEnable();
            }

            public void ButtonLeftChoiceAnimationEnable() {
                _mainMenuUISkinStoreViewController.ButtonLeftChoiceAnimationEnable();
            }

            public void ButtonSelectSkinAnimationEnable() {
                _mainMenuUISkinStoreViewController.ButtonSelectSkinAnimationEnable();
            }

            public void ButtonBuySkinAnimationEnable() {
                _mainMenuUISkinStoreViewController.ButtonBuySkinAnimationEnable();
            }

            public void ButtonCloseSkinStoreMenuAnimationEnable() {
                _mainMenuUISkinStoreViewController.ButtonCloseSkinStoreMenuAnimationEnable();
            }
        #endregion
    }
}