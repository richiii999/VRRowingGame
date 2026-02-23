using UnityEngine;
using System.Collections.Generic;
using System.Linq;

using static Tools;

// CheckpointController.cs: Controls the checkpoints and their timers.
// When trigger a CP, next one's timer starts, when trigger the last one, print the cumulative time.

// Note: The checkpoints must be child objects of this, in order, in the SceneTree

public class CheckpointController : MonoBehaviour{
    public List<Checkpoint> checkpoints;
    private int currCPidx = 0;
    
    private BoatUI BoatUI = null;
    public SoundController soundController = null; // To play cheers

    public float totalTime = 0.00f; // Total time spent on all checkpoints (except 1st)
    public float startTime = 0.00f; // At what time did the 1st CP get crossed?
    public bool finished = false; // Set to true when finished (read from outside to do stuff when level is done)
    

    void Start(){
        soundController = RefToComp<SoundController>("SoundController");
        BoatUI = RefToComp<BoatUI>("BoatUI", false); // mustExist=false, ex. NPC testing scene with no player

        // Init checkpoints[] (children of the this gameObj)
        for (int i = 0; i < transform.childCount; i++) checkpoints.Add(transform.GetChild(i).GetComponent<Checkpoint>());
        if (checkpoints.Count == 0) QuitGame("No checkpoints detected!");
    
        // First Checkpoint is active from start (but ignored timer)
        checkpoints[0].isNext = true;
        checkpoints[0].timerTxt.color = new Color(0f,0f,0f,0f);
    }

    void Update(){ 
        if (!finished && BoatUI) { // Update BoatUI's timer and angle based on the current checkpoint
            BoatUI.SetTimerText( (startTime != 0.00f) ? Time.time - startTime : 0f ); 
            BoatUI.SetUIAngle(XZAngleBetween(checkpoints[currCPidx].gameObject, BoatUI.gameObject));
        }
    }

    public void OnCheckpoint(Checkpoint CP, bool playerOrNPC = true){ // Do stuff when a checkpoint is reached
        float newTime = CP.GetTime();
        totalTime += newTime;

        CP.isNext = false; // Current checkpoint becomes past
        CP.SetGlowAlpha(0f); 
        soundController.PlayRandomSound("cheer", transform.position.x, transform.position.y, transform.position.z);

        if (CP == checkpoints[0]) { totalTime -= newTime; startTime = Time.time; } // First CP: Dont count time
        if (CP != checkpoints.Last()) checkpoints[currCPidx += 1].isNext = true; // Middle CP: Activate next
        else { // Final CP (may also be first if only 1, thats fine)
            Debug.Log("Final Checkpoint passed," + " Time = " + newTime + " totalTime = " + totalTime);
            finished = true; // Stop timers
            if (BoatUI) BoatUI.FinishButton(playerOrNPC);
        }
    }

    public Checkpoint GetCP(int idx = -1){ // Returns a CP by index, or currently active CP (default), or null if none/finished
        if (idx < -1 || idx > checkpoints.Count - 1) { Debug.LogWarning("Invalid idx for GetCP()"); return null; }
        if (idx > -1) return checkpoints[idx]; // Get CP by index
        
        return finished ? null : checkpoints[currCPidx]; // Finished means no currCP
    }

    public Checkpoint GetNextCP(Checkpoint CP){ return GetCP(checkpoints.IndexOf(CP) + 1); }
}
