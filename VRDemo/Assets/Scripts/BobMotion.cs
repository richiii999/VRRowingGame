using UnityEngine;
using System;

// BobMotion: Makes the gameObject bob up and down gently over time

public class BobMotion : MonoBehaviour{
    public float bobStrength = 0.50f; // How strong the effect is
    public float bobRand = 30.0f; // Range of random starting position
    float originalY = 0.00f; // Centerpoint of sin motion

    void Start() { 
        originalY = transform.position.y; 
        bobRand = UnityEngine.Random.Range(0.00f, bobRand); // Randomly set the bobOffset (otherwise all bobbers in sync with eachother)
    }

    void Update(){ 
        transform.position = new Vector3(
            transform.position.x,
            originalY + ((float)Math.Sin(Time.time + bobRand) * bobStrength),
            transform.position.z); 
    }
}