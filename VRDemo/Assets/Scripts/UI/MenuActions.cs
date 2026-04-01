using UnityEngine;

using Rowing.Core;
using Unity.XR.CoreUtils;
using static Tools;

// MenuActions: Provides Scene Management functions for UI objects
// Setup: The buttons under 'Pause Menu' should have their click event set to their resepective functions in this script (ex. ResumeButton -> Resume())

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
        
        if (Input.GetAxisRaw("XRI_Left_Trigger") > 0.5f && !GameIsPaused) Pause();
    }

    void ToggleActive(bool state){ // True means pause menu is shown, false means it is hidden
        foreach (GameObject child in GetAllChildren(gameObject)) child.SetActive(state);
        rayInteracter.SetActive(state);

        Time.timeScale = state ? 0.0f : 1.0f;
        GameIsPaused = state;
    }

    // Button Funcs
    public void Resume() { ToggleActive(false); }
    public void Pause() { ToggleActive(true); }
    public void MainMenu() { Resume(); LoadScene(SceneList.MainMenuLevel.ToString()); }
    public void RestartLevel() { Resume(); LoadScene(GetCurrScene()); }
    public void RecalibrateVR() { xrOrigin.MoveCameraToWorldLocation(boatTF.position + offset); }
}