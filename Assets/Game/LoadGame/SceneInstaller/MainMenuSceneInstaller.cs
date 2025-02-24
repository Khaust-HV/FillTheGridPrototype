using UnityEngine;
using Zenject;
using Managers;
using GameConfigs;
using MainMenuUI;
using BallControllers;

public sealed class MainMenuSceneInstaller : MonoInstaller { // Dependency injection pattern
    [Header("Configurations")]
    [SerializeField] private VisualEffectsConfigs _visualEffectsConfigs;
    [SerializeField] private MainMenuUIConfigs _mainMenuUIConfigs;
    [SerializeField] private GameplayUIConfigs _gameplayUIConfigs;
    [SerializeField] private BallAndCellConfigs _ballAndCellConfigs;
    [Header("DI prefabs")]
    [SerializeField] private GameObject _mainMenuUIPrefab;
    [SerializeField] private GameObject _ballModelPrefab;

    public override void InstallBindings() {
        Application.targetFrameRate = 120;

        ConfigsBind();

        ManagersInitAndBind();

        OtherDependencesInitAndBind();
    }

    private void ConfigsBind() {
        Container.Bind<VisualEffectsConfigs>().FromInstance(_visualEffectsConfigs).AsSingle();
        Container.Bind<MainMenuUIConfigs>().FromInstance(_mainMenuUIConfigs).AsSingle();
        Container.Bind<GameplayUIConfigs>().FromInstance(_gameplayUIConfigs).AsSingle();
        Container.Bind<BallAndCellConfigs>().FromInstance(_ballAndCellConfigs).AsSingle();
    }

    private void ManagersInitAndBind() {
        Container.BindInterfacesTo<MainMenuUIManager>().AsSingle().NonLazy();
        Container.BindInterfacesTo<MainMenuPlayerManager>().AsSingle().NonLazy();
    }

    private void OtherDependencesInitAndBind() {        
        Container.BindInterfacesTo<MainMenuUIController>().FromComponentInNewPrefab(_mainMenuUIPrefab).AsSingle().NonLazy();
        Container.BindInterfacesTo<BallModelController>().FromComponentInNewPrefab(_ballModelPrefab).AsSingle().NonLazy();
    }
}