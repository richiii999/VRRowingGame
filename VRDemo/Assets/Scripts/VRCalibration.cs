using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;

public class VRCalibration : MonoBehaviour{
    public Transform boatTF;
    public XROrigin xrOrigin;
    public Vector3 offset;
    public KeyCode key;

    void Start(){ StartCoroutine(LateStart(0.5f)); } // Delay starting calibration by a short time
    IEnumerator LateStart(float waitTime){yield return new WaitForSeconds(waitTime); Calibrate(); }

    void Update(){ if (Input.GetKeyDown(key)) Calibrate(); }

    void Calibrate() { xrOrigin.MoveCameraToWorldLocation(boatTF.position + offset); }
}
