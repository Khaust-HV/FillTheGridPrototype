using UnityEngine;
using Zenject;
using Managers;
using GameConfigs;
using System.Linq;
using BallControllers;
using CameraControllers;

public sealed class GameplaySceneInstaller : MonoInstaller { // Dependency injection pattern
    [Header("Configurations")]
    [SerializeField] private BallAndCellConfigs _ballAndCellConfigs;
    [SerializeField] private CameraConfigs _cameraConfigs;
    [SerializeField] private VisualEffectsConfigs _visualEffectsConfigs;
    [SerializeField] private LevelConfigs _levelConfigs;
    [Header("DI prefabs")]
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private GameObject _cameraPrefab;

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
    }

    private void ManagersInitAndBind() {
        Container.BindInterfacesTo<PlayerManager>().AsSingle().NonLazy();
        Container.BindInterfacesTo<InputManager>().AsSingle().NonLazy();
        Container.BindInterfacesTo<LevelManager>().AsSingle().NonLazy();
    }

    private void OtherDependencesInitAndBind() {
        // Container.BindInterfacesTo<LevelObjectPool>().AsSingle().NonLazy();
        
        Container.BindInterfacesTo<BallController>().FromComponentInNewPrefab(_ballPrefab).AsSingle().NonLazy();
        Container.BindInterfacesTo<CameraController>().FromComponentInNewPrefab(_cameraPrefab).AsSingle().NonLazy();
    }
}