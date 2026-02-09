using Unity.XR.CoreUtils;
using UnityEngine;
using System.Collections;

public class recalibrate : MonoBehaviour{
    public Transform boat;
    public XROrigin xrOrigin;
    public Vector3 offset;
    public KeyCode key;

    void Start(){ StartCoroutine(LateStart(0.5f)); } // Delay starting calibration by a short time
    IEnumerator LateStart(float waitTime){yield return new WaitForSeconds(waitTime); Calibrate(); }

    void Update(){ if (Input.GetKeyDown(key)) Calibrate(); }

    void Calibrate(){ 
        Debug.Log("XR Camera Calibrated");
        xrOrigin.MoveCameraToWorldLocation(boat.position + offset); 
    }
}