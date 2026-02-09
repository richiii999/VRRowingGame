using UnityEngine;
using System;

// NPCRacer.cs: Controls the NPCs behavior
// NPCs are effectively just visuals to look at in the background, they move along a set track and dont do any
// pathfinding or anything, 'difficulty' can be adjusted via changing the speed variable.

// NOTE: This script must be executed after CPC, see Edit>Project Settings>Script Execution Order


public class NPCRacer : MonoBehaviour{
    public CheckpointController CPC = null; // Ref to CheckpointController script on CheckpointGroup obj
    public GameObject currCP = null; // Set from ^
    private int currCPidx = 0;

    public Rigidbody motorRB = null; // Ref to 'BoatMotor' obj's Rigidbody to apply forces to
    public GameObject oarL = null; // Refs to the Oars & Look objects (to spin them)
    public GameObject oarR = null; 
    public GameObject lookTargetL = null;
    public GameObject lookTargetR = null;

    public float speed = 3.0f; // How fast the NPC goes along their track
    public float animSpan = 4.0f; // How wide the row anim move the oars
    public float animSpeed = 3.0f; // Speed of animation

    void Start(){ 
        if (CPC == null) Debug.LogError("NPC CPC not set!"); 
        else {currCPidx = 0; currCP = CPC.GetCP(); } // NOTE: This script must be executed after CPC 
    }
        
    void Update(){
        if (oarL != null && oarR != null){ // Make the oars "row" in a loop by pointing towards moving targets
            oarL.transform.LookAt(lookTargetL.transform);
            oarR.transform.LookAt(lookTargetR.transform);

            float sin = (float)Math.Sin(Time.time * animSpeed) * animSpan; // Sinwave movement pattern

            Vector3 boatPos = motorRB.transform.position;

            lookTargetL.transform.position = new Vector3(boatPos.z + 5, 2f, boatPos.x + sin * -1);
            lookTargetR.transform.position = new Vector3(boatPos.z + 5, 2f, boatPos.x + sin);
        }

        // Move towards next cp smoothly via adding force to boatMotor
        Vector3 forceVec = Vector3.MoveTowards(motorRB.transform.position, currCP.transform.position, speed) - transform.position;
        motorRB.AddForce(forceVec);
    }

    private void OnTriggerEnter(Collider other){ // NPC Checkpoint
        if (other.CompareTag("Checkpoint") && other.transform.parent.gameObject == currCP){
            Debug.Log("NPC Checkpoint");

            if (currCP == null) Debug.Log("NPC Finished"); // Reached last CP
            else { currCPidx += 1; currCP = CPC.GetNextCP(currCPidx); Debug.Log(currCP); }
        }
    }
    
}
