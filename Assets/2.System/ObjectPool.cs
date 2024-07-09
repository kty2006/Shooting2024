using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoSingleTone<ObjectPool>
{

    public Dictionary<LayerMask, Queue<GameObject>> Pool = new();

    public void AddPool(LayerMask layerMask)
    {
        if (!Pool.ContainsKey(layerMask))
        {
            Pool.Add(layerMask, new Queue<GameObject>());
        }
    }

    public GameObject Pooling(Transform objTransform, GameObject prefab, LayerMask layer)
    {
        GameObject insGameObject;
        if (Pool[layer].Count > 0)
        {
            insGameObject = Pool[layer].Dequeue();
            insGameObject.SetActive(true);
        }
        else
        {
            insGameObject = Instantiate(prefab);
        }
        insGameObject.transform.SetPositionAndRotation(objTransform.position, objTransform.rotation);
        //Debug.Log($"오브젝트 위치{insGameObject.transform.position} 설정 위치{objTransform.position}");
        return insGameObject;
    }

    public void EnqueuePool(GameObject gameObject, LayerMask layer)
    {
        gameObject.transform.position = Vector3.zero;
        gameObject.SetActive(false);
        Pool[layer].Enqueue(gameObject);
    }
}
