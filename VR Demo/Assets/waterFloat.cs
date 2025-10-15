using UnityEngine;

public class waterFloat : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 pos;
        Quaternion rot;
        transform.GetPositionAndRotation(out pos, out rot);
        transform.position = new Vector3(0, (float)3.87, (float)14.81);
        Debug.Log(pos.x + pos.y + pos.z);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos;
        Quaternion rot;
        transform.GetPositionAndRotation(out pos, out rot);
        //Debug.Log(pos.y);
        //if (pos.y < 0) { transform.position = new Vector3(0, (float)3.87, (float)14.81); }
        Debug.Log(gameObject.GetComponent<Rigidbody>().useGravity = false);
        gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, (float)3.87, (float)14.81);
    }
}