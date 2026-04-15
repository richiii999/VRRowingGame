using Unity.Mathematics;
using UnityEngine;

public class snapBacktoPostition : MonoBehaviour
{
    public GameObject oar;
    private double timePassed = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        
    }
    void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed > 0.1)
        {
            SetRotation();
            timePassed -= 0.1;
        }
    }    
    public void SetRotation()
    {
        transform.localRotation = Quaternion.Euler(42.297f, 90f, 0f);
    }
}
