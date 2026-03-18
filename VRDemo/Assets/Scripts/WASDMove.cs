using UnityEngine;

// WASDMove: Provides WASD movement to the given RB (for debugging, not intended for actual gameplay)

public class BoatControls : MonoBehaviour{
    public float strength = 0.05f; // How much force applied per frame
    public float maxVel = 2f; // Speed limit
    
    // When set to BoatMotor, left-right movement is supposed to be a pure rotating movement, but due to physics, also pushes the boat in that direction slightly.
    public Rigidbody RB;

    void Update(){ 
        RB.AddForce(Input.GetAxis("Vertical") * strength * transform.forward, ForceMode.Impulse);
        RB.AddForce(Input.GetAxis("Horizontal") * strength * Vector3.Normalize(Quaternion.Euler(0,0,0) * transform.right), ForceMode.Impulse);

        RB.linearVelocity = Vector3.ClampMagnitude(RB.linearVelocity, maxVel); // Apply speed limit
    }
}
