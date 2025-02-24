using System.Collections;
using CellControllers;
using GameConfigs;
using UnityEngine;
using Zenject;

public sealed class GameplayBootstrap : MonoBehaviour { // Entry point pattern
    [SerializeField] private CellInteractionController[] _cellsStorage;

    #region DI
        private IControlGameplayInput _iControlGameplayInput;
        private IControlMoveTheCamera _iControlMoveTheCamera;
        private IControlMoveTheBall _iControlMoveTheBall;
        private IControlRenderTheBall _iControlRenderTheBall;
        private VisualEffectsConfigs _visualEffectsConfigs;
        private IControlTheLevel _iControlTheLevel;
        private IControlBlackWindowView _iControlBlackWindowView;
        private LevelConfigs _levelConfigs;
    #endregion

    [Inject]
    private void Construct (
        IControlGameplayInput iControlGameplayInput, 
        IControlMoveTheCamera iControlMoveTheCamera, 
        IControlMoveTheBall iControlMoveTheBall,
        IControlRenderTheBall iControlRenderTheBall,
        VisualEffectsConfigs visualEffectsConfigs,
        IControlTheLevel iControlTheLevel,
        IControlBlackWindowView iControlBlackWindowView,
        LevelConfigs levelConfigs
        ) {
        // Set DI
        _iControlGameplayInput = iControlGameplayInput;
        _iControlMoveTheCamera = iControlMoveTheCamera;
        _iControlMoveTheBall = iControlMoveTheBall;
        _iControlRenderTheBall = iControlRenderTheBall;
        _visualEffectsConfigs = visualEffectsConfigs;
        _iControlTheLevel = iControlTheLevel;
        _iControlBlackWindowView = iControlBlackWindowView;
        _levelConfigs = levelConfigs;
    }

    private void Start() {
        // Generate level
        StartCoroutine(StageByStageToCreateLevel());

        // Set camera control
        _iControlMoveTheCamera.SetLookedTarget(_iControlMoveTheBall.GetTransformBall());
        _iControlMoveTheCamera.SetMovingActive(true);
        _iControlMoveTheCamera.SetMoveState(CameraMoveState.MovingAfterTheTarget);
    }

    private IEnumerator StageByStageToCreateLevel() {
        // Set player data
        var playrData = SaveAndLoadController.LoadPlayerData();
        
        _iControlRenderTheBall.SetBallBaseColorFromPlayerData(playrData);
        
        // Set cells
        _iControlTheLevel.SetCells(_cellsStorage);

        var levelData = SaveAndLoadController.LoadLevelData(_levelConfigs.LevelName);

        if (levelData.paintedCellsIndexStorage == null) {
            _iControlTheLevel.LevelReset();
        } else _iControlTheLevel.SetLevelData(levelData);

        _iControlTheLevel.ShowCells();

        _iControlBlackWindowView.SetBlackWindowActive(false);

        yield return new WaitForSeconds(_visualEffectsConfigs.SpawnDuration);

        // Set ball
        _iControlRenderTheBall.SpawnEffectEnable();

        yield return new WaitForSeconds(_visualEffectsConfigs.SpawnDuration);

        // Set input active
        _iControlGameplayInput.SetGameplayInputActionMapActive(true);
        _iControlGameplayInput.SetAllGameplayActive(true);
    }
}