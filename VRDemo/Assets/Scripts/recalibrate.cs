using Unity.XR.CoreUtils;
using UnityEngine;

public class recalibrate : MonoBehaviour
{
    public GameObject boat;
    public Component xrOrigin;
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
            xrOrigin.GetComponent<XROrigin>().MoveCameraToWorldLocation(new Vector3((float) (boat.transform.position.x-0.6),(float) (boat.transform.position.y+1.0),(float) (boat.transform.position.z+0.0)));
        }
        if ((int)Input.GetAxis("Jump") != 0)
        {
            xrOrigin.GetComponent<XROrigin>().MoveCameraToWorldLocation(new Vector3((float) (boat.transform.position.x-0.6),(float) (boat.transform.position.y+1.0),(float) (boat.transform.position.z+0.0)));
        }
    }
}
