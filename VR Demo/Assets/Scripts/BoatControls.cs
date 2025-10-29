using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatControls : MonoBehaviour{
    Rigidbody boat;
    public float strength = 0.03f;
    public float maxVel = 2f;

    void Start() {boat=GetComponent<Rigidbody>();}

    void Update(){
        boat.AddForce(Vector3.left    * strength * Input.GetAxis("Vertical"),   ForceMode.Impulse);
        boat.AddForce(Vector3.forward * strength * Input.GetAxis("Horizontal"), ForceMode.Impulse);

        boat.linearVelocity = Vector3.ClampMagnitude(boat.linearVelocity, maxVel);
    }
}
