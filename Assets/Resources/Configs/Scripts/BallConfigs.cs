using UnityEngine;

namespace GameConfigs {
    [CreateAssetMenu(menuName = "Configs/BallConfigs", fileName = "BallConfigs")]
    public sealed class BallConfigs : ScriptableObject {
        [field: Header("Ball move settings")]
        [field: SerializeField] public float StepSize { get; private set; }
        [field: SerializeField] public float MoveDuration { get; private set; }
    }
}