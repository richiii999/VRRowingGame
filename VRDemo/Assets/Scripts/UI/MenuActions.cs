using UnityEngine;

using Rowing.Core;

// MenuActions: Provides Scene Management functions for UI objects
// Note: Refactored to use Tools interface for changing scenes & quitting

public class MenuActions : MonoBehaviour{
    public static bool GameIsPaused = false;
    
    [Header("UI Canvas")]
    public GameObject pauseMenuUI;

    [Header("Ray interactor on hands")]
    public GameObject rayInteracter;

    [Header("Scene Selection")]
    public SceneList targetScene;

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
    public void goToMainMenu() { Resume(); Tools.LoadScene(SceneList.MainMenuLevel.ToString()); }

    public void RestartLevel() { Resume(); Tools.LoadScene(targetScene.ToString()); }
    
    public void recenter() {
        Debug.Log("Re-Center Clicked");
    }
    
}