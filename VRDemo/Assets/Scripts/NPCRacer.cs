using UnityEngine;
using System;

using static Tools;

// NPCRacer: Controls the NPCs behavior
// NPCs automatically row to each checkpoint in order. CurrCP can be set in the editor to make them start farther along (ex. main menu NPCs)

// NOTE: This script must be executed after CPC, see Edit>Project Settings>Script Execution Order

public class NPCRacer : MonoBehaviour{
    private CheckpointController CPC = null; // Ref to CheckpointController script on CheckpointGroup obj
    public Checkpoint currCP         = null; // If not set, uses CPC to find first CP.
    public bool frozen = true; // Freeze NPC until player first CP

    public Rigidbody  motorRB     = null; // Ref to 'BoatMotor' obj's Rigidbody to apply forces to
    public GameObject oarL        = null; // Refs to the Oars & Look objects (to spin them)
    public GameObject oarR        = null; 
    public GameObject lookTargetL = null;
    public GameObject lookTargetR = null;

    public float speed     = 1.0f;  // How fast the NPC move
    public float animSpan  = 10.0f; // Width of rowing animation (0 = static)
    public float animSpeed = 3.0f;  // Speed of rowing animation

    void Start(){ 
        CPC = RefToComp<CheckpointController>("CheckpointGroup"); 
        if (currCP == null) currCP = CPC.GetCP(); // NOTE: This script must be executed after CPC 
        if (oarL == null || oarR == null) QuitGame("NPCRacer Oars are missing!");
    }
        
    void Update(){ 
        // Make the oars "row" in a loop by pointing them towards moving targets
        float sin = (float)Math.Sin(Time.time * animSpeed) * animSpan; // sin movement
        Vector3 side = motorRB.transform.forward * sin; // side-to-side looktarget movement
        Vector3 fwd = motorRB.transform.right * 10.0f; // forward looktarget position

        lookTargetL.transform.position = motorRB.transform.position + fwd + side;
        lookTargetR.transform.position = motorRB.transform.position + fwd - side;

        oarL.transform.LookAt(lookTargetL.transform);
        oarR.transform.LookAt(lookTargetR.transform);

        // Move towards next cp smoothly via adding force to boatMotor
        if (!frozen && currCP != null) motorRB.AddForce(Vector3.MoveTowards(motorRB.transform.position, currCP.transform.position, speed) - transform.position);
    }

    private void OnTriggerEnter(Collider other){ // NPC Checkpoint
        if (other.CompareTag("Checkpoint") && currCP && other.transform.parent.parent.gameObject == currCP.gameObject){
            currCP = CPC.GetNextCP(currCP); // currCP is now the next CP
            if (currCP == null) { // true: NPC passed the final checkpoint
                animSpan = 0f; // Stop rowing anim
                if (CPC.finished == false) CPC.FinishRace(isPlayer: false); // only call end game if NPC wins
            }
        }
    }

    public void UnFreeze(float boostFactor = 800.0f){ // Unfreeze NPC, with optional speedboost on start
        if (frozen) { // Only if NPC is currently frozen
            frozen = false;
            motorRB.AddForce((Vector3.MoveTowards(motorRB.transform.position, currCP.transform.position, speed) - transform.position) * boostFactor);
        }
    }
}