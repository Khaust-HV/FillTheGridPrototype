using System;
using Zenject;

namespace Managers {
    public sealed class MainMenuUIManager : IControlUIMainMenu, IControlBaseMainMenuUIViewInput, IControlLevelsMainMenuUIViewInput, IControlBlackWindowView, IControlSkinStoreMainMenuUIViewInput {
        #region DI
            private IControlBaseMainMenuUIViewOutput _iControlBaseMainMenuUIViewOutput;
            private IControlLevelsMainMenuUIViewOutput _iControlLevelsMainMenuUIViewOutput;
            private IControlBlackWindowViewOutput _iControlBlackWindowUIViewOutput;
            private IControlSkinStoreMainMenuUIViewOutput _iControlSkinStoreMainMenuUIViewOutput;
            private IControlPlayerManagerInMainMenu _iControlPlayerManagerInMainMenu;
        #endregion

        [Inject]
        private void Construct (
            IControlBaseMainMenuUIViewOutput iControlBaseMainMenuUIViewOutput,
            IControlLevelsMainMenuUIViewOutput iControlLevelsMainMenuUIViewOutput,
            IControlBlackWindowViewOutput iControlBlackWindowUIViewOutput,
            IControlSkinStoreMainMenuUIViewOutput iControlSkinStoreMainMenuUIViewOutput,
            IControlPlayerManagerInMainMenu iControlPlayerManagerInMainMenu
            ) {
            // Set DI
            _iControlBaseMainMenuUIViewOutput = iControlBaseMainMenuUIViewOutput;
            _iControlLevelsMainMenuUIViewOutput = iControlLevelsMainMenuUIViewOutput;
            _iControlBlackWindowUIViewOutput = iControlBlackWindowUIViewOutput;
            _iControlSkinStoreMainMenuUIViewOutput = iControlSkinStoreMainMenuUIViewOutput;
            _iControlPlayerManagerInMainMenu = iControlPlayerManagerInMainMenu;
        }
        
        #region Base Main Menu UI View
            public void RootMenuEnable() {
                _iControlBaseMainMenuUIViewOutput.SetRootMenuActive(true);
            }

            public void CoinCountUpdate(int coinNumber) {
                _iControlBaseMainMenuUIViewOutput.CoinCountUpdate(coinNumber);
            }

            public void BallSkinPriceCountUpdate(int coinNumber) {
                _iControlSkinStoreMainMenuUIViewOutput.BallSkinPriceCountUpdate(coinNumber);
            }

            public void ButtonPlayClicked() {
                _iControlBaseMainMenuUIViewOutput.ButtonPlayAnimationEnable();

                _iControlBaseMainMenuUIViewOutput.SetRootMenuActive(false);

                _iControlLevelsMainMenuUIViewOutput.SetLevelsMenuActive(true);
            }

            public void ButtonSkinStoreClicked() {
                _iControlBaseMainMenuUIViewOutput.ButtonSkinStoreAnimationEnable();

                _iControlBaseMainMenuUIViewOutput.SetRootMenuActive(false);

                _iControlSkinStoreMainMenuUIViewOutput.SetSkinStoreMenuActive(true);

                _iControlPlayerManagerInMainMenu.ShowFirstBallSkin();
            }

            public void ButtonSettingsClicked() {
                _iControlBaseMainMenuUIViewOutput.ButtonSettingsAnimationEnable();

                // Enable settings view
            }
        #endregion

        #region Levels Main Menu UI View 
            public void ButtonFirstLevelClicked() {
                _iControlLevelsMainMenuUIViewOutput.ButtonFirstLevelAnimationEnable();

                SetBlackWindowActive(true, () => SceneLoadController.LoadScene(SceneNameType.LevelFirstScene));
            }

            public void ButtonSecondLevelClicked() {
                _iControlLevelsMainMenuUIViewOutput.ButtonSecondLevelAnimationEnable();

                SetBlackWindowActive(true, () => SceneLoadController.LoadScene(SceneNameType.LevelSecondScene));
            }

            public void ButtonThirdLevelClicked() {
                _iControlLevelsMainMenuUIViewOutput.ButtonThirdLevelAnimationEnable();

                SetBlackWindowActive(true, () => SceneLoadController.LoadScene(SceneNameType.LevelThirdScene));
            }

            public void ButtonCloseLevelsMenuClicked() {
                _iControlLevelsMainMenuUIViewOutput.ButtonCloseLevelsMenuAnimationEnable();

                _iControlLevelsMainMenuUIViewOutput.SetLevelsMenuActive(false);

                _iControlBaseMainMenuUIViewOutput.SetRootMenuActive(true);
            }
        #endregion

        #region Black Window View
            public void SetBlackWindowActive(bool isActive, Action completion = null) {
                if (isActive) _iControlBlackWindowUIViewOutput.SetBlackWindowActive(isActive, completion);
                else _iControlBlackWindowUIViewOutput.SetBlackWindowActive(isActive, completion);
            }
        #endregion

        #region Skin Store Main Menu UI View 
            public void ButtonRightChoiceClicked() {
                if (_iControlPlayerManagerInMainMenu.ShowNextBallSkin(1)) _iControlSkinStoreMainMenuUIViewOutput.ButtonRightChoiceAnimationEnable();
            }

            public void ButtonLeftChoiceClicked() {
                if (_iControlPlayerManagerInMainMenu.ShowNextBallSkin(-1)) _iControlSkinStoreMainMenuUIViewOutput.ButtonLeftChoiceAnimationEnable();
            }

            public void ButtonSelectSkinClicked() {
                if (_iControlPlayerManagerInMainMenu.SelectCurrentBallSkin()) _iControlSkinStoreMainMenuUIViewOutput.ButtonSelectSkinAnimationEnable();
            }

            public void ButtonBuySkinClicked() {
                if (_iControlPlayerManagerInMainMenu.BuyCurrentBallSkin()) _iControlSkinStoreMainMenuUIViewOutput.ButtonBuySkinAnimationEnable();
            }

            public void ButtonCloseSkinStoreMenuClicked() {
                _iControlSkinStoreMainMenuUIViewOutput.ButtonCloseSkinStoreMenuAnimationEnable();

                _iControlSkinStoreMainMenuUIViewOutput.SetSkinStoreMenuActive(false);

                _iControlBaseMainMenuUIViewOutput.SetRootMenuActive(true);

                _iControlPlayerManagerInMainMenu.HideBallModel();
            }
        #endregion
    }
}

public interface IControlUIMainMenu {
    public void RootMenuEnable();
    public void CoinCountUpdate(int coinNumber);
    public void BallSkinPriceCountUpdate(int coinNumber);
}

public interface IControlBaseMainMenuUIViewInput {
    public void ButtonPlayClicked();
    public void ButtonSkinStoreClicked();
    public void ButtonSettingsClicked();
}

public interface IControlLevelsMainMenuUIViewInput {
    public void ButtonFirstLevelClicked();
    public void ButtonSecondLevelClicked();
    public void ButtonThirdLevelClicked();
    public void ButtonCloseLevelsMenuClicked();
}

public interface IControlSkinStoreMainMenuUIViewInput {
    public void ButtonRightChoiceClicked();
    public void ButtonLeftChoiceClicked();
    public void ButtonSelectSkinClicked();
    public void ButtonBuySkinClicked();
    public void ButtonCloseSkinStoreMenuClicked();
}