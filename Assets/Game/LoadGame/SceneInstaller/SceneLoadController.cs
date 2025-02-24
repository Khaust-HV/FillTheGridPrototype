using DG.Tweening;
using UnityEngine.SceneManagement;

public static class SceneLoadController {
    public static void LoadScene(SceneNameType sceneNameType) {
        DOTween.KillAll();

        SceneManager.LoadScene(sceneNameType.ToString());
    }
}

public enum SceneNameType {
    MainMenuScene,
    LevelFirstScene,
    LevelSecondScene,
    LevelThirdScene
}