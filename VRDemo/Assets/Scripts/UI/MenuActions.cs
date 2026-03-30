using UnityEngine;

using Rowing.Core;
using Unity.XR.CoreUtils;
using System.Collections;
using static Tools;

// MenuActions: Provides Scene Management functions for UI objects
// Setup: The buttons under 'Pause Menu' should have their click event set to their resepective functions in this script (ex. ResumeButton -> Resume())

// Note for Mav: Refactored to use Tools interface for changing scenes & quitting

public class MenuActions : MonoBehaviour {
    public bool GameIsPaused = false;

    [Header("Ray interactor on hands")]
    public GameObject rayInteracter;

    public XROrigin xrOrigin;
    public Transform boatTF;
    public Vector3 offset;
    void Start() { ToggleActive(false); } // Hidden by default (leave active in editor)

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) Resume();
            else Pause();
        }
    }

    void ToggleActive(bool state){ // True means pause menu is shown, false means it is hidden
        foreach (GameObject child in GetAllChildren(gameObject)) child.SetActive(state);
        rayInteracter.SetActive(state);
    }

    public void Resume() {
        ToggleActive(false);

        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void Pause() {
        ToggleActive(true);

        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }

    public void MainMenu() { Resume(); LoadScene(SceneList.MainMenuLevel.ToString()); }

    public void RestartLevel() { Resume(); LoadScene(GetCurrScene()); }
    
    public void RecalibrateVR() {
        Debug.Log("Recalibrate Clicked (does nothing for now)");
        // TODO: Recalibrate the VR (borrow from hayden's function somewhere idk)
        // Actually sorry, I think I broke what this button was attached to before, something in XRController, that was probably correct sry
        xrOrigin.MoveCameraToWorldLocation(boatTF.position + offset); 
    }
    
}