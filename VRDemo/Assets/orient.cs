using Unity.Mathematics;
using UnityEngine;

public class orient : MonoBehaviour
{
    public GameObject hand;
    public float addRotation = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.SetPositionAndRotation(this.transform.position, new quaternion(this.transform.rotation.x, hand.transform.rotation.y+addRotation, this.transform.rotation.z,this.transform.rotation.w));
    }
}
