using Unity.XR.CoreUtils;
using UnityEngine;
using System.Collections;

// VRCalibration: Resets the position (but not direction) of the XROrigin according to the set values.
// Note: Values should be recalibrated anytime XROrigin (or any parent of it) are changed

public class VRCalibration : MonoBehaviour{
    public Transform boatTF;
    public XROrigin xrOrigin;
    public Transform lookTarget; // Point camera at this obj
    
    public Vector3 offset; // Dial this in to feel good
    public KeyCode key;

    void Start(){ StartCoroutine(LateStart(0.5f)); } // Delay starting calibration by a short time
    IEnumerator LateStart(float waitTime){yield return new WaitForSeconds(waitTime); Calibrate(); }

    void Update(){ if (Input.GetKeyDown(key)) Calibrate(); }

    public void Calibrate() { 
        xrOrigin.MoveCameraToWorldLocation(boatTF.position + offset); 
        xrOrigin.gameObject.transform.LookAt(lookTarget);
    }
}