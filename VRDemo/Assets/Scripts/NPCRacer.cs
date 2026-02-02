using UnityEngine;
using System;

// NPCRacer.cs: Controls the NPCs behavior
// NPCs are effectively just visuals to look at in the background, they move along a set track and dont do any
// pathfinding or anything, 'difficulty' can be adjusted via changing the speed variable.


public class NPCRacer : MonoBehaviour{
    public GameObject oarL = null; // Refs to the Oars objects (to spin them)
    public GameObject oarR = null; 
    public GameObject lookTargetL = null;
    public GameObject lookTargetR = null;

    public float speed = 1.0f; // How fast the NPC goes along their track
    public float rowAnimSpan = 4.0f; // How wide the row anim move the oars

    void Update(){
        if (oarL != null && oarR != null){ // Make the oars "row" in a loop by pointing towards moving targets
            oarL.transform.LookAt(lookTargetL.transform);
            oarR.transform.LookAt(lookTargetR.transform);

            lookTargetL.transform.position += new Vector3(0f, 0f, (float)Math.Sin(Time.time) * rowAnimSpan);
            lookTargetR.transform.position += new Vector3(0f, 0f, (float)Math.Sin(Time.time) * rowAnimSpan * -1);
        }
        
    }
}
