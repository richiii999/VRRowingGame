using UnityEngine;
using System;

// NPCRacer.cs: Controls the NPCs behavior
// NPCs are effectively just visuals to look at in the background, they move along a set track and dont do any
// pathfinding or anything, 'difficulty' can be adjusted via changing the speed variable.


public class NPCRacer : MonoBehaviour{
    public CheckpointController CPC = null; // Ref to CheckpointController script on CheckpointGroup obj
    private GameObject currCP = null; // Set from ^
    private int currCPidx = 0;

    public Rigidbody motorRB = null; // Ref to 'BoatMotor' obj's Rigidbody to apply forces to
    public GameObject oarL = null; // Refs to the Oars & Look objects (to spin them)
    public GameObject oarR = null; 
    public GameObject lookTargetL = null;
    public GameObject lookTargetR = null;

    public float speed = 3.0f; // How fast the NPC goes along their track
    public float animSpan = 4.0f; // How wide the row anim move the oars
    public float animSpeed = 3.0f; // Speed of animation

    void Start(){ if (CPC == null) Debug.LogWarning("NPC CPC not set!"); }
        
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
        if (other.CompareTag("Checkpoint") && other.transform.parent == currCP){
            Debug.Log("NPC Checkpoint");

            currCPidx += 1;
            currCP = CPC.GetNextCP(currCPidx);
            if (currCP == null) Debug.Log("NPC Finished"); // Reached last CP

            Debug.Log(currCP);
        }
    }
    
}
