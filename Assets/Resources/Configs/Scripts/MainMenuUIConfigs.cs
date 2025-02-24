using UnityEngine;

namespace GameConfigs {
    [CreateAssetMenu(menuName = "Configs/MainMenuUIConfigs", fileName = "MainMenuUIConfigs")]
    public sealed class MainMenuUIConfigs : ScriptableObject {
        [field: Header("Base settings")]
        [field: SerializeField] public float DefaultButtonAnimationDuration { get; private set; }
        [field: SerializeField] public float DefaultMenuAnimationDuration { get; private set; }
        // [field: Space(25)]
    }
}