using UnityEngine;

namespace GameConfigs {
    [CreateAssetMenu(menuName = "Configs/BallAndCellConfigs", fileName = "BallAndCellConfigs")]
    public sealed class BallAndCellConfigs : ScriptableObject {
        [field: Header("Ball move settings")]
        [field: SerializeField] public float StepSize { get; private set; }
        [field: SerializeField] public float MoveDuration { get; private set; }

        [field: Header("Cell base settings")]
        [field: SerializeField] public float PaintDuration { get; private set; }
    }
}