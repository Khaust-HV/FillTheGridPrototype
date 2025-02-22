using UnityEngine;

namespace GameConfigs {
    [CreateAssetMenu(menuName = "Configs/LevelConfigs", fileName = "LevelConfigs")]
    public sealed class LevelConfigs : ScriptableObject {
        [field: Header("Raycast check cell settings")]
        [field: SerializeField] public LayerMask CellLayer { get; private set; }
        [field: SerializeField] public float RayCheckCellDistance { get; private set; }
        [field: Space(25)]

        [field: Header("Level start settings")]
        [field: SerializeField] public Vector3 BallStartPosition { get; private set; }
        [field: SerializeField] public int CoinNumberOnLevel { get; private set; }
    }
}