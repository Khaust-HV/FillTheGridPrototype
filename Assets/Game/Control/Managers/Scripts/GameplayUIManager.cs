using System;
using Zenject;

namespace Managers {
    public sealed class GameplayUIManager : IControlBaseGameplayUIViewInput, IControlMiniMenuGameplayUIViewInput, IControlBlackWindowView {
        private bool _isMiniMenuActive;

        #region DI
            private IControlTheLevel _iControlTheLevel;
            private IControlTheLastAction _iControlTheLastAction;
            private IControlGameplayInput _iControlGameplayInput;
            private IControlBaseGameplayUIViewOutput _iControlBaseGameplayUIViewOutput;
            private IControlMiniMenuGameplayUIViewOutput _iControlMiniMenuGameplayUIViewOutput;
            private IControlBlackWindowViewOutput _iControlBlackWindowUIViewOutput;
        #endregion

        [Inject]
        private void Construct (
            IControlTheLevel iControlTheLevel, 
            IControlTheLastAction iControlTheLastAction,
            IControlGameplayInput iControlGameplayInput,
            IControlBaseGameplayUIViewOutput iControlBaseGameplayUIViewOutput,
            IControlMiniMenuGameplayUIViewOutput iControlMiniMenuGameplayUIViewOutput,
            IControlBlackWindowViewOutput iControlBlackWindowUIViewOutput
            ) {
            // Set DI
            _iControlTheLevel = iControlTheLevel;
            _iControlTheLastAction = iControlTheLastAction;
            _iControlGameplayInput = iControlGameplayInput;
            _iControlBaseGameplayUIViewOutput = iControlBaseGameplayUIViewOutput;
            _iControlMiniMenuGameplayUIViewOutput = iControlMiniMenuGameplayUIViewOutput;
            _iControlBlackWindowUIViewOutput = iControlBlackWindowUIViewOutput;
        }

        #region Base Gameplay UI View
            public void ButtonBackBeforeActionClicked() {
                if (_iControlTheLastAction.BackBeforeAction()) _iControlBaseGameplayUIViewOutput.ButtonBackBeforeActionAnimationEnable();
            }

            public void ButtonMiniMenuClicked() {
                _iControlBaseGameplayUIViewOutput.ButtonMiniMenuAnimationEnable();

                _iControlMiniMenuGameplayUIViewOutput.SetMiniMenuActive(_isMiniMenuActive = !_isMiniMenuActive);

                if (_isMiniMenuActive) _iControlGameplayInput.SetAllGameplayActive(false);
                else _iControlGameplayInput.SetAllGameplayActive(true);
            }
        #endregion 

        #region Mini Menu Gameplay UI View
            public void ButtonLevelResetClicked() {
                _iControlMiniMenuGameplayUIViewOutput.SetMiniMenuActive(_isMiniMenuActive = !_isMiniMenuActive);

                _iControlGameplayInput.SetAllGameplayActive(true);

                _iControlTheLevel.LevelReset();
            }

            public void ButtonBackToMainMenuClicked() {
                SetBlackWindowActive(true, () => SceneLoadController.LoadScene(SceneNameType.MainMenuScene));
            }
        #endregion 

        #region Black Window View
            public void SetBlackWindowActive(bool isActive, Action completion = null) {
                if (isActive) {
                    _iControlBlackWindowUIViewOutput.SetBlackWindowActive(isActive, completion);

                    _iControlGameplayInput.SetAllGameplayActive(false);
                } else _iControlBlackWindowUIViewOutput.SetBlackWindowActive(isActive, completion);
            }
        #endregion
    }
}

public interface IControlBaseGameplayUIViewInput {
    public void ButtonBackBeforeActionClicked();
    public void ButtonMiniMenuClicked();
}

public interface IControlMiniMenuGameplayUIViewInput {
    public void ButtonLevelResetClicked();
    public void ButtonBackToMainMenuClicked();
}

public interface IControlBlackWindowView {
    public void SetBlackWindowActive(bool isActive, Action completion = null);
}