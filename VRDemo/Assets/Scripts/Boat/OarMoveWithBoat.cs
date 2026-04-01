using UnityEngine;

// OarMoveWithBoat: Attaches the oars to the boat via script. (Joints didnt work)

public class OarMoveWithBoat : MonoBehaviour
{
    public GameObject Oar;
    public GameObject Boat;
    //private Vector3 differenceAnchor;
    private Vector3 differenceBoat_Oar;
    
    void Start(){
        //differenceAnchor = Oar.GetComponent<ConfigurableJoint>().anchor - Boat.transform.position;
        differenceBoat_Oar = Oar.transform.position - Boat.transform.position;
    }

    void Update(){
        //Oar.GetComponent<ConfigurableJoint>().anchor = differenceAnchor + Boat.transform.position;
        Oar.transform.position = differenceBoat_Oar + Boat.transform.position;
        // Debug.Log(Oar.transform.position);
    }
}
