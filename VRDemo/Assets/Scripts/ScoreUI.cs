using TMPro;
using UnityEngine;

using static Tools;

// ScoreUI: Attached to BoatUI, updates score each checkpoint, and displays a combined score at the end.


public class ScoreUI : MonoBehaviour{
    public BoatUI boatUI = null;

    // Refs to child text boxes
    private TextMeshPro totalScoreValue = RefToComp<TextMeshPro>("TotalScoreValueText");
    private TextMeshPro timerScore      = RefToComp<TextMeshPro>("TimeScoreText");
    private TextMeshPro timerInc        = RefToComp<TextMeshPro>("TimeIncText");
    private TextMeshPro angleScore      = RefToComp<TextMeshPro>("AngleScoreText");
    private TextMeshPro angleInc        = RefToComp<TextMeshPro>("AngleIncText");

    void Start(){
        totalScoreValue.gameObject.SetActive(false); // Hide totalScore on start, it shows up when the race is finished

        timerInc.faceColor = new Color32(255, 128, 0, 0); // Hide incs on start
    }

    void Update(){
        
    }
}
