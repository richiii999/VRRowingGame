using UnityEngine;

// WASDMove: Provides WASD movement to the given RB (for debugging, not intended for actual gameplay)

public class WASDMove : MonoBehaviour{
    public float strength = 0.05f; // How much force applied per frame
    public float maxVel = 2.0f; // Speed limit

    public float boost = 3.0f; // How much faster when boosting? (hold shift, forward/back only)
    private bool boostEnabled = false; // input stuff not working, this is dumb way of doing it

    // When set to BoatMotor, left-right movement is supposed to be a pure rotating movement, but due to physics, also pushes the boat in that direction slightly.
    public Rigidbody RB;

    void Update(){ 
        // dumb way
        if (Input.GetKeyDown(KeyCode.LeftShift)) { boostEnabled = true;  maxVel = 8f; }
        if (Input.GetKeyUp(KeyCode.LeftShift))   { boostEnabled = false; maxVel = 2f; }
        float boostFactor = boostEnabled ? 1f + boost : 1f;
        // Debug.Log(boostFactor);

        // Add fwd/back and L/R forces separately
        RB.AddForce(Input.GetAxis("Vertical") * boostFactor * strength * transform.forward, ForceMode.Impulse);
        RB.AddForce(Input.GetAxis("Horizontal") * strength * Vector3.Normalize(Quaternion.Euler(0,0,0) * transform.right), ForceMode.Impulse);

        RB.linearVelocity = Vector3.ClampMagnitude(RB.linearVelocity, maxVel); // Apply speed limit
    }
}
