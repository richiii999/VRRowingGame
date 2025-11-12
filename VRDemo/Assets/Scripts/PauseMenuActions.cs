using UnityEngine;

public class PauseMenuActions : MonoBehaviour {
    public void recalibrate() {
       Debug.Log("Recalibrate Clicked");
    }
    
    public void restartLevel() {
       Debug.Log("Restart Level Clicked");
    }

    public void returnToMainMenu() {
        Debug.Log("Return to Main Menu Clicked");
    }
}
