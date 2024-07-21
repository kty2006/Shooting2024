using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public Vector3 Rotater;
    public void FixedUpdate()
    {
        transform.Rotate(Rotater * Time.deltaTime * 1, Space.World); 
        //transform.rotation = Quaternion.Euler(Rotater) * transform.rotation;
    }
}
