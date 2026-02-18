using UnityEngine;
using System;

using static Tools;

// NPCRacer.cs: Controls the NPCs behavior
// NPCs automatically row to each checkpoint in order. CurrCP can be set in the editor to make them start farther along (ex. main menu NPCs)

// NOTE: This script must be executed after CPC, see Edit>Project Settings>Script Execution Order


public class NPCRacer : MonoBehaviour{
    private CheckpointController CPC = null; // Ref to CheckpointController script on CheckpointGroup obj
    public Checkpoint currCP = null; // If not set, uses ^ to find first CP.
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
        CPC = RefToComp<CheckpointController>("CheckpointGroup"); // NOTE: This script must be executed after CPC 
        if (currCP == null) currCP = CPC.GetCP();
        currCPidx = CPC.checkpoints.IndexOf(currCP);
            
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
    // BUG: NPC's entire hitbox is a trigger so it counts for the checkpoint but not this, which only detects the yellow trigger (the intended trigger)
    // Not going to fix however since 1. idk how 2. its not a big deal, I extended the yellow trigger in the CP to be inside the buoys so this is minimal issue.
        if (other.CompareTag("Checkpoint") && other.transform.parent.gameObject == currCP){
            currCP = CPC.GetNextCP(currCPidx); currCPidx += 1; 
            if (currCP == null) {
                Debug.Log("NPC Finished");
                animSpan = 0f; // Stop rowing anim
            }
        }
    }
}