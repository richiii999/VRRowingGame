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

    public void SetUIAngle(float a = 0.0f){ // Rotates the AngleNeedle image according to the supplied angle, (+) = left, (-) = right
        // a 
        Vector3 rotation = new Vector3(0f,0f, a - angleNeedle.transform.localRotation.z); // 33 is the center value
        // Not in degrees idk

        angleNeedle.transform.Rotate(rotation, Space.Self);
        Debug.Log("a = " + a.ToString() + "localRotZ = " + angleNeedle.transform.localRotation.z.ToString());

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
