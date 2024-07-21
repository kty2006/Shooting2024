using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ShakeData
{
    public float time;
    public float force;

    public ShakeData(float time, float force)
    {
        this.time = time;
        this.force = force;
    }
}
public class CameraShake : MonoSingleTone<CameraShake>
{
    public List<ShakeData> shakeDatas = new();
    public Vector3 CameraPos;
    private void Update()
    {
        CameraPos = transform.position;
        if (shakeDatas.Count == 0)
        {
            transform.localPosition = CameraPos;
            return;
        }
        float max = shakeDatas.Max(f => f.force);
        transform.localPosition = CameraPos +Random.insideUnitSphere.normalized * max;
        shakeDatas = shakeDatas.Where(d => d.time > Time.time).ToList();
    }

    public void Shake(float time, float force)
    {
        shakeDatas.Add(new(Time.time + time, force));
    }
}
