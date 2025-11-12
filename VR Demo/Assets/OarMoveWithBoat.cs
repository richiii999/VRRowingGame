using System.Numerics;
using UnityEngine;

public class OarMoveWithBoat : MonoBehaviour
{
    public GameObject Oar;
    public GameObject Boat;
    //private UnityEngine.Vector3 differenceAnchor;
    private UnityEngine.Vector3 differenceBoat_Oar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //differenceAnchor = Oar.GetComponent<ConfigurableJoint>().anchor - Boat.transform.position;
        differenceBoat_Oar = Oar.transform.position - Boat.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Oar.GetComponent<ConfigurableJoint>().anchor = differenceAnchor + Boat.transform.position;
        Oar.transform.position = differenceBoat_Oar + Boat.transform.position;
        Debug.Log(Oar.transform.position);
    }
}
