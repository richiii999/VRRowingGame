using UnityEngine;

public class BoatControls : MonoBehaviour{
    public float strength = 0.05f; // How much force applied per frame
    public float maxVel = 2f; // Speed limit
    public bool moveParent = false; // Use the parent's rigidbody instead of this
    
    Rigidbody boatRB;

    void Start() {
        if (moveParent) boatRB = transform.parent.GetComponent<Rigidbody>();
        else boatRB = GetComponent<Rigidbody>();

        // Debug.Log(boatRB);
    }

    void Update(){ // Go forward / backward or left / right (turning) relative to the boat's current direction
        boatRB.AddForce(transform.forward * strength * Input.GetAxis("Vertical"), ForceMode.Impulse);
        boatRB.AddForce(Vector3.Normalize(Quaternion.Euler(0,0,0) * transform.right) * strength * Input.GetAxis("Horizontal"), ForceMode.Impulse);

        boatRB.linearVelocity = Vector3.ClampMagnitude(boatRB.linearVelocity, maxVel); // Apply speed limit
    }
}
