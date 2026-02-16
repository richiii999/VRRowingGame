using UnityEngine;
using System.Collections.Generic;
using TMPro;

// CheckpointController.cs: Controls the checkpoints and their timers.
// When trigger a CP, next one's timer starts, when trigger the last one, print the cumulative time.

// Note: There must be atleast 2 checkpoints for it to have a proper start / finish.
// Note: The checkpoints must be in order in the Scene Tree

public class CheckpointController : MonoBehaviour{
    public List<GameObject> checkpoints; // Stores references to each of the checkpoint gameObjects
    public GameObject currCP = null;
    
    private BoatUI BoatUI = null; // Ref Player's BoatUI
    private SoundController soundController = null; // Ref to the level's SoundController to play cheers

    public float totalTime = 0.00f; // Total time spent on all checkpoints (except 1st)
    public float startTime = 0.00f; // At what time did the 1st CP get crossed?
    public bool finished = false; // Set to true when finished
    

    void Start(){
        // Connections to other objects (if present, not all are required)
        GameObject searchObj = GameObject.Find("SoundController");
        if (searchObj == null) Debug.LogWarning("No soundcontroller detected");
        else soundController = searchObj.GetComponent<SoundController>();

        searchObj = GameObject.Find("BoatUI");
        if (searchObj == null) Debug.LogError("Cannot find BoatUI Object!");
        else BoatUI = searchObj.GetComponent<BoatUI>();

        // First, find all the checkpoints (children of the 'Checkpoints' gameObj)
        for (int i = 0; i < transform.childCount; i++) checkpoints.Add(transform.GetChild(i).gameObject);
        if (checkpoints.Count < 2) Debug.LogWarning("CheckpointController: Less than 2 checkpoints in level");
        else currCP = checkpoints[0];

        // In each checkpoint (except the last), set 'nextCheckpoint' to the one after it 
        for (int i = 0; i < checkpoints.Count - 1; i++) checkpoints[i].GetComponent<Checkpoint>().nextCheckpoint = checkpoints[i + 1];
    
        // First Checkpoint is active from start (but ignored timer)
        checkpoints[0].GetComponent<Checkpoint>().isNext = true;
        checkpoints[0].GetComponentInChildren<TMP_Text>().color = new Color(0f,0f,0f,0f);
    }

    void Update(){ 
        if (!finished && BoatUI) { // Update BoatUI's timer and angle based on the current checkpoint
            BoatUI.SetTimerText( (startTime != 0.00f) ? Time.time - startTime : 0f ); 
            BoatUI.SetUIAngle(currCP.GetComponent<Checkpoint>().GetRelativeAngle(BoatUI.gameObject));
        }
    }

    public void OnCheckpoint(GameObject CP, bool playerOrNPC = true){ // Do stuff when a checkpoint is reached
        float newTime = CP.GetComponent<Checkpoint>().GetTime();
        totalTime += newTime;

        CP.GetComponent<Checkpoint>().isNext = false; // Current checkpoint becomes past
        CP.GetComponent<Checkpoint>().R.material.color = new Color(0f,0f,0f,0f);

        // Play a sound from soundEffects (randomly chosen, if any)
        if (soundController != null) soundController.PlayRandomSound("cheer");
        
        if (checkpoints.IndexOf(CP) == 0) { // First Checkpoint: Activate effects
            totalTime -= newTime; // Negative time for first CP, since we dont count it
            startTime = Time.time; // Start the actual timer now
        }

        if (CP.GetComponent<Checkpoint>().nextCheckpoint) { // Next checkpoint
            checkpoints[checkpoints.IndexOf(CP) + 1].GetComponent<Checkpoint>().isNext = true;
            currCP = CP.GetComponent<Checkpoint>().nextCheckpoint;
            // Debug.Log("Checkpoint passed: " + CP.name + " Time = " + newTime + " totalTime = " + totalTime);
        }
        else { // No next checkpoint (Player reached finish)
            Debug.Log("Final Checkpoint passed," + " Time = " + newTime + " totalTime = " + totalTime);
            
            finished = true; // Stop timers

            if (BoatUI) BoatUI.FinishButton(playerOrNPC);
        }
    }

    public GameObject GetCP(int idx = -1){ // Returns a CP by index, or currently active CP (default), or null if none/finished
        if (idx < -1 || idx > checkpoints.Count - 1) { Debug.LogWarning("Invalid idx for GetCP()"); return null; }
        if (idx > -1) return checkpoints[idx]; // Get CP by index
        
        if (finished) return null; // No currCP, race finished
        else foreach (GameObject CP in checkpoints){ if (CP.GetComponent<Checkpoint>().isNext) return CP; }
        Debug.LogWarning("Cannot find currCP!"); return null; // Should never exit loop ^ without returning
    }

    public GameObject GetNextCP(int idx = -1){ // Returns the CP after the idx / currCP, or null if invalid idx/none/finished/lastCP
        GameObject CP = GetCP(idx);
        return (CP == null) ? null : CP.GetComponent<Checkpoint>().nextCheckpoint; // Null if no currCP, else nextCP (which may also be null if curr is final CP)
    }
}
