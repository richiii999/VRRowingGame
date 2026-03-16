using UnityEngine;
using TMPro;
using Unity.Mathematics;

// BoatUI: Contains funcs for activating the boatUI buttons and win/lose text.

// TODO: Test if the menu buttons work with raycast hand on finish level.
// TODO add level select orbs to MM prefab, also redo MM prefab platform as one prefab instead of 2

public class BoatUI : MonoBehaviour{
    public GameObject rayToEnableOnFinish = null; // On parent boatObject

    public TextMeshProUGUI timerText = null; // Refs to child objects, set in editor for the prefab
    public GameObject menuButton = null;
    public GameObject nextButton = null;
    public GameObject retryButton = null;
    public GameObject angleNeedle = null;

    private int scoreTime = 0; // How much extra time (above Scoretime per CP) the player accumulated
    private int angleGrade = 0; // How many degrees above straightline (per CP) the player was.
    // Total Score is just the sum of these two displayed at the end

    void Start(){
        timerText.text = "Pass the Checkpoint to start!";
        menuButton.SetActive(false);
        nextButton.SetActive(false);
        retryButton.SetActive(false);
        rayToEnableOnFinish.SetActive(false);
    }

    public void SetTimerText(float t = 0.0f){
        if (t != 0.0f) { 
            timerText.text = "Time: " + t.ToString("F1"); 
        }
    }

    public void SetUIAngle(float a = 0.0f){ angleNeedle.transform.localEulerAngles = new Vector3(0f, 90f, a); } // Rotates the AngleNeedle

    public void FinishButton(bool win = true){ // Activate level / menu button & VR hand ray. Called from outside on level finish
        menuButton.SetActive(true); 
        rayToEnableOnFinish.SetActive(true);

        if (win){
            nextButton.SetActive(true); 
            timerText.text += "\nYou Win!";
            timerText.color = Color.yellow;
        }
        else{
            retryButton.SetActive(true);
            timerText.text += "\nYou Lose!";
            timerText.color = Color.red;
        }
    }

    // ratio should usually be (checkpointTime / scoretime), and gets capped at 1 (so faster != more score)
    public void ScoreByTimeRatio(float ratio = 1.0f){scoreTime += 100 * (int)( math.max(ratio, 1.0) ); }
    
    // maxAngle of the boat pointing away from the checkpoint (capped at 90 = perpendicular facing direction)
    public void ScoreByAngleGrade(float maxAngle = 0.0f){angleGrade += 100 * (int)( (math.max(maxAngle, 90) - 90) / -90); }
}
