using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

// MenuActions: Provides Scene Management functions for UI objects

public class MenuActions : MonoBehaviour {
    public static string MainMenuSceneName = "MainMenuLevel";
    public static string LevelSelectSceneName = "LevelSelect";


    /* Main Menu */
    public void GoToLevelSelect() {
        Debug.Log("Level Select Clicked");
        SceneManager.LoadSceneAsync(LevelSelectSceneName); 
    }

    public static void QuitGame() {
        Debug.Log("Quit Clicked");
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}