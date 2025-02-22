using UnityEngine.SceneManagement;

public static class SceneLoadController {
    public static void LoadScene(SceneNameType sceneNameType) {
        SceneManager.LoadScene(sceneNameType.ToString());
    }
}

public enum SceneNameType {
    MainMenuScene,
    LevelFirstScene,
    LevelSecondScene,
    LevelThirdScene
}