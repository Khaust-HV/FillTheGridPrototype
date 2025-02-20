using UnityEngine;
using Zenject;
using Managers;
using GameConfigs;
using System.Linq;

public sealed class GameplaySceneInstaller : MonoInstaller { // Dependency injection pattern
    [Header("Configurations")]
    [SerializeField] private BallConfigs _ballConfigs;
    [SerializeField] private CameraConfigs _cameraConfigs;
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
        Container.Bind<BallConfigs>().FromInstance(_ballConfigs).AsSingle().NonLazy();
        Container.Bind<CameraConfigs>().FromInstance(_cameraConfigs).AsSingle().NonLazy();
    }

    private void ManagersInitAndBind() {
        Container.BindInterfacesTo<InputManager>().AsSingle().NonLazy();
        Container.BindInterfacesTo<LevelManager>().AsSingle().NonLazy();
    }

    private void OtherDependencesInitAndBind() {
        // Container.BindInterfacesTo<LevelObjectPool>().AsSingle().NonLazy();
        
        // Container.BindInterfacesTo<BallMoveController>().FromComponentInNewPrefab(_ballPrefab).AsSingle().NonLazy();

        BindAllInterfacesFromPrefab(_ballPrefab, "BallControllers");
        BindAllInterfacesFromPrefab(_cameraPrefab, "CameraControllers");
    }

    private void BindAllInterfacesFromPrefab(GameObject prefab, string nameSpace) { // Temporary solution
        GameObject instance = Container.InstantiatePrefab(prefab);

        var behaviours = instance.GetComponents<MonoBehaviour>();

        foreach (var behaviour in behaviours) {
            var interfaces = behaviour.GetType (
            ).GetInterfaces().Where(i => behaviour.GetType().Namespace != null && behaviour.GetType().Namespace.StartsWith(nameSpace));

            foreach (var intf in interfaces) {
                Container.Bind(intf).FromInstance(behaviour).AsSingle();
            }
        }
    }
}