using System;
using DG.Tweening;
using GameConfigs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class BlackWindowViewController : MonoBehaviour, IControlBlackWindowViewOutput {
    [SerializeField] private Image _blackWindow;

    #region DI
        private GameplayUIConfigs _gameplayUIConfigs;
    #endregion

    [Inject]
    private void Construct(GameplayUIConfigs gameplayUIConfigs) {
        // Set DI
        _gameplayUIConfigs = gameplayUIConfigs;

        _blackWindow.gameObject.SetActive(false);
    }

    private void Awake() {
        GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void SetBlackWindowActive(bool isActive, Action completion) {
        if (isActive) {
            _blackWindow.color = new Color(0f, 0f, 0f, 0f);

            _blackWindow.gameObject.SetActive(true);

            _blackWindow.DOFade(1, _gameplayUIConfigs.BlackWindowAnimationDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() => completion?.Invoke())
            ;
        } else {
            _blackWindow.color = new Color(0f, 0f, 0f, 1f);

            _blackWindow.gameObject.SetActive(true);

            _blackWindow.DOFade(0, _gameplayUIConfigs.BlackWindowAnimationDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() => _blackWindow.gameObject.SetActive(false));
            ;
        }   
    }
}

public interface IControlBlackWindowViewOutput {
    public void SetBlackWindowActive(bool isActive, Action completion);
}