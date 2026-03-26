using UnityEngine;
using System.Collections.Generic;

// MainMenuManager: Provides Scene Management functions for the main menu

// TODO: Still refactoring this, need to have the platform enabled for each level, so you can overlook the level on start in VR

public class MainMenuManager : MonoBehaviour {
    [Header("Game Camera")]
    public GameObject gameCamera;


    [Header("Main Menu Camera")]
    public GameObject mainMenuCamera;


    [Header("UI")]
    public GameObject menuCanvas;

    [Header("Level Select UI")]
    public GameObject levelSelectUI;


    [Header("Game State Management")]
    [Tooltip("Drag objects here you want to be disabled while in the menu")]
    public List<GameObject> objectsToDisableInMenu;


    [Header("DEBUG- Auto Start Tutorial")]
    public bool skipMainMenu;


    //MARK: - Start
    void Start() {
        if (skipMainMenu == true) {
            StartTutorial();
        } else {
            EnterMenuMode();
        }
    }


    // MARK: - Enter Menu Mode
    // Teleport player to menu spawn point
    // Turn on Menu UI
    // Toggle Game Systems off
    public void EnterMenuMode() {
        Debug.Log("Enter Menu Mode Tapped");
        ToggleGameSystem(false);
    }


    //MARK: - Start Tutorial
    // Teleport player to tutorial spawn point
    // Turn off Menu UI
    // Toggle Game Systems on
    public void StartTutorial() {
        // Debug.Log("Start Tutorial Tapped");
        ToggleGameSystem(true);
    }

    // MARK: - Show level select
    // Turn off Menu UI
    // Turn on Level Select UI
    public void ShowLevelSelect() {
       Debug.Log("Show Level Select");
    }


    //MARK: - Toggle Game System
    // will set all objects in our objectsToDisableInMenu list equal to state
    // will either turn fully off or turn fully on menu ui
    // State = True (Start Game); State = False (Toggle Menu)
    private void ToggleGameSystem(bool state) { 
        if (menuCanvas != null) menuCanvas?.SetActive(!state);
        if (mainMenuCamera != null) mainMenuCamera?.SetActive(!state);
        if (levelSelectUI != null) levelSelectUI.SetActive(!state);

        if (gameCamera != null) gameCamera?.SetActive(state);

        foreach (GameObject obj in objectsToDisableInMenu) {
            obj?.SetActive(state);
        }
    }

    public void QuitGame() { Tools.QuitGame(); }
}
