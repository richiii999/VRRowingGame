using UnityEngine;
using TMPro;

// BoatUI: Contains funcs for activating the boatUI buttons and win/lose text.

public class BoatUI : MonoBehaviour{
    public TextMeshProUGUI timerText = null; // Refs to child objects
    public GameObject menuButton = null;
    public GameObject nextButton = null;
    public GameObject retryButton = null;
    
    
    void Start(){
        if (timerText == null || menuButton == null || nextButton == null || retryButton == null) Debug.LogError("BoatUI missing child connections!");
        
        timerText.text = "Pass the Checkpoint to start!";
        menuButton.SetActive(false);
        nextButton.SetActive(false);
        retryButton.SetActive(false);
    }

    public void SetTimerText(float t = 0.0f){
        if (t != 0.0f) timerText.text = "Time: " + t.ToString("F1");
    }

    public void FinishButton(bool win = true){ // Activate next level (true) / retry (false) button, and the main menu. Called from outside on level finish
        menuButton.SetActive(true); 

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
}
