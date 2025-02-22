using UnityEngine;
using Zenject;
using Managers;
using GameConfigs;
using MainMenuUI;

public sealed class MainMenuSceneInstaller : MonoInstaller { // Dependency injection pattern
    [Header("Configurations")]
    [SerializeField] private VisualEffectsConfigs _visualEffectsConfigs;
    [SerializeField] private GameplayUIConfigs _gameplayUIConfigs;
    [Header("DI prefabs")]
    [SerializeField] private GameObject _mainMenuUIPrefab;

    public override void InstallBindings() {
        Application.targetFrameRate = 120;

        ConfigsBind();

        ManagersInitAndBind();

        OtherDependencesInitAndBind();
    }

    private void ConfigsBind() {
        Container.Bind<VisualEffectsConfigs>().FromInstance(_visualEffectsConfigs).AsSingle();
        Container.Bind<GameplayUIConfigs>().FromInstance(_gameplayUIConfigs).AsSingle();
    }

    private void ManagersInitAndBind() {
        Container.BindInterfacesTo<MainMenuUIManager>().AsSingle().NonLazy();
    }

    private void OtherDependencesInitAndBind() {        
        Container.BindInterfacesTo<MainMenuUIController>().FromComponentInNewPrefab(_mainMenuUIPrefab).AsSingle().NonLazy();
    }
}