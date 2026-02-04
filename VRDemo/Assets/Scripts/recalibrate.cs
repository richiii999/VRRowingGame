using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;

public class recalibrate : MonoBehaviour
{
    public GameObject boat;
    public XROrigin xrOrigin;
    public Vector3 offset;

    void Start(){ StartCoroutine(LateStart(0.5f)); } // Delay starting calibration by a short time
    IEnumerator LateStart(float waitTime){yield return new WaitForSeconds(waitTime); Recalibrate(); }

    void Update(){ if (Input.GetKeyDown(KeyCode.Space)) Recalibrate(); }

    void Recalibrate() { xrOrigin.GetComponent<XROrigin>().MoveCameraToWorldLocation(boat.transform.position + offset); }
}
