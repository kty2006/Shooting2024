using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoSingleTone<ObjectPool>
{

    public Dictionary<string, Queue<GameObject>> Pool = new();

    public void AddPool(string gameObject)
    {
        if (!Pool.ContainsKey(gameObject))
        {
            Pool.Add(gameObject, new Queue<GameObject>());
        }
    }

    public bool CheckPool(string gameObject)
    {
        //foreach (var pool in Pool)
        //{
        //    Debug.Log(pool.Key.);
        //}
        return Pool.ContainsKey(gameObject);
    }
    public GameObject Pooling(Vector3 pos, Quaternion rotate, GameObject prefab)
    {
        GameObject insGameObject;
        if (Pool[prefab.tag].Count > 0)
        {
            insGameObject = Pool[prefab.tag].Dequeue();
            insGameObject.SetActive(true);
        }
        else
        {
            insGameObject = Instantiate(prefab);
        }
        insGameObject.transform.SetPositionAndRotation(pos, rotate);
        //Debug.Log($"오브젝트 위치{insGameObject.transform.position} 설정 위치{objTransform.position}");
        return insGameObject;
    }

    public void EnqueuePool(GameObject gameObject)
    {
        gameObject.transform.position = Vector3.zero;
        gameObject.SetActive(false);
        Pool[gameObject.tag].Enqueue(gameObject);
    }
}
