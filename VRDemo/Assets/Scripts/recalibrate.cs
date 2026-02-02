using Unity.XR.CoreUtils;
using UnityEngine;

public class recalibrate : MonoBehaviour
{
    public GameObject boat;
    public Component xrOrigin;
    public float xOffset;
    public float yOffset;
    public float zOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int start;
    void Start()
    {
        start = 0;
    }
    // Update is called once per frame
    void Update()
    {
        start++;
        if (start == 10)
        {
            xrOrigin.GetComponent<XROrigin>().MoveCameraToWorldLocation(new Vector3((float) (boat.transform.position.x+xOffset),(float) (boat.transform.position.y+yOffset),(float) (boat.transform.position.z+zOffset)));
        }
        if ((int)Input.GetAxis("Jump") != 0)
        {
            xrOrigin.GetComponent<XROrigin>().MoveCameraToWorldLocation(new Vector3((float) (boat.transform.position.x+xOffset),(float) (boat.transform.position.y+yOffset),(float) (boat.transform.position.z+zOffset)));
        }
    }
}
