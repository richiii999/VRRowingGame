using System;
using UnityEngine;

public class FlipBackwards : MonoBehaviour
{   
    public void Flip_Backwards()
    {
        this.transform.Rotate(                
            0f, 
            180f, 
            0f, 
            Space.Self);
        //backwards = !backwards;
        //if (backwards)
        //{
        //    this.transform.localEulerAngles = new Vector3(0f,90f,0f);
        //}
        //else
        //{
        //    this.transform.localEulerAngles = new Vector3(0f,-90f,0f);
        //}
       
    }
}
