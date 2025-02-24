using UnityEngine;

namespace GameConfigs {
    [CreateAssetMenu(menuName = "Configs/BallAndCellConfigs", fileName = "BallAndCellConfigs")]
    public sealed class BallAndCellConfigs : ScriptableObject {
        [field: Header("Ball move settings")]
        [field: SerializeField] public float StepSize { get; private set; }
        [field: SerializeField] public float MoveDuration { get; private set; }
        [field: Space(25)]

        [field: Header("Cell base settings")]
        [field: SerializeField] public float PaintDuration { get; private set; }
        [field: Space(25)]

        [field: Header("Ball skins settings")]
        [field: SerializeField] public BallSkinType[] AllSkinsBall { get; private set; }
        [field: SerializeField] public int GreenSkinCost { get; private set; }
        [field: SerializeField] public int OrangeSkinCost { get; private set; }
    }
}