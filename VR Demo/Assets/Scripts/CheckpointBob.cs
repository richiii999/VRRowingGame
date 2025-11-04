using UnityEngine;
using System;
using System.Collections;

// CheckpointBob.cs: Makes the checkpoints bob up and down gently over time
// Yoinked from: https://gamedev.stackexchange.com/questions/96878/how-to-animate-objects-with-bobbing-up-and-down-motion-in-unity

public class CheckpointBob : MonoBehaviour{
    public float floatStrength = 1; // How strong the effect is
    float originalY;

    void Start() {this.originalY = this.transform.position.y;}
    void Update(){
        transform.position = new Vector3(
            transform.position.x,
            originalY + ((float)Math.Sin(Time.time) * floatStrength), 
            transform.position.z);
    }
}