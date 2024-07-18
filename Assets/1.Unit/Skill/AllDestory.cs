using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDestory : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (ObjectPool.Instance.CheckPool(other.gameObject.tag))
        {
            ObjectPool.Instance.EnqueuePool(other.gameObject);
        }
    }
}
