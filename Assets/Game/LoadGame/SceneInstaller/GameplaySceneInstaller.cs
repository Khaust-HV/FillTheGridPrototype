using UnityEngine;
using Zenject;
using Managers;
using GameConfigs;
using BallControllers;
using CameraControllers;
using GameplayUI;

public sealed class GameplaySceneInstaller : MonoInstaller { // Dependency injection pattern
    [Header("Configurations")]
    [SerializeField] private BallAndCellConfigs _ballAndCellConfigs;
    [SerializeField] private CameraConfigs _cameraConfigs;
    [SerializeField] private VisualEffectsConfigs _visualEffectsConfigs;
    [SerializeField] private LevelConfigs _levelConfigs;
    [SerializeField] private GameplayUIConfigs _gameplayUIConfigs;
    [Header("DI prefabs")]
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private GameObject _cameraPrefab;
    [SerializeField] private GameObject _gameplayUIPrefab;

    public override void InstallBindings() {
        Application.targetFrameRate = 120;

        ConfigsBind();

        ManagersInitAndBind();

        OtherDependencesInitAndBind();
    }

    private void ConfigsBind() {
        Container.Bind<BallAndCellConfigs>().FromInstance(_ballAndCellConfigs).AsSingle();
        Container.Bind<CameraConfigs>().FromInstance(_cameraConfigs).AsSingle();
        Container.Bind<VisualEffectsConfigs>().FromInstance(_visualEffectsConfigs).AsSingle();
        Container.Bind<LevelConfigs>().FromInstance(_levelConfigs).AsSingle();
        Container.Bind<GameplayUIConfigs>().FromInstance(_gameplayUIConfigs).AsSingle();
    }

    private void ManagersInitAndBind() {
        Container.BindInterfacesTo<GameplayPlayerManager>().AsSingle().NonLazy();
        Container.BindInterfacesTo<InputManager>().AsSingle().NonLazy();
        Container.BindInterfacesTo<LevelManager>().AsSingle().NonLazy();
        Container.BindInterfacesTo<GameplayUIManager>().AsSingle().NonLazy();
    }

    private void OtherDependencesInitAndBind() {        
        Container.BindInterfacesTo<BallController>().FromComponentInNewPrefab(_ballPrefab).AsSingle().NonLazy();
        Container.BindInterfacesTo<CameraController>().FromComponentInNewPrefab(_cameraPrefab).AsSingle().NonLazy();
        Container.BindInterfacesTo<GameplayUIController>().FromComponentInNewPrefab(_gameplayUIPrefab).AsSingle().NonLazy();
    }
}