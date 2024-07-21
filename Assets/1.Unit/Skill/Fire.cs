using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float time;
    public void Start()
    {
        StartCoroutine(Des());
    }
    public void OnTriggerStay(Collider other)
    {
    }

    public IEnumerator Des()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
