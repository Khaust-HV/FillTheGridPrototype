using UnityEngine;

namespace GameConfigs {
    [CreateAssetMenu(menuName = "Configs/GameplayUIConfigs", fileName = "GameplayUIConfigs")]
    public sealed class GameplayUIConfigs : ScriptableObject {
        [field: Header("Base view settings")]
        [field: SerializeField] public float ButtonBackBeforeActionAnimationDuration { get; private set; }
        // [field: Space(25)]
    }
}