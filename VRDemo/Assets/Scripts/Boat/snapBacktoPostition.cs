using Unity.Mathematics;
using UnityEngine;

public class snapBacktoPostition : MonoBehaviour
{
    public GameObject oar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetRotation()
    {
        transform.localRotation = Quaternion.Euler(42.297f, 90f, 0f);
    }
}
