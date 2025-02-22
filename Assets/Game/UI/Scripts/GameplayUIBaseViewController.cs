using UnityEngine;
using Zenject;
using GameConfigs;
using DG.Tweening;

namespace GameplayUI {
    public sealed class GameplayUIBaseViewController : MonoBehaviour, IControlBaseGameplayUIViewOutput {
        [SerializeField] private RectTransform _rtButtonBackBeforeAction;

        private bool _isButtonBackBeforeActionAnimationActive;

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
                .SetEase(Ease.Linear)
                .OnComplete(() => _isButtonBackBeforeActionAnimationActive = false)
            ;


        }

        #region Button events
            public void ButtonBackBeforeActionClicked() {
                if (!_isButtonBackBeforeActionAnimationActive) _iControlBaseGameplayUIViewInput.ButtonBackBeforeActionClicked();
            }
        #endregion
    }
}

public interface IControlBaseGameplayUIViewOutput {
    public void ButtonBackBeforeActionAnimationEnable();
}