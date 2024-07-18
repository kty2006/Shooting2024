using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public float speed;
    public float time;
    public void Start()
    {
        StartCoroutine(Des());
    }
    public void OnTriggerStay(Collider other)
    {
        if(!other.CompareTag("Wing"))
        {
            other.transform.Translate(-Vector3.forward * 50 * Time.deltaTime);
        }
    }

    public IEnumerator Des()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
