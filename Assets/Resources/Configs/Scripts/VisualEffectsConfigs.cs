using UnityEngine;

namespace GameConfigs {
    [CreateAssetMenu(menuName = "Configs/VisualEffectsConfigs", fileName = "VisualEffectsConfigs")]
    public sealed class VisualEffectsConfigs : ScriptableObject {
        [field: Header("Cell material settings")]
        [field: SerializeField] public Material CellMaterial { get; private set; }
        [field: SerializeField] public Color CellBaseColor { get; private set; }
        [field: SerializeField] public float StartCutoffHeight { get; private set; }
        [field: SerializeField] public float FinishCutoffHeight { get; private set; }
        [field: Space(25)]

        [field: Header("Spawn settings")]
        [field: SerializeField] public float SpawnDuration { get; private set; }
        [field: Space(25)]

        [field: Header("Ball skin settings <Default>")]
        [field: SerializeField] public Material DefaultSkinMaterial { get; private set; }
        [field: SerializeField] public Color DefaultSkinColor { get; private set; }
    }
}