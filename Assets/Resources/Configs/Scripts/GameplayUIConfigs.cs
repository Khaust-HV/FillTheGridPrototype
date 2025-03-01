using UnityEngine;

namespace GameConfigs {
    [CreateAssetMenu(menuName = "Configs/GameplayUIConfigs", fileName = "GameplayUIConfigs")]
    public sealed class GameplayUIConfigs : ScriptableObject {
        [field: Header("Base settings")]
        [field: SerializeField] public float DefaultButtonAnimationDuration { get; private set; }
        [field: Space(25)]

        [field: Header("Base view settings")]
        [field: SerializeField] public float ButtonBackBeforeActionAnimationDuration { get; private set; }
        [field: Space(25)]

        [field: Header("Mini menu view settings")]
        [field: SerializeField] public float MiniMenuAnimationDuration { get; private set; }
        [field: Space(25)]

        [field: Header("Black window view settings")]
        [field: SerializeField] public float BlackWindowAnimationDuration { get; private set; }
    }
}