using UnityEngine;
using UnityEngine.SceneManagement;

// MenuActions: Provides Scene Management functions for UI objects

public class PauseMenuAction : MenuActions {
    public static bool GameIsPaused = false;
    
    [Header("UI Canvas")]
    public GameObject pauseMenuUI;

    [Header("Ray interactor on hands")]
    public GameObject rayInteracter;


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
        rayInteracter.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void Pause() {
        pauseMenuUI.SetActive(true);
        rayInteracter.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }



    /* Custom Actions */
    public void goToMainMenu() {
        Debug.Log("Return to Main Menu Clicked");
        SceneManager.LoadSceneAsync(MenuActions.MainMenuSceneName); 
        Resume();
    }
    public void recenter() {
        Debug.Log("Re-Center Clicked");
    }
    
    public void goToLevelStart() { // Restart Level 
        Debug.Log("Restart Level Clicked");
        Resume();
    }
}