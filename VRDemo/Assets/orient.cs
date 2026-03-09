using Unity.Mathematics;
using UnityEngine;

public class orient : MonoBehaviour
{
    public GameObject hand;
    public float addRotation = 0;
    private int count = 0;
    private Rigidbody oar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oar = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        if(count % 10 == 0)
        {
            oar.AddTorque(this.transform.rotation.x, this.transform.rotation.y, hand.transform.rotation.x - this.transform.rotation.z);
            //new quaternion(this.transform.rotation.x, this.transform.rotation.y, hand.transform.rotation.x+addRotation, this.transform.rotation.w));
        }
    }
}
