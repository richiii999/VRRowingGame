using UnityEngine;
using System;
using TMPro;

using static Tools;

// Checkpoint.cs: Signals the parent CheckpointController when a CP is crossed

// Note: Checkpoints are controlled by their parent object, 'Checkpoints' via the script 'CheckpointController'
// Note: The Checkpoint prefab object has a kinematic rigidbody, it cannot sense the child trigger without it.

public class Checkpoint : MonoBehaviour{
    private CheckpointController CPC; // Ref to the CPC
    public Material glowfield; // The CheckpointTrigger's Renderer
    public TMP_Text timerTxt; // TimerText obj
    public bool isNext = false; // Is this checkpoint the next one?
    public float startTime = 0.00f; // At what time did this CP become active?
    
    void Start(){ 
        CPC = RefToComp<CheckpointController>("CheckpointGroup");

        // Hide checkpoint glow on start
        SetGlowAlpha(0f);
    }

    void Update(){ 
        if (isNext) { 
            SetGlowAlpha( Mathf.Abs(((float)Math.Sin(Time.time)) * 0.7f) );
            
            if (startTime == 0.00f) startTime = Time.time; // First CP doesnt count timer
            timerTxt.text = (Time.time - startTime).ToString("F1"); // Round time to 2 dec places
        }

    }

    // Signal Player/NPC collisions to CheckpointController
    void OnTriggerEnter(Collider other){ if (isNext && (other.CompareTag("Player") || other.CompareTag("NPCRacer"))) CPC.OnCheckpoint(gameObject, other.CompareTag("Player")); }

    private void SetGlowAlpha(float a){ glowfield.color = new Color( glowfield.color.r, glowfield.color.g, glowfield.color.b, a); }

    public float GetTime(){ return float.Parse(timerTxt.text); }
}
