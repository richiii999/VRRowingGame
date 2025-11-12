using System.Diagnostics;

public class PauseMenuActions : MonoBehaviour {
    public void recalibrate() {
       Debug.log("Recalibrate Clicked");
    }
    
    public void restartLevel() {
       Debug.log("Restart Level Clicked");
    }

    public void returnToMainMenu() {
        Debug.log("Return to Main Menu Clicked");
    }
}
