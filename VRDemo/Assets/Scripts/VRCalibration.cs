using System.Collections;
using UnityEngine;

public class VRCalibration : MonoBehaviour{
    public Transform calibrationObj; // Snap camera to this Obj's position
    public KeyCode key;
    public Vector3 positionOffset;

    void Start(){ StartCoroutine(LateStart(0.5f)); } // Delay starting calibration by a short time
    IEnumerator LateStart(float waitTime){yield return new WaitForSeconds(waitTime); Calibrate(); }

    void Update(){ if (Input.GetKeyDown(key)) Calibrate(); }

    void Calibrate() { 
        transform.position = calibrationObj.position + positionOffset; 
        transform.rotation = calibrationObj.rotation;
    }
}
