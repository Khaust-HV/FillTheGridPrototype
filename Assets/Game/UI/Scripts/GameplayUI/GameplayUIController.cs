using System;
using UnityEngine;

namespace GameplayUI {
    public sealed class GameplayUIController : MonoBehaviour, IControlBaseGameplayUIViewOutput, IControlMiniMenuGameplayUIViewOutput, IControlBlackWindowViewOutput{
        [SerializeField] GameplayUIBaseViewController _gameplayUIBaseController;
        [SerializeField] GameplayUIMiniMenuViewController _gameplayUIMiniMenuViewController;
        [SerializeField] BlackWindowViewController _blackWindowUIViewController;
        
        #region Base Gameplay UI View 
            public void ButtonBackBeforeActionAnimationEnable() {
                _gameplayUIBaseController.ButtonBackBeforeActionAnimationEnable();
            }

            public void ButtonMiniMenuAnimationEnable() {
                _gameplayUIBaseController.ButtonMiniMenuAnimationEnable();
            }
        #endregion

        #region Mini Menu Gameplay UI View
            public void SetMiniMenuActive(bool isActive) {
                _gameplayUIMiniMenuViewController.SetMiniMenuActive(isActive);
            }

            public void ButtonLevelResetAnimationEnable() {
                _gameplayUIMiniMenuViewController.ButtonLevelResetAnimationEnable();
            }

            public void ButtonBackToMainMenuAnimationEnable() {
                _gameplayUIMiniMenuViewController.ButtonBackToMainMenuAnimationEnable();
            }
        #endregion

        #region Black Window View
            public void SetBlackWindowActive(bool isActive, Action completion) {
                _blackWindowUIViewController.SetBlackWindowActive(isActive, completion);
            }
        #endregion
    }
}