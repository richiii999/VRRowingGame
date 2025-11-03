using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

// CheckpointController.cs: Controls the checkpoints.
// When a checkpoint is triggered, stuff happens.
// When trigger the first one, a timer starts, when trigger the last one, it ends.

// Note: There must be atleast 2 checkpoints for it to have a proper start / finish.
// Note: The checkpoints must be in order in the Scene Tree

public class CheckpointController : MonoBehaviour{
    public List<GameObject> checkpoints; // Stores references to each of the checkpoint gameObjects

    void Start(){
        // First, find all the checkpoints (children of the 'Checkpoints' gameObj)
        for (int i = 0; i < transform.childCount; i++) checkpoints.Add(transform.GetChild(i).gameObject);
        if (checkpoints.Count < 2) Debug.LogWarning("CheckpointController: Less than 2 checkpoints in level");

        // In each checkpoint (except the last), set 'nextCheckpoint' to the one after it 
        for (int i = 0; i < checkpoints.Count - 1; i++) checkpoints[i].GetComponent<Checkpoint>().nextCheckpoint = checkpoints[i + 1];
    
        // First Checkpoint: Activate immediately (but hide timer)
        checkpoints[0].GetComponent<Checkpoint>().isNext = true;
        checkpoints[0].GetComponentInChildren<TMP_Text>().color = new Color(0f,0f,0f,0f);
    }

    public void OnCheckpoint(GameObject CP){ // Do stuff when a checkpoint is reached
        Debug.Log("Checkpoint passed: " + CP.name);

        CP.GetComponent<Checkpoint>().isNext = false; // Current checkpoint becomes past
        CP.GetComponent<Checkpoint>().R.material.color = new Color(0f,0f,0f,0f);
        
        if (checkpoints.IndexOf(CP) == 0) { // First Checkpoint: Activate effects
        
        } 

        if (CP.GetComponent<Checkpoint>().nextCheckpoint) { // Next checkpoint
            checkpoints[checkpoints.IndexOf(CP) + 1].GetComponent<Checkpoint>().isNext = true;
        }
        else { // No next checkpoint (Player reached finish)
            Debug.Log("Final Checkpoint passed");
        }

        /* Do stuff */
        // Idea: Animation or some kind of visual change when a checkpoint is passed
        // First: Start the timer, weather effects, music change, etc.
        // Last: End the timer, effects, etc.

        /* Crazy level idea: 2 parrallel lanes of checkpoints (A & B), 
         * each lane 1 CP is removed from the checkpoints child list to elsewhere in tree (when loading the level)
         * however it is RANDOM, so the level is different each time
         * I wonder if easy to implement, and how fun it would be */

         // IDEA: Each checkpoint has a floating timer text above it, which stops when that checkpoint is passed.
         // only THAT checkpoint's timer stops
         // Then, when level done, tp to room or something and see all the texts above the timer
    }
}
