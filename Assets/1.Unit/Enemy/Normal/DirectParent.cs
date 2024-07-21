using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectParent : MonoBehaviour
{
    public void OnEnable()
    {
        StartCoroutine(EnemyTime());
    }
    public IEnumerator EnemyTime()
    {
        yield return new WaitForSeconds(13);
        ObjectPool.Instance.EnqueuePool(gameObject);
    }
}
