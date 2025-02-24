using UnityEngine;

namespace GameConfigs {
    [CreateAssetMenu(menuName = "Configs/VisualEffectsConfigs", fileName = "VisualEffectsConfigs")]
    public sealed class VisualEffectsConfigs : ScriptableObject {
        [field: Header("Cell material settings")]
        [field: SerializeField] public Material CellMaterial { get; private set; }
        [field: SerializeField] public Color CellBaseColor { get; private set; }
        [field: SerializeField] public float CellStartCutoffHeight { get; private set; }
        [field: SerializeField] public float CellFinishCutoffHeight { get; private set; }
        [field: Space(25)]

        [field: Header("Spawn settings")]
        [field: SerializeField] public float SpawnDuration { get; private set; }
        [field: SerializeField] public float BallDissolveSpawnDuration { get; private set; }
        [field: Space(25)]

        [field: Header("Ball skin settings")]
        [field: SerializeField] public Material DefaultSkinMaterial { get; private set; }
        [field: SerializeField] public float BallStartCutoffHeight { get; private set; }
        [field: SerializeField] public float BallFinishCutoffHeight { get; private set; }
        [field: SerializeField] public Color DefaultSkinColor { get; private set; }
        [field: SerializeField] public Color GreenSkinColor { get; private set; }
        [field: SerializeField] public Color OrangeSkinColor { get; private set; }
        // Any skins...
        // [field: Space(25)]
    }
}