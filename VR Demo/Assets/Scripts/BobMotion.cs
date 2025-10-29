using UnityEngine;
using System;
using System.Collections;

// BobMotion.cs: Makes the gameObject bob up and down gently over time
// Yoinked from: https://gamedev.stackexchange.com/questions/96878/how-to-animate-objects-with-bobbing-up-and-down-motion-in-unity

public class BobMotion : MonoBehaviour{
    public float floatStrength = 1.00f; // How strong the effect is
    float originalY;

    void Start() { originalY = transform.position.y; }
    void Update(){ 
        transform.position = new Vector3(
            transform.position.x,
            originalY + ((float)Math.Sin(Time.time) * floatStrength),
            transform.position.z); 
        }
}