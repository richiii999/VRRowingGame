using UnityEngine;
using System;
using TMPro;

using static Tools;
using System.Drawing;
using Color = UnityEngine.Color;


// Checkpoint.cs: Signals the parent CheckpointController when a CP is crossed by a Player/NPC

// Note: Checkpoints are controlled by their parent object via the script 'CheckpointController'
// Note: The Checkpoint prefab object has a kinematic rigidbody, it cannot sense the child trigger without it.

public class Checkpoint : MonoBehaviour{
    CheckpointController CPC; // Ref to the CPC
    
    public Renderer PostL; // The CheckpointTrigger's Renderer (to access material color as INSTANCE not the base color)
    public Renderer PostR;
    public TMP_Text timerTxt; // TimerText obj

    public bool isNext = false; // Is this checkpoint the currently active one?
    private float startTime = 0.00f; // At what time did this CP become active?

    public float scoreTime = 6.0f; // How many seconds for full score? (Reach slower than this = less score given)
    
    void Start(){ 
        // set ref
        CPC = RefToComp<CheckpointController>("CheckpointGroup");

        SetGlowAlpha(0.15f); // Super low checkpoint glow on start
    }

    void Update(){ 
        if (isNext) { 
            // Minimum value 0.1 - PingPong between 0.1-0.5
            // Multiply Time by 0.4 to slow the PingPong down a little bit
            float alpha = Mathf.PingPong(Time.time * 0.4f, 0.6f) + 0.1f;
            SetGlowAlpha(alpha);
            
            if (startTime == 0.00f) startTime = Time.time; // First CP doesnt count timer
            timerTxt.text = (Time.time - startTime).ToString("F1"); // Round time to 2 dec places
        }
    }

    // Signal Player collisions to CheckpointController
    void OnTriggerEnter(Collider other){ 
        if ( !(isNext && other.CompareTag("Player")) ) return; // Player, in-order only
        DeactivateCheckpoint(other);
    }

    public void SetGlowAlpha(float a){
        // Same Color as whatever the post is right now, just with an updated alphaw
        SetColor(new Color(PostL.material.color.r, PostL.material.color.g, PostL.material.color.b, a));
    }

    private void SetColor(Color color) {
        PostL.material.color = color;
        PostR.material.color = color;
    }

    private void DeactivateCheckpoint(Collider other) {
        if ( !(isNext && other.CompareTag("Player")) ) return; // Player, in-order only
        
        CPC.OnCheckpoint(this); 
        other.GetComponentInChildren<BoatUI>().Score( scoreTime / (Time.time - startTime), CPC.ResetMaxAngle()); // ResetMaxAngle() returns the value it was.
        SetColor(Color.gray);
        SetGlowAlpha(0.4f);
    }
}
