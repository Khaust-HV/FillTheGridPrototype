using System.Collections;
using CellControllers;
using GameConfigs;
using UnityEngine;
using Zenject;

public sealed class Bootstrap : MonoBehaviour { // Entry point pattern
    [SerializeField] private CellInteractionController[] _cellsStorage;

    #region DI
        private IControlGameplayInput _iControlGameplayInput;
        private IControlMoveTheCamera _iControlMoveTheCamera;
        private IControlMoveTheBall _iControlMoveTheBall;
        private IControlRenderTheBall _iControlRenderTheBall;
        private VisualEffectsConfigs _visualEffectsConfigs;
        private IControlTheLevel _iControlTheLevel;
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
        LevelConfigs levelConfigs
        ) {
        // Set DI
        _iControlGameplayInput = iControlGameplayInput;
        _iControlMoveTheCamera = iControlMoveTheCamera;
        _iControlMoveTheBall = iControlMoveTheBall;
        _iControlRenderTheBall = iControlRenderTheBall;
        _visualEffectsConfigs = visualEffectsConfigs;
        _iControlTheLevel = iControlTheLevel;
        _levelConfigs = levelConfigs;
    }

    private void Awake() {
        // Generate level
        StartCoroutine(StageByStageToCreateLevel());

        // Set camera control
        _iControlMoveTheCamera.SetLookedTarget(_iControlMoveTheBall.GetTransformBall());
        _iControlMoveTheCamera.SetMovingActive(true);
        _iControlMoveTheCamera.SetMoveState(CameraMoveState.MovingAfterTheTarget);
    }

    private IEnumerator StageByStageToCreateLevel() {
        _iControlTheLevel.SetCells(_cellsStorage);
        _iControlTheLevel.ShowCells();

        yield return new WaitForSeconds(_visualEffectsConfigs.SpawnDuration);

        _iControlMoveTheBall.SetStartPosition(_levelConfigs.BallStartPosition);
        _iControlRenderTheBall.SpawnEffectEnable();

        yield return new WaitForSeconds(_visualEffectsConfigs.SpawnDuration);

        // Set input active
        _iControlGameplayInput.SetGameplayInputActionMapActive(true);
        _iControlGameplayInput.SetAllGameplayActive(true);
    }
}