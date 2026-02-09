using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuActions : MonoBehaviour {
    public static string MainMenuSceneName = "MainMenu";
    public static string LevelSelectSceneName = "LevelSelect";


    /* Main Menu */
    public void goToLevelSelect() {
        Debug.Log("Level Select Clicked");
        SceneManager.LoadSceneAsync(MenuActions.LevelSelectSceneName); 
    }

    public void quitGame() {
        Debug.Log("Quit Clicked");
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}