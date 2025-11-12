using UnityEngine;
using System;
using System.Collections;

// BobMotion.cs: Makes the gameObject bob up and down gently over time
// Yoinked from: https://gamedev.stackexchange.com/questions/96878/how-to-animate-objects-with-bobbing-up-and-down-motion-in-unity

public class BobMotion : MonoBehaviour{
    public float bobStrength = 0.50f; // How strong the effect is
    public float bobRand = 30.0f; // Range of random starting position
    float originalY = 0.00f;

    void Start() { 
        originalY = transform.position.y; 
        bobRand = UnityEngine.Random.Range(0.00f, bobRand); // Randomly set the bobOffset
    
    }
    void Update(){ 
        transform.position = new Vector3(
            transform.position.x,
            originalY + ((float)Math.Sin(Time.time + bobRand) * bobStrength),
            transform.position.z); 
        }
}