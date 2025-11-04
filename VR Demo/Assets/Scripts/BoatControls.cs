using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatControls : MonoBehaviour{
    Rigidbody boat;
    public float strength = 0.05f;
    public float maxVel = 2f;

    void Start() {boat=GetComponent<Rigidbody>();}

    void Update(){
        // Go forward / backward or left / right (turning) relative to the boat's current direction
        boat.AddForce(-transform.right * strength * Input.GetAxis("Vertical"),   ForceMode.Impulse);
        boat.AddForce(Vector3.Normalize(Quaternion.Euler(0,-45,0) * transform.forward) * strength * Input.GetAxis("Horizontal"), ForceMode.Impulse);

        boat.linearVelocity = Vector3.ClampMagnitude(boat.linearVelocity, maxVel);
    }
}
