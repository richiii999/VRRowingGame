using System.Numerics;
using UnityEngine;

public class MoveBoat : MonoBehaviour
{
    public Rigidbody boat;
    private int count;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        count = count + 1;
        UnityEngine.Vector3 flapperPosition = this.transform.position;
        if(flapperPosition.y < 0)
        {
            boat.linearVelocity = new UnityEngine.Vector3((float)(boat.linearVelocity.x + 0.1), boat.linearVelocity.y, (float)(boat.linearVelocity.z + 0.1));
        }
        if(count % 100 == 0)
        {
            Debug.Log("flapper: " + flapperPosition);
            Debug.Log("boat speed: " + boat.linearVelocity);
        }
    }
}
