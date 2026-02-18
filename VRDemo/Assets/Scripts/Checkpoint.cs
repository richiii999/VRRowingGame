using UnityEngine;
using System;
using TMPro;

using static Tools;

// Checkpoint.cs: Signals the parent CheckpointController when a CP is crossed

// Note: Checkpoints are controlled by their parent object, 'Checkpoints' via the script 'CheckpointController'
// Note: The Checkpoint prefab object has a kinematic rigidbody, it cannot sense the child trigger without it.

public class Checkpoint : MonoBehaviour{
    private CheckpointController CPC; // Ref to the CPC
    public GameObject nextCheckpoint; // Which CP is next? (None = finish)
    public Renderer R; // The CheckpointTrigger's Renderer
    public TMP_Text T; // TimerText obj
    public bool isNext = false; // Is this checkpoint the next one?
    public float startTime = 0.00f; // At what time did this CP become active?
    

    void Start(){ 
        T = GetComponentInChildren<TMP_Text>();
        CPC = RefToComp<CheckpointController>("CheckpointGroup");

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
    void OnTriggerEnter(Collider other){ if (isNext && (other.CompareTag("Player") || other.CompareTag("NPCRacer"))) CPC.OnCheckpoint(transform.gameObject, other.CompareTag("Player")); }

    public float GetTime(){ return float.Parse(T.text); }

    public float GetRelativeAngle(GameObject target){ // Gets the relative angle (deg in XZ-plane) of target's fwd to the center of the CP, 0 = X+, 180 = X-
        // A = BoatFace, B = BoatPos, C = CP pos
        Vector2 A = new Vector2(target.transform.forward.x,  target.transform.forward.z).normalized;
        Vector2 B = new Vector2(0f, 0f);
        Vector2 C = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.z - transform.position.z).normalized;

        return Vector2.SignedAngle(B-A, C-B);
    }
}
