using System.Numerics;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class MoveBoat : MonoBehaviour
{
    public Rigidbody boat;
    public int Speed_Coffecient;
    public bool debugBoatVelocity;
    public bool debugFlapper;
    private UnityEngine.Vector3 current;
    private UnityEngine.Vector3 previous;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current = this.transform.localPosition;
        previous = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 flapperPosition = this.transform.position;
        var flapperVelocity = (current - previous) / Time.deltaTime;
        if(flapperPosition.y < 0)
        {
            boat.AddForce(Speed_Coffecient*flapperVelocity);
        }
        //Debug.Log("flapper position: " + flapperPosition);
        //Debug.Log("flapper velocity: " + flapperVelocity);
        if(!(boat.linearVelocity.x < 0.01 && boat.linearVelocity.x > -0.01) && debugBoatVelocity)
        {
            Debug.Log("boat speed: " + boat.linearVelocity);
        }
        if(!(boat.linearVelocity.z < 0.01 && boat.linearVelocity.z > -0.01) && debugBoatVelocity)
        {
            Debug.Log("boat speed: " + boat.linearVelocity);
        }
        if (debugFlapper && (current != previous))
        {
            Debug.Log("Flapper Position: " + flapperPosition);
            Debug.Log("Relative to Boat: " + current);
            Debug.Log("Flapper Velocity: " + flapperVelocity);
        }
        previous = current;
        current = this.transform.localPosition;
    }
}
