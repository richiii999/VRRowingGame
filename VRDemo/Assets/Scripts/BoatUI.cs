using UnityEngine;
using TMPro;

// BoatUI: Contains funcs for activating the boatUI buttons and win/lose text.

public class BoatUI : MonoBehaviour{
    public Transform boatFrontUIAnchor = null; // refs to boat UI anchors (on boat prefab)
    public Transform boatBackUIAnchor = null;
    public GameObject rayToEnableOnFinish = null; // On parent boatObject

    public TextMeshProUGUI timerTextFront = null; // Refs to child objects
    public TextMeshProUGUI timerTextBack = null;
    public GameObject menuButton = null;
    public GameObject nextButton = null;
    public GameObject retryButton = null;
    
    void Start(){
        timerTextFront.text = "Pass the Checkpoint to start!";
        timerTextBack.text = "Pass the Checkpoint to start!";
        menuButton.SetActive(false);
        nextButton.SetActive(false);
        retryButton.SetActive(false);
        rayToEnableOnFinish.SetActive(false);


        Vector3 UIScale = transform.Find("FrontCanvas").localScale; // Preserve UI scale when reparenting
        boatFrontUIAnchor.localScale = UIScale;
        boatBackUIAnchor.localScale = UIScale;
        transform.Find("FrontCanvas").transform.SetParent(boatFrontUIAnchor, false); // Reparent UI canvases to be in correct positions on boat
        transform.Find("BackCanvas").transform.SetParent(boatBackUIAnchor, false);
    }

    public void SetTimerText(float t = 0.0f){
        if (t != 0.0f) { 
            timerTextFront.text = "Time: " + t.ToString("F1"); 
            timerTextBack.text  = "Time: " + t.ToString("F1"); 
        }
    }

    public void FinishButton(bool win = true){ // Activate next level (true) / retry (false) button, and the main menu & ray. Called from outside on level finish
        menuButton.SetActive(true); 
        rayToEnableOnFinish.SetActive(true);

        if (win){
            nextButton.SetActive(true); 
            timerTextFront.text += "\nYou Win!";
            timerTextFront.color = Color.yellow;
            timerTextBack.text += "\nYou Win!";
            timerTextBack.color = Color.yellow;
        }
        else{
            retryButton.SetActive(true);
            timerTextFront.text += "\nYou Lose!";
            timerTextFront.color = Color.red;
            timerTextBack.text += "\nYou Lose!";
            timerTextBack.color = Color.red;
        }
    }
}
