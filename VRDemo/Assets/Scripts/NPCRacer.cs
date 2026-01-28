using UnityEngine;
using System;

// NPCRacer.cs: Controls the NPCs behavior
// NPCs are effectively just visuals to look at in the background, they move along a set track and dont do any
// pathfinding or anything, 'difficulty' can be adjusted via changing the speed variable.


public class NPCRacer : MonoBehaviour{
    public GameObject oarL = null; // Refs to the Oars objects (to spin them)
    public GameObject oarR = null; 

    public float speed = 1.0f; // How fast the NPC goes along their track

    void Start(){
        oarL = GameObject.Find("Oar_Left"); // Tips of oars
        oarR = GameObject.Find("Oar_Right"); 
    }

    void Update(){
        if (oarL != null && oarR != null){
            oarL.transform.RotateAround(oarL.transform.position, new Vector3(0,1,0), (float)Math.Sin(Time.time) * 0.5f * speed );
            oarR.transform.RotateAround(oarR.transform.position, new Vector3(0,1,0), (float)Math.Sin(Time.time) * 0.5f * speed * -1);
        }
        
    }
}
