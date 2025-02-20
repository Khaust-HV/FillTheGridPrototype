using UnityEngine;

namespace GameConfigs {
    [CreateAssetMenu(menuName = "Configs/CameraConfigs", fileName = "CameraConfigs")]
    public sealed class CameraConfigs : ScriptableObject {
        [field: Header("Camera move settings")]
        [field: SerializeField] public float MovingSmoothSpeed { get; private set; }
        [field: Space(25)]

        [field: Header("Camera zoom settings")]
        [field: SerializeField] public float ZoomingSmoothSpeed { get; private set; }
        [field: SerializeField] public float ZoomDuration { get; private set; }
        [field: SerializeField] public float SensitivityZoom { get; private set; }
        [field: SerializeField] public float MinZoomScale { get; private set; }
        [field: SerializeField] public float MaxZoomScale { get; private set; }
    }
}