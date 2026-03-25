using UnityEngine;
using System;
using Unity.Mathematics;

using static Tools;

// CheckpointController: Controls the checkpoints and their timers.
// When trigger a CP, next one's timer starts, when trigger the last one, print the cumulative time.

// Note: The checkpoints must be child objects of this, in order, in the SceneTree

public class CheckpointController : MonoBehaviour{
    public Checkpoint[] checkpoints;
    private int currCPidx = 0; // index instead of reference, to make getting the next one easier.
    
    BoatUI BoatUI = null;
    BoatUI BackUI = null;
    SoundController soundController = null; // To play cheers

    float startTime = 0.00f; // At what time did the 1st CP get crossed?
    public bool finished = false; // Set to true when finished (read from outside to do stuff when level is done)

    private float currAngle = 0.0f; // Angle of the player facing away from each checkpoint (resets per CP). For scoring
    public float maxAngle   = 0.0f; // Max value of ^, reset when player reaches CP
    
    void Start(){
        soundController = RefToComp<SoundController>("SoundController", mustExist: false);
        BoatUI = RefToComp<BoatUI>("BoatUI", mustExist: false); // mustExist: false, ex. NPC testing scene with no player
        if (BoatUI) BackUI = RefToComp<BoatUI>("BackUI");

        // Init checkpoints[] (children of the this gameObj)
        checkpoints = GetComponentsInChildren<Checkpoint>(gameObject);
        if (checkpoints.Length == 0) QuitGame("No checkpoints detected!");
    
        // First Checkpoint is active from start 
        checkpoints[0].isNext = true;
        checkpoints[0].timerTxt.color = new Color(0f,0f,0f,0f); // Hide 1st CP timer

        int i = 1;
        foreach (FlagNum flagGroup in FindObjectsByType<FlagNum>(FindObjectsSortMode.None)) {
            flagGroup.SetFlagText(i.ToString());
            i += 1;
        }
    }

    void Update(){ 
        if (!finished && BoatUI) { // Update BoatUI's timer and angle based on the current checkpoint
            BoatUI.SetTimerText( (startTime == 0.00f) ? 0f : Time.time - startTime); 
            BackUI.SetTimerText( (startTime == 0.00f) ? 0f : Time.time - startTime); 

            currAngle = XZAngleBetween(checkpoints[currCPidx].gameObject, BoatUI.gameObject);
            maxAngle = math.max(maxAngle, math.abs(currAngle));
            BoatUI.SetUIAngle(currAngle);
            BackUI.SetUIAngle(currAngle);
        }
    }

    public void OnCheckpoint(Checkpoint CP){ // Do stuff when a checkpoint is reached
        if (finished) return; // Dont count CPs after finish (ex. NPC beats player)

        CP.isNext = false; // Current checkpoint becomes past
        CP.SetGlowAlpha(0f); 
        if (soundController) soundController.PlayRandomSound("cheer", transform.position.x, transform.position.y, transform.position.z);

        // First CP: Dont count time or score
        if (CP == checkpoints[0]) { 
            startTime = Time.time;
            BoatUI.ResetScore();
            BackUI.ResetScore();
            foreach (NPCRacer NPC in FindObjectsByType<NPCRacer>(FindObjectsSortMode.None)) NPC.UnFreeze();
        } 

        // Middle CP: Activate next CP
        if (CP != checkpoints[checkpoints.Length - 1]) checkpoints[currCPidx += 1].isNext = true; 
        
        // Final CP (may also be first if only 1, thats fine), finish the race
        else FinishRace(true); 
    }

    public Checkpoint GetCP(int idx = -1){ // Returns a CP by index, or currently active CP (default), or null if none/finished
        if (idx < -1 || idx > checkpoints.Length - 1) { Debug.LogWarning("Invalid idx for GetCP()"); return null; } // NPCRacer will hit this when it finishes, thats ok.
        if (idx > -1) return checkpoints[idx]; // Get CP by index
        
        return finished ? null : checkpoints[currCPidx]; // Finished means no currCP
    }

    public Checkpoint GetNextCP(Checkpoint CP){ return GetCP(Array.IndexOf(checkpoints, CP) + 1); }

    public void FinishRace(bool isPlayer){
        if (finished) { Debug.LogError("Multiple FinishRace() call!"); return; } // Only finish once

        for(int i = 0; i < checkpoints.Length; i++) {checkpoints[i].isNext = false; } // Disable all CPs (ex. in case player loses)
        finished = true; // Stop timers
        Debug.Log($"Race Finished, {(isPlayer ? "Player" : "NPC")} wins!");
        if (BoatUI) { BoatUI.Finish(isPlayer); BackUI.Finish(isPlayer); }
    }

    public float ResetMaxAngle() { float a = maxAngle; maxAngle = 0.0f; return a; } // Resets maxAngle, returning the value it was before resetting.
}
