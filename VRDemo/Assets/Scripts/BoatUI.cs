using UnityEngine;
using TMPro;
using Unity.Mathematics;

// BoatUI: Contains funcs for activating the boatUI buttons and win/lose text.

public class BoatUI : MonoBehaviour{
    public GameObject rayToEnableOnFinish = null; // On parent boatObject

    public TextMeshProUGUI timerText = null; // Refs to child objects, set in editor for the prefab
    public GameObject menuButton = null;
    public GameObject nextButton = null;
    public GameObject retryButton = null;
    public GameObject angleNeedle = null;

    private int scoreTime = 0; // How much time-based score (faster than CP.scoreTime) the player accumulated
    private int scoreAngle = 0; // How much angle-based score (lower CP.maxAngle -> higher score) the player accumulated.
    // Total Score is just the sum of these two displayed at the end

    void Start(){
        timerText.text = "Pass the Checkpoint to start!";
        menuButton.SetActive(false);
        nextButton.SetActive(false);
        retryButton.SetActive(false);
        rayToEnableOnFinish.SetActive(false);
    }

    public void SetTimerText(float t = 0.0f){ if (t != 0.0f) timerText.text = "Time: " + t.ToString("F1"); }

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

    public void ResetScore(){ scoreTime = 0; scoreAngle = 0; }

    // timeRatio is just (scoretime / checkpointTime)
    // maxAngle of the boat pointing away from the checkpoint (capped at 90 = perpendicular facing direction)
    public void Score(float timeRatio = 1.0f, float maxAngle = 0.0f){
        scoreTime += (int)(100 * timeRatio ); 
        Debug.Log($"Scored time = {scoreTime}, timeRatio = {timeRatio}");
        
        maxAngle = math.max(maxAngle, 90f);
        scoreAngle += 100 * (int)( (math.min(maxAngle, 90) - 90) / -90); 
        Debug.Log($"Scored ang = {scoreAngle}, maxAng = {maxAngle}");
    }
}
