using UnityEngine;

public class MoveBoat : MonoBehaviour{
    public Rigidbody boat;
    public Rigidbody boatMotor;
    public float Speed_Coffecient = 1.00f; // Scales the force applied to the boat
    public bool debugBoatVelocity = false; // Enable to turn on debug.log calls for the boat
    public bool debugFlapper = false; // Enable to turn on debug.log calls for the flappers

    private Vector3 current; // Current and previous coordinates of the flapper (used for physics calculation)
    private Vector3 previous;

    private bool underwaterTrigger = false; // Trigger when enter water, resets when leaves water (ex. to play sounds)
    // Note: Use the setter setUnderwater(), do not set directly

    public SoundController soundController = null; // Ref to the level's SoundController to play splashes

    // Idea: Instead of checking for the flapper being below water via (y < 0),
    // Can use a raycast that only collides with the water object, this works for 3d water.
    // Raycast starts at the center of the flapper, and goes straight up.

    // Idea: Oars snap back to starting position when you let go
    // Idea: Oars model needs improvement. Probably make them longer, as the current boat sits very high
    
    void Start(){
        soundController = GameObject.Find("SoundController").GetComponent<SoundController>();
        if (soundController == null) Debug.LogWarning("No soundcontroller detected");

        current = this.transform.position;
        previous = this.transform.position;
    }

    void Update(){
        Vector3 flapperPosition = this.transform.position;
        Vector3 flapperVelocity = (current - previous) / Time.deltaTime;
        
        if(flapperPosition.y < -1){ // If flapper below water
            if (!underwaterTrigger) setUnderwater(true);

            boat.AddForce( -flapperVelocity * Speed_Coffecient);
            boatMotor.AddForce( -flapperVelocity * Speed_Coffecient);
        }
        else if (underwaterTrigger) setUnderwater(false);
        
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
        current = this.transform.position;
        
    }

    private void setUnderwater(bool state){ // Setter for underwater, used to play 3D splash sounds
        underwaterTrigger = state;
        if (underwaterTrigger && soundController) soundController.PlayRandomSound("splash",
                                                                                  transform.position.x, 
                                                                                  transform.position.y, 
                                                                                  transform.position.z);
        
    }
}
