using UnityEngine;
using System.Collections.Generic;


public class MainMenuManager : MonoBehaviour {
    [Header("Game Camera")]
    public GameObject gameCamera;


    [Header("Main Menu Camera")]
    public GameObject mainMenuCamera;


    [Header("UI")]
    public GameObject menuCanvas;


    [Header("Game State Management")]
    [Tooltip("Drag objects here you want to be disabled while in the menu")]
    public List<GameObject> objectsToDisableInMenu;


    [Header("DEBUG- Auto Start Level")]
    public bool skipMainMenu;


    //MARK: - Start
    void Start() {
        if (skipMainMenu == true) {
            StartGame();
        } else {
            EnterMenuMode();
        }
    }


    // MARK: - Enter Menu Mode
    // Teleport player to menu spawn point
    // Turn on Menu UI
    // Toggle Game Systems off
    public void EnterMenuMode() {
        menuCanvas?.SetActive(true);
        ToggleGameSystem(false);
    }


    //MARK: - Start Game
    // Teleport player to game spawn point
    // Turn off Menu UI
    // Toggle Game Systems on
    public void StartGame() {
        menuCanvas?.SetActive(false);
        ToggleGameSystem(true);
    }


    //MARK: - Toggle Game System
    // will set all objects in our objectsToDisableInMenu list equal to state
    private void ToggleGameSystem(bool state) {
        gameCamera?.SetActive(state);
        mainMenuCamera?.SetActive(!state);

        foreach (GameObject obj in objectsToDisableInMenu) {
            obj?.SetActive(state);
        }
    }
}
