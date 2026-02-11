using UnityEngine;
using System;

// NPCRacer.cs: Controls the NPCs behavior
// NPCs are effectively just visuals to look at in the background, they move along a set track and dont do any
// pathfinding or anything, 'difficulty' can be adjusted via changing the speed variable.

// NOTE: This script must be executed after CPC, see Edit>Project Settings>Script Execution Order


public class NPCRacer : MonoBehaviour{
    public CheckpointController CPC = null; // Ref to CheckpointController script on CheckpointGroup obj
    private GameObject currCP = null; // Set from ^
    private int currCPidx = 0;

    public Rigidbody motorRB = null; // Ref to 'BoatMotor' obj's Rigidbody to apply forces to
    public GameObject oarL = null; // Refs to the Oars & Look objects (to spin them)
    public GameObject oarR = null; 
    public GameObject lookTargetL = null;
    public GameObject lookTargetR = null;

    public float speed = 1.0f; // How fast the NPC goes along their track
    public float animSpan = 10.0f; // How wide the row anim move the oars
    public float animSpeed = 3.0f; // Speed of animation

    void Start(){ 
        if (CPC == null) Debug.LogError("NPC CPC not set!"); 
        else {currCPidx = 0; currCP = CPC.GetCP(); } // NOTE: This script must be executed after CPC 
    }
        
    void Update(){
        if (oarL != null && oarR != null){ // Make the oars "row" in a loop by pointing towards moving targets
            float sin = (float)Math.Sin(Time.time * animSpeed) * animSpan; // sin movement
            Vector3 side = motorRB.transform.forward * sin; // side-to-side looktarget movement
            Vector3 fwd = motorRB.transform.right * 10.0f; // forward looktarget position

            lookTargetL.transform.position = motorRB.transform.position + fwd + side;
            lookTargetR.transform.position = motorRB.transform.position + fwd - side;

            oarL.transform.LookAt(lookTargetL.transform);
            oarR.transform.LookAt(lookTargetR.transform);
        }

        // Move towards next cp smoothly via adding force to boatMotor
        if (currCP != null) motorRB.AddForce(Vector3.MoveTowards(motorRB.transform.position, currCP.transform.position, speed) - transform.position);
    }

    private void OnTriggerEnter(Collider other){ // NPC Checkpoint
        if (other.CompareTag("Checkpoint") && other.transform.parent.gameObject == currCP){
            currCP = CPC.GetNextCP(currCPidx); currCPidx += 1; Debug.Log("NPC Checkpoint");
            if (currCP == null) {
                Debug.Log("NPC Finished");
                animSpan = 0f; // Stop rowing anim
            }
        }
    }
}

// Alternate rowing animation code that rows faster/slower according to current velocity.
// Removed because I couldnt get it to look good, but it does work.
// animTimer = (animTimer + Time.deltaTime) % (2*math.PI); // sin movement
// float sin = (float) Math.Sin(animTimer * animSpeed * (math.abs(motorRB.linearVelocity.x) + math.abs(motorRB.linearVelocity.z))); 
// 
// Vector3 side = motorRB.transform.forward * (sin % (animSpan * 2*math.PI));
// Vector3 fwd = motorRB.transform.right * 10.0f;