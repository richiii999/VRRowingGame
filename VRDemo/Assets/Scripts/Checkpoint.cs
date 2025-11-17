using UnityEngine;
using System;
using System.Collections;
using TMPro;

// Checkpoint.cs: Signals the parent CheckpointController when a CP is crossed

// Note: Checkpoints are controlled by their parent object, 'Checkpoints' via the script 'CheckpointController'
// Note: The Checkpoint prefab object has a kinematic rigidbody, it cannot sense the child trigger without it.

public class Checkpoint : MonoBehaviour{
    public GameObject nextCheckpoint; // Which CP is next? (None = finish)
    public Renderer R; // The CheckpointTrigger's Renderer
    public TMP_Text T; // TimerText obj
    public bool isNext = false; // Is this checkpoint the next one?
    public float startTime = 0.00f; // At what time did this CP become active?
    

    void Start(){ 
        T = GetComponentInChildren<TMP_Text>();

        // Hide checkpoint glow on start
        R.material.color = new Color( R.material.color.r, R.material.color.g, R.material.color.b, 0.00f);
    }

    void Update(){ 
        if (isNext && T) { 
            if (startTime == 0.00f) startTime = Time.time;
            T.text = (Time.time - startTime).ToString("F1"); // Round time to 2 dec places
        }


        if (isNext && R) R.material.color = new Color( // Active checkpoint Glow effect
            R.material.color.r,
            R.material.color.g,
            R.material.color.b,
            Mathf.Abs(((float)Math.Sin(Time.time)) * 0.7f) ); // Adjust the float to change glow amount
    }

    // If player collides with the trigger, signal to CheckpointController
    void OnTriggerEnter(Collider other){ if (other.tag == "Player") transform.parent.gameObject.GetComponent<CheckpointController>().OnCheckpoint(transform.gameObject); }

    public float getTime(){ return float.Parse(T.text); }
}
