using UnityEngine;

namespace Managers {
    public sealed class LevelManager : IControlTheLevel {
        public bool IsMovementPossible(Vector3 position) {
            return true;
        }

        public void ColorTheCell(Vector3 position) {

        }
    }
}

public interface IControlTheLevel {
    public bool IsMovementPossible(Vector3 position);
    public void ColorTheCell(Vector3 position);
}