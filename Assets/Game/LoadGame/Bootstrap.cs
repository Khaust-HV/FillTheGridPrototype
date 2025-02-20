using UnityEngine;
using Zenject;

public sealed class Bootstrap : MonoBehaviour { // Entry point pattern
    #region DI
        IControlGameplayInput _iControlGameplayInput;
        IControlMoveTheCamera _iControlMoveTheCamera;
        IControlMoveTheBall _iControlMoveTheBall;
    #endregion

    [Inject]
    private void Construct (
        IControlGameplayInput iControlGameplayInput, 
        IControlMoveTheCamera iControlMoveTheCamera, 
        IControlMoveTheBall iControlMoveTheBall
        ) {
        // Set DI
        _iControlGameplayInput = iControlGameplayInput;
        _iControlMoveTheCamera = iControlMoveTheCamera;
        _iControlMoveTheBall = iControlMoveTheBall;
    }

    private void Awake() {
        // Generate level
        // _iGenerateLevel.GenerateLevel();

        // Set camera control
        _iControlMoveTheCamera.SetLookedTarget(_iControlMoveTheBall.GetTransformBall());
        _iControlMoveTheCamera.SetMovingActive(true);
        _iControlMoveTheCamera.SetMoveState(CameraMoveState.MovingAfterTheTarget);

        // Set input active
        _iControlGameplayInput.SetGameplayInputActionMapActive(true);
        _iControlGameplayInput.SetAllGameplayActive(true);
    }
}