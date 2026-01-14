using Unity.XR.CoreUtils;
using UnityEngine;

public class recalibrate : MonoBehaviour
{
    public GameObject boat;
    public Component xrOrigin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        xrOrigin.GetComponent<XROrigin>().MoveCameraToWorldLocation(new Vector3((float) 0.0,(float) 1.2,(float) 0.0));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
