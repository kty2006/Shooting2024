using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaringTime : MonoBehaviour
{
    public float time;
    public void Start()
    {
        StartCoroutine(Des());
    }

    IEnumerator Des()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
