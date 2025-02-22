using UnityEngine;
using Zenject;

namespace GameplayUI {
    public sealed class GameplayUIController : MonoBehaviour, IControlBaseGameplayUIViewOutput {
        [SerializeField] GameplayUIBaseViewController _gameplayUIBaseController;

        public void ButtonBackBeforeActionAnimationEnable() {
            _gameplayUIBaseController.ButtonBackBeforeActionAnimationEnable();
        }
    }
}