using TMPro;
using UnityEngine;

using static Tools;

// ScoreUI: Attached to BoatUI, updates score each checkpoint, and displays a combined score at the end.


public class ScoreUI : MonoBehaviour{
    public BoatUI boatUI = null;

    // Refs to child text boxes
    private TextMeshProUGUI totalVal = null;
    private TextMeshProUGUI timeVal  = null;
    private TextMeshProUGUI timeInc  = null;
    private TextMeshProUGUI angleVal = null;
    private TextMeshProUGUI angleInc = null;

    private Color incColor = default; // ref to color (idk how to set colors properly, this a dumb way bruh)
    private int R = 127; // Correct color values (light-green: #7FFF7F)
    private int G = 255;
    private int B = 127;
    private int A = 255;

    void Start(){
        // set refs
        totalVal = RefToComp<TextMeshProUGUI>("TotalValue");
        timeVal  = RefToComp<TextMeshProUGUI>("TimeValue");
        timeInc  = RefToComp<TextMeshProUGUI>("TimeInc");
        angleVal = RefToComp<TextMeshProUGUI>("AngleValue");
        angleInc = RefToComp<TextMeshProUGUI>("AngleInc");

        totalVal.transform.parent.gameObject.SetActive(false); // Hide totalScore on start, it shows up when the race is finished

        // timeInc.faceColor = new Color(255, 128, 0, 0); // Hide incs on start
        // angleInc.faceColor = new Color(255, 128, 0, 0);
    }

    void Update(){
        A = timeInc.faceColor.a; // Decrement alpha each frame to fade out the score increments
        if (A > 0) timeInc.faceColor = new Color(R,G,B, A-2); 
        Debug.Log($"{timeInc.faceColor.a}");
    }

    void Score(int time = 0, int angle = 0){
        timeInc.text = $"(+{time})";
        angleInc.text = $"(+{angle})";

        timeVal.text = $"{boatUI.scoreTime}";
        angleVal.text = $"{boatUI.scoreAngle}";
    }
}
