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
        //xrOrigin.GetComponent<XROrigin>().MoveCameraToWorldLocation(new Vector3((float) (boat.transform.position.x-0.8),(float) (boat.transform.position.y+2.0),(float) (boat.transform.position.z-0.63)));
        xrOrigin.GetComponent<XROrigin>().MoveCameraToWorldLocation(new Vector3((float) (boat.transform.position.x),(float) (boat.transform.position.y+2.0),(float) (boat.transform.position.z)));
    }
    // Update is called once per frame
    void Update()
    {
        start++;
        if (start == 10)
        {
            xrOrigin.GetComponent<XROrigin>().MoveCameraToWorldLocation(new Vector3((float) (boat.transform.position.x-0.6),(float) (boat.transform.position.y+1.5),(float) (boat.transform.position.z+0.0)));
        }
        if ((int)Input.GetAxis("Jump") != 0)
        {
            xrOrigin.GetComponent<XROrigin>().MoveCameraToWorldLocation(new Vector3((float) (boat.transform.position.x-0.6),(float) (boat.transform.position.y+1.5),(float) (boat.transform.position.z+0.0)));
        }
    }
}
