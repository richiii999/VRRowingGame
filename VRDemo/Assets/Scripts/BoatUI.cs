using UnityEngine;
using TMPro;

// BoatUI: Contains funcs for activating the boatUI buttons and win/lose text.

public class BoatUI : MonoBehaviour{
    public TextMeshPro timerText = null;
    
    void Start(){
        timerText.text = "Pass the Checkpoint to start!";
        
    }

    public void SetTimerText(float t = 0.0f){
        if (t != 0.0f) timerText.text = "Time: " + (t).ToString("F1");
    }

    public void FinishButton(bool win = true){ // Activate next level (true) / retry (false) button, and the main menu. Called from outside on level finish
        transform.Find("Canvas/FinishGroup/MenuButton").gameObject.SetActive(true); 

        if (win){
            transform.Find("Canvas/FinishGroup/NextLevelButton").gameObject.SetActive(true); 
            timerText.text += ", You Win!";
            timerText.color = Color.yellow;
        }
        else{
            transform.Find("Canvas/FinishGroup/RetryButton").gameObject.SetActive(true);
            timerText.text += ", You Lose!";
            timerText.color = Color.red;

        }
    }
}
