using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoSingleTone<EnemyGenerate>
{
    public float MinX, MaxX, ClampY, ClampZ;
    public Transform EnemyStart;
    public List<GameObject> Enemies = new();
    public int count = 5;
    public void OnEnable()
    {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        GameObject generateObj;
        foreach (var enemy in Enemies)
        {
            ObjectPool.Instance.AddPool(enemy.gameObject.tag);
        }
        while (true)
        {
            foreach (var enemy in Enemies)
            {
                EnemyStart.position = new Vector3(Random.Range(MinX, MaxX), ClampY, ClampZ);
                if (enemy.CompareTag("Monster1") && enemy.CompareTag("Monster4"))
                    EnemyStart.rotation = Quaternion.Euler(0, CheckRotate(EnemyStart.position, Player.Instance.transform.position), 0);
                else
                    EnemyStart.rotation = Quaternion.Euler(0, 180, 0);
                generateObj = ObjectPool.Instance.Pooling(EnemyStart.position, EnemyStart.rotation, enemy.gameObject.gameObject);
                if (generateObj.transform.parent != transform)
                    generateObj.transform.parent = transform;
                EnemyController.Instance.ReSetting();
                yield return new WaitForSeconds(count);
            }

        }
    }

    public float CheckRotate(Vector3 startPos, Vector3 targetPos)
    {
        Vector3 dir; //계속 생성되지만 다른데서 사용하지 않기 때문에 
        dir = targetPos - startPos;
        dir.Normalize();
        return Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
    }
}
