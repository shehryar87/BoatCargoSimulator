using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
   
    public static Transform target;
  

    public void Update()
    {
        transform.LookAt(target);
    }
}
