using UnityEngine;
using System;
using TMPro;

using static Tools;

// Checkpoint.cs: Signals the parent CheckpointController when a CP is crossed by a Player/NPC

// Note: Checkpoints are controlled by their parent object via the script 'CheckpointController'
// Note: The Checkpoint prefab object has a kinematic rigidbody, it cannot sense the child trigger without it.

public class Checkpoint : MonoBehaviour{
    private CheckpointController CPC; // Ref to the CPC
    public Renderer glowfield; // The CheckpointTrigger's Renderer (to access material color as INSTANCE not the base color)
    public TMP_Text timerTxt; // TimerText obj
    public bool isNext = false; // Is this checkpoint the currently active one?
    private float startTime = 0.00f; // At what time did this CP become active?

    public float scoreTime = 10.0f; // How many seconds for full score?
    
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
    void OnTriggerEnter(Collider other){ if (isNext && other.CompareTag("Player")) CPC.OnCheckpoint(this); }

    public void SetGlowAlpha(float a){ glowfield.material.color = new Color( glowfield.material.color.r, glowfield.material.color.g, glowfield.material.color.b, a); }

    public float GetTime(){ return float.Parse(timerTxt.text); }
}
