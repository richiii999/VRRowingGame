using UnityEngine;
using TMPro;

using static Tools;

// BoatUI: Contains funcs for activating the boatUI buttons and win/lose text.

public class BoatUI : MonoBehaviour{
    public GameObject rayToEnableOnFinish = null; // On parent boatObject

    public TextMeshProUGUI timerText = null; // Refs to child objects, set in editor for the prefab
    public GameObject menuButton = null;
    public GameObject nextButton = null;
    public GameObject retryButton = null;
    public GameObject angleNeedle = null;
    private ScoreUI scoreUI = null;

    void Start(){
        // set refs
        scoreUI = GetComponentInChildren<ScoreUI>();

        timerText.text = "Pass the Checkpoint to start!";
        menuButton.SetActive(false);
        nextButton.SetActive(false);
        retryButton.SetActive(false);
        rayToEnableOnFinish.SetActive(false);
    }

    public void SetTimerText(float t = 0.0f){ if (t != 0.0f) timerText.text = $"Time: {t:F1}"; }

    public void SetUIAngle(float a = 0.0f){ angleNeedle.transform.localEulerAngles = new Vector3(0f, 90f, a); } // Rotates the AngleNeedle

    public void Finish(bool win = true){ // Activate level / menu button & VR hand ray. Called from outside on level finish
        scoreUI.ShowTotalScore();
        
        menuButton.SetActive(true); 
        rayToEnableOnFinish.SetActive(true);

        // Avoids text bug where NPC wins before player hits 1st CP
        if (!timerText.text.StartsWith("Time: ")) timerText.text = $"Time: 0.0";

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

    // Old Interfaces (dont remove since other stuff depends on it)
    public void ResetScore(){ scoreUI.ResetScore();  scoreUI.ShowTotalScore(false); }
    public void Score(float timeRatio = 0f, float maxAngle = 0f){ scoreUI.CheckpointScore(timeRatio, maxAngle); }
}
