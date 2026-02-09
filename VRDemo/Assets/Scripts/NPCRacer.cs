using UnityEngine;
using System;
using Unity.XR.CoreUtils;
using UnityEngine.UIElements;

// NPCRacer.cs: Controls the NPCs behavior
// NPCs are effectively just visuals to look at in the background, they move along a set track and dont do any
// pathfinding or anything, 'difficulty' can be adjusted via changing the speed variable.


public class NPCRacer : MonoBehaviour{
    public GameObject CPGroup = null; // Ref to CheckpointGroup so the NPC
    private Transform currCP = null; // Set on start from ^
    private int CPTotal = 0; // How many CPs in ^^
    // Probably an easier way to do ^ , like put the CPs in a list or someth idk

    public Rigidbody motorRB = null; // Ref to 'BoatMotor' obj's Rigidbody to apply forces to

    public GameObject oarL = null; // Refs to the Oars objects (to spin them)
    public GameObject oarR = null; 
    public GameObject lookTargetL = null;
    public GameObject lookTargetR = null;

    public float speed = 3.0f; // How fast the NPC goes along their track
    public float animSpan = 4.0f; // How wide the row anim move the oars
    public float animSpeed = 3.0f; // Speed of animation

    void Start(){ 
        if (CPGroup == null) Debug.LogWarning("NPC CPGroup not set!"); 
        else {
            currCP = CPGroup.transform.GetChild(0);
            CPTotal = CPGroup.transform.childCount;
        }
        
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

        // Move towards next cp smoothly via adding force to motor
        motorRB.AddForce(Vector3.MoveTowards(motorRB.transform.position, currCP.position, speed));
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Checkpoint") && other.transform.parent.transform == currCP){
            Debug.Log("NP C Checkpoint");
            int currIndex = other.gameObject.transform.GetSiblingIndex();
            if (currIndex == CPTotal) Debug.Log("NPC Finished"); // Reached last CP
            else currCP = CPGroup.transform.GetChild(currIndex + 1);
        }
    }
    
}
