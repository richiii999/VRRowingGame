using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.XR;
// MenuActions: Provides Scene Management functions for UI objects

public class PauseMenuAction : MenuActions {
    public static bool GameIsPaused = false;
    
    [Header("UI Canvas")]
    public GameObject pauseMenuUI;

    [Header("Ray interactor on hands")]
    public GameObject rayInteracter;
    private InputDevice targetDevice;
    void Start(){ StartCoroutine(GetDevices(1.0f)); }
    IEnumerator GetDevices(float delayTime){
        yield return new WaitForSeconds(delayTime);
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if(devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

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