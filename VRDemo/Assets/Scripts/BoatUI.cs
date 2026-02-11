using UnityEngine;
using TMPro;

// BoatUI: Contains funcs for activating the boatUI buttons and win/lose text.

// TODO: Test if the menu buttons work with raycast hand on finish level.
// TODO add level select orbs to MM prefab, also redo MM prefab platform as one prefab instead of 2

public class BoatUI : MonoBehaviour{
    public Transform boatFrontUIAnchor = null; // refs to boat UI anchors (on boat prefab)
    public Transform boatBackUIAnchor = null;
    public GameObject rayToEnableOnFinish = null; // On parent boatObject

    public TextMeshProUGUI timerTextFront = null; // Refs to child objects, set in editor for the prefab
    public TextMeshProUGUI timerTextBack = null;
    public GameObject menuButton = null;
    public GameObject nextButton = null;
    public GameObject retryButton = null;
    public GameObject angleNeedle = null;

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

    public void SetUIAngle(float a = 0.0f){ // Rotates the AngleNeedle image according to the supplied angle in degrees, (+) = left, (-) = right
        angleNeedle.transform.localEulerAngles = new Vector3(0f, 0f, 33f + a); // 33 is center position
        Debug.Log(a);
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
