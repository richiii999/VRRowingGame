using UnityEngine;

public class MoveBoat : MonoBehaviour{
    public Rigidbody boat;
    public float Speed_Coffecient = 1.00f; // Scales the force applied to the boat
    public bool debugBoatVelocity = false; // Enable to turn on debug.log calls for the boat
    public bool debugFlapper = false; // Enable to turn on debug.log calls for the flappers

    private Vector3 current; // Current and previous coordinates of the flapper (used for physics calculation)
    private Vector3 previous;

    // Idea: Instead of checking for the flapper being below water via (y < 0),
    // Can use a raycast that only collides with the water object, this works for 3d water.
    // Raycast starts at the center of the flapper, and goes straight up.
    
    void Start(){
        current = this.transform.localPosition;
        previous = this.transform.localPosition;
    }

    void Update(){
        Vector3 flapperPosition = this.transform.position;
        current = this.transform.localPosition;
        Vector3 flapperVelocity = (current - previous) / Time.deltaTime;
        
        if(flapperPosition.y < 0){ // If flapper below water
            boat.AddForce( Quaternion.AngleAxis(90, Vector3.up) * flapperVelocity * Speed_Coffecient);
        }
        
        //Debug.Log("flapper position: " + flapperPosition);
        //Debug.Log("flapper velocity: " + flapperVelocity);
        
        if(!(boat.linearVelocity.x < 0.01 && boat.linearVelocity.x > -0.01) && debugBoatVelocity){
            Debug.Log("boat speed: " + boat.linearVelocity);
        }
        
        if(!(boat.linearVelocity.z < 0.01 && boat.linearVelocity.z > -0.01) && debugBoatVelocity){
            Debug.Log("boat speed: " + boat.linearVelocity);
        }
        
        if (debugFlapper && (current != previous)){
                Debug.Log("Flapper Position: " + flapperPosition);
                Debug.Log("Relative to Boat: " + current);
                Debug.Log("Flapper Velocity: " + flapperVelocity);
        }
        
        previous = current;
    }
}
