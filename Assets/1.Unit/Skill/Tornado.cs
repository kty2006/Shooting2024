using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public int speed;
    public int time;
    public void Start()
    {
        StartCoroutine(Des());
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * Time.fixedDeltaTime * speed);
    }

    IEnumerator Des()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
