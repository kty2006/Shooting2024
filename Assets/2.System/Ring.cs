using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public LineRenderer l;
    public float time;

    public List<LineRenderer> ls;

    private IEnumerator Start()
    {
        float re = 1f / time;
        float width = l.startWidth;
        for (float t = 0; t < 1; t += Time.fixedDeltaTime * re)
        {
            l.transform.localScale = Vector3.one * Mathf.Sin(t * 90 * Mathf.Deg2Rad);
            yield return null;
        }
        Destroy(gameObject);
    }


    [ContextMenu("Cir")]
    public void MadeCir()
    {
        float angle = 360f / (l.positionCount - 2);

        for (int i = 0; i < l.positionCount; i++)
        {
            float e = angle * i * Mathf.Deg2Rad;
            Vector3 pos = new Vector3(Mathf.Cos(e), 0, Mathf.Sin(e)) * 0.5f;
            l.SetPosition(i, pos);

        }
    }
}
