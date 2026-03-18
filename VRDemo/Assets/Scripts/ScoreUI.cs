using TMPro;
using UnityEngine;
using Unity.Mathematics;
using static Tools;

// ScoreUI: Attached to BoatUI, updates scores each checkpoint, and displays a combined score at the end.
// Note: The public CheckpointScore() should be called by CheckpointController, while the private UpdateScoreUI() is what actually sets the values

public class ScoreUI : MonoBehaviour{
    // Refs to child text boxes
    TextMeshProUGUI totalVal = null;
    TextMeshProUGUI timeVal  = null;
    TextMeshProUGUI timeInc  = null;
    TextMeshProUGUI angleVal = null;
    TextMeshProUGUI angleInc = null;

    // Correct color values (light-green: #7FFF7F), idk how to set colors properly, this a dumb way bruh
    readonly byte R = 127; 
    readonly byte G = 255;
    readonly byte B = 127;

    int scoreTime = 0; // How much time-based score (faster than CP.scoreTime) the player accumulated
    int scoreAngle = 0; // How much angle-based score (lower CP.maxAngle -> higher score) the player accumulated.
    // Total Score is just the sum of these two displayed at the end

    void Start(){
        // set refs
        totalVal = RefToComp<TextMeshProUGUI>("TotalValue");
        timeVal  = RefToComp<TextMeshProUGUI>("TimeValue");
        timeInc  = RefToComp<TextMeshProUGUI>("TimeInc");
        angleVal = RefToComp<TextMeshProUGUI>("AngleValue");
        angleInc = RefToComp<TextMeshProUGUI>("AngleInc");

        // Hide stuff on start
        totalVal.transform.parent.gameObject.SetActive(false); 
        IncrementScore();
        timeInc.faceColor  = new Color32(R, G, B, 0);
        angleInc.faceColor = new Color32(R, G, B, 0);
    }

    void Update(){ 
        if (timeInc.faceColor.a > 0) { // Fade out the score increments
            timeInc.faceColor = new Color32(R, G, B, (byte)(timeInc.faceColor.a - 1)); 
            angleInc.faceColor = timeInc.faceColor;
        } 
    }

    // Updates score by player performance each CP
    public void CheckpointScore(float timeRatio = 1.0f, float maxAngle = 0.0f){
        // timeRatio is just (scoretime / checkpointTime)
        // maxAngle of the boat pointing away from the checkpoint's fwd in XZ-plane
        
        int scoreTimeInc = (int)(100 * timeRatio);
        
        maxAngle = math.abs(maxAngle); // direction doesnt matter
        int scoreAngleInc = (int)(100 * (1.0f - (maxAngle / 180f)));
        
        IncrementScore(scoreTimeInc, scoreAngleInc);
    }

    // Sets all score to 0
    public void ResetScore(){
        scoreTime  = 0;
        scoreAngle = 0;

        IncrementScore();
        totalVal.text = $"{scoreTime + scoreAngle}"; // In case totalScore is visible (shouldnt happen I dont think)
    }

    // Updates the score values and UI
    private void IncrementScore(int timeIncAmt = 0, int angleIncAmt = 0){
        scoreTime  += timeIncAmt;
        scoreAngle += angleIncAmt;
        
        timeInc.text  = $"(+{timeIncAmt})";
        angleInc.text = $"(+{angleIncAmt})";
        timeInc.faceColor  = new Color32(R, G, B, 255);
        angleInc.faceColor = new Color32(R, G, B, 255);
        
        timeVal.text  = $"{scoreTime}";
        angleVal.text = $"{scoreAngle}";
    }

    public void ShowTotalScore(bool state){
        totalVal.transform.parent.gameObject.SetActive(true); 
        totalVal.text = $"{scoreTime + scoreAngle}";
    }
}
