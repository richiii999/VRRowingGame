using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Rowing.Core;

// MenuActions: Provides Scene Management functions for UI objects

public class MenuActions : MonoBehaviour {
    /* Main Menu */
    public void GoToLevelSelect() {
        Debug.Log("Level Select Clicked");
        SceneManager.LoadSceneAsync(SceneList.MainMenuLevel.ToString()); 
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