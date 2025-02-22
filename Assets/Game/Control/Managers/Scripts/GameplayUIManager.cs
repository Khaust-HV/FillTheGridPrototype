using Zenject;

namespace Managers {
    public sealed class GameplayUIManager : IControlBaseGameplayUIViewInput {
        #region DI
            private IControlTheLevel _iControlTheLevel;
            private IControlTheLastAction _iControlTheLastAction;
            private IControlBaseGameplayUIViewOutput _iControlBaseGameplayUIViewOutput;
        #endregion

        [Inject]
        private void Construct (
            IControlTheLevel iControlTheLevel, 
            IControlTheLastAction iControlTheLastAction,
            IControlBaseGameplayUIViewOutput iControlBaseGameplayUIViewOutput
            ) {
            // Set DI
            _iControlTheLevel = iControlTheLevel;
            _iControlTheLastAction = iControlTheLastAction;
            _iControlBaseGameplayUIViewOutput = iControlBaseGameplayUIViewOutput;
        }

        public void ButtonBackBeforeActionClicked() {
            if (_iControlTheLastAction.BackBeforeAction()) _iControlBaseGameplayUIViewOutput.ButtonBackBeforeActionAnimationEnable();
        }
    }
}

public interface IControlBaseGameplayUIViewInput {
    public void ButtonBackBeforeActionClicked();
}