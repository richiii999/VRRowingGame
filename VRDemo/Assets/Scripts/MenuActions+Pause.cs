using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


#if UNITY_EDITOR
    using UnityEditor;
#endif
    
public class PauseMenuAction : MenuActions {
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;


    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }



    /* Custom Actions */
    public void goToMainMenu() {
        Debug.Log("Return to Main Menu Clicked");
        SceneManager.LoadSceneAsync(MenuActions.MainMenuSceneName); 
    }
    public void recenter() {
        Debug.Log("Re-Center Clicked");
    }
    
    public void goToLevelStart() { // Restart Level 
        Debug.Log("Restart Level Clicked");
    }
}