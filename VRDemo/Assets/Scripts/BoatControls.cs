using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatControls : MonoBehaviour{
    Rigidbody boatRB;
    public float strength = 0.05f;
    public float maxVel = 2f;
    public bool moveParent = false; // Move the parent instead of this

    void Start() {
        if (moveParent) boatRB = transform.parent.GetComponent<Rigidbody>();
        else boatRB = GetComponent<Rigidbody>();

        Debug.Log(boatRB);
    }

    void Update(){
        // Go forward / backward or left / right (turning) relative to the boat's current direction
        boatRB.AddForce(-transform.right * strength * Input.GetAxis("Vertical"),   ForceMode.Impulse);
        boatRB.AddForce(Vector3.Normalize(Quaternion.Euler(0,-45,0) * transform.forward) * strength * Input.GetAxis("Horizontal"), ForceMode.Impulse);

        boatRB.linearVelocity = Vector3.ClampMagnitude(boatRB.linearVelocity, maxVel);
    }
}
