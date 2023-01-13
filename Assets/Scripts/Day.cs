using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day : MonoBehaviour
{
   
    public float backflip;
   
   
    void Update()
    {

        

        
        this.transform.RotateAround (Vector3.zero, Vector3.right, backflip);
        
    }


}
