using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMap : MonoBehaviour
{
    public float Offset;
    public bool IsDir;
    public int Speed;
    public void Update()
    {
        transform.Translate(-Vector3.forward * Time.deltaTime * Speed);
        if ( transform.position.z <= -2585)
        {
            Debug.Log("·çÇÁ");
            transform.position = new Vector3(0, 0, Offset);
        }
        //else if (!Dir && transform.position.z >= Offset)
        //{
        //    transform.position = new Vector3(0, 0, -Offset);
        //}
    }
}
