using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Formats.Fbx.Exporter;
using Unity.XR.CoreUtils;

// CheckpointController.cs: Controls the checkpoints.
// When a checkpoint is triggered, stuff happens.
// When trigger the first one, a timer starts, when trigger the last one, it ends.

// Note: There must be atleast 2 checkpoints for it to have a proper start / finish.
// Note: The checkpoints must be in order in the Scene Tree

public class CheckpointController : MonoBehaviour{
    public List<GameObject> checkpoints; // Stores references to each of the checkpoint gameObjects

    private GameObject BoatUI; // Reference to BoatUI
    public TMP_Text BoatHUD; // Reference to the BoatHUD on the boat / UI

    public float time = 0.00f; // Stores the player's total time
    public float startTime = 0.00f; // At what time did the 1st CP get crossed?

    public bool finished = false; // Set to true when finished

    public SoundController soundController = null; // Ref to the level's SoundController to play splashes

    void Start(){
        soundController = GameObject.Find("SoundController").GetComponent<SoundController>();
        if (soundController == null) Debug.LogWarning("No soundcontroller detected");

        // First, find all the checkpoints (children of the 'Checkpoints' gameObj)
        for (int i = 0; i < transform.childCount; i++) checkpoints.Add(transform.GetChild(i).gameObject);
        if (checkpoints.Count < 2) Debug.LogWarning("CheckpointController: Less than 2 checkpoints in level");

        // In each checkpoint (except the last), set 'nextCheckpoint' to the one after it 
        for (int i = 0; i < checkpoints.Count - 1; i++) checkpoints[i].GetComponent<Checkpoint>().nextCheckpoint = checkpoints[i + 1];
    
        // First Checkpoint: Activate immediately (but hide timer)
        checkpoints[0].GetComponent<Checkpoint>().isNext = true;
        checkpoints[0].GetComponentInChildren<TMP_Text>().color = new Color(0f,0f,0f,0f);

        BoatUI = GameObject.Find("BoatUI");
        if (BoatUI) BoatHUD = BoatUI.GetComponentInChildren<TMP_Text>();
        else Debug.LogWarning("CheckPointController.cs: Cannot find BoatUI Object!");
    }

    void Update(){ if (!finished && BoatHUD) BoatHUD.text = (startTime != 0.00f) ? "Total Time = " + (Time.time - startTime).ToString("F1") : "Pass the Checkpoint to start!"; }

    public void OnCheckpoint(GameObject CP){ // Do stuff when a checkpoint is reached
        float newTime = CP.GetComponent<Checkpoint>().getTime();
        time += newTime;

        CP.GetComponent<Checkpoint>().isNext = false; // Current checkpoint becomes past
        CP.GetComponent<Checkpoint>().R.material.color = new Color(0f,0f,0f,0f);

        // Play a sound from soundEffects (randomly chosen, if any)
        if (soundController != null) soundController.PlayRandomSound("cheer");
        
        if (checkpoints.IndexOf(CP) == 0) { // First Checkpoint: Activate effects
            time -= newTime; // Negative time for first CP, since we dont count it
            startTime = Time.time; // Start the actual timer now
        }

        if (CP.GetComponent<Checkpoint>().nextCheckpoint) { // Next checkpoint
            checkpoints[checkpoints.IndexOf(CP) + 1].GetComponent<Checkpoint>().isNext = true;
            Debug.Log("Checkpoint passed: " + CP.name + " Time = " + newTime + " totalTime = " + time);
        }
        else { // No next checkpoint (Player reached finish)
            Debug.Log("Final Checkpoint passed," + " Time = " + newTime + " totalTime = " + time);
            OnFinish();
            if (BoatHUD) BoatHUD.text += " !";
        }

        /* Do stuff */
        // Idea: Animation or some kind of visual change when a checkpoint is passed
        // First: Start the timer, weather effects, music change, etc.
        // Last: End the timer, effects, etc.

        /* Crazy level idea: 2 parrallel lanes of checkpoints (A & B), 
         * each lane 1 CP is removed from the checkpoints child list to elsewhere in tree (when loading the level)
         * however it is RANDOM, so the level is different each time
         * I wonder if easy to implement, and how fun it would be */
    }

    public void OnFinish(){
        finished = true; // Used to stop the timer ticks
        BoatUI.transform.GetChild(0).GetChild(1).gameObject.SetActive(true); // Show the menu / level buttons
        // ^^^ Bad practice but there isnt an easy way to get named children, so dumb
        // I would prefer something like: BoatUI.FindChild("ButtonsGroup").SetActive() 
    }
}
