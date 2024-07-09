using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public TimeAgent BulletTimeAgent;
    public LayerMask Layer;
    public Vector3 Dir = Vector3.forward;
    public int Speed;
    public float Power;
    public bool IsCheck;
    public void OnEnable()
    {
        Dir = IsCheck switch
        {
            true => Vector3.forward,
            false => -Vector3.forward,
        };
        BulletTimeAgent = new(10, (timeAgent) => { }, (timeAgent) => transform.Translate(Dir * Time.fixedDeltaTime * Speed), (timeAgent) => ObjectPool.Instance.EnqueuePool(gameObject, Layer)); //이거 최적화
        TimerSystem.Instance.AddTimer(BulletTimeAgent);
    }
}
