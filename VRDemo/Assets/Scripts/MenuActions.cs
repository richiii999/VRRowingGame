using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuActions : MonoBehaviour {
    private string MainMenuSceneName = "MainMenu";
    private string LevelSelectSceneName = "LevelSelect";



    /* Main Menu */
    public void goToLevelSelect() {
        Debug.Log("Level Select Clicked");
        SceneManager.LoadSceneAsync(LevelSelectSceneName); 
    }

    public void quitGame() {
        Debug.Log("Quit Clicked");
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }





    /* Pause Menu */
    public void goToMainMenu() {
        Debug.Log("Return to Main Menu Clicked");
        SceneManager.LoadSceneAsync(MainMenuSceneName); 
    }
    public void recenter() {
       Debug.Log("Re-Center Clicked");
    }
    
    public void goToLevelStart() { // Restart Level 
       Debug.Log("Restart Level Clicked");
    }
}