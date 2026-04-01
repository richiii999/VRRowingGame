using UnityEngine;

// MoveBoat: Controls the forces applied to BoatMotor from the Oars' Flappers

public class MoveBoat : MonoBehaviour{
    public Rigidbody boat;
    public Rigidbody boatMotor;
    public float Speed_Coffecient = 1.00f; // Scales the force applied to the boat
    public bool debugFlapper = false; // Enable to turn on debug.log calls for the flappers

    private Vector3 current; // Current and previous coordinates of the flapper (used for physics calculation)
    private Vector3 previous;
    private float waterYLevel = 0.0f; // Water's Y level (grabbed from waterFloat.cs on the boat group)

    private bool underwaterTrigger = false; // Trigger when enter water, resets when leaves water (ex. to play sounds)
    // Note: Use the setter SetUnderwater(), do not set directly

    public SoundController soundController = null; // Ref to the level's SoundController to play splashes

    // Idea: Instead of checking for the flapper being below water via (y < 0),
    // Can use a raycast that only collides with the water object, this works for 3d water.
    // Raycast starts at the center of the flapper, and goes straight up.

    // Idea: Oars snap back to starting position when you let go
    // Idea: Oars model needs improvement. Probably make them longer, as the current boat sits very high
    
    void Start(){
        soundController = GameObject.Find("SoundController").GetComponent<SoundController>();
        if (soundController == null) Debug.LogWarning("No soundcontroller detected");

        waterYLevel = Tools.RefToObj("/Env/Water").transform.position.y + boat.GetComponent<Buoyancy>().waterOffset; 

        Vector3 relativePosition = boat.transform.position - transform.position;
        current = relativePosition;
        previous = relativePosition;
    }

    void Update(){
        //setting velocity
        Vector3 flapperPosition = transform.position;
        Vector3 flapperVelocity = current - previous;
        flapperVelocity.y = 0f;

        //spazzing out prevention
        //currently a bandaid fix. Instead, the hands should deattached when they get too far from the object.
        if(flapperVelocity.magnitude > 0.5)
        {
            flapperVelocity = new Vector3(0f, 0f, 0f);
            // Debug.Log("Magnitude too big");
        }
        
        //check if underwater. If yes add force and change underwater state
        if(flapperPosition.y < waterYLevel){ // If flapper below water
            if (!underwaterTrigger) SetUnderwater(true);

            //boat.AddForce(flapperVelocity * Speed_Coffecient);
            boatMotor.AddForce(flapperVelocity * Speed_Coffecient);
        }
        else if (underwaterTrigger) SetUnderwater(false);
        
        //debug code
        if (debugFlapper){
            Debug.Log(flapperVelocity.magnitude);
        }

        //setting positions 
        Vector3 relativePosition = boat.transform.position - transform.position;
        previous = current;
        current = relativePosition;
    }

    private void SetUnderwater(bool state){ // Setter for underwater, used to play 3D splash sounds
        underwaterTrigger = state;
        if (underwaterTrigger && soundController)
        {
            //Debug.Log("Oar Underwater");
            soundController.PlayRandomSound("splash",transform.position.x,transform.position.y,transform.position.z);
        }
    }
}