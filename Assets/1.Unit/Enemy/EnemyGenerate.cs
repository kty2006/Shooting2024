using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    public float MinX, MaxX, ClampY, ClampZ;
    public Transform EnemyStart;
    public List<Enemy> Enemies = new();
    public LayerMask TypeLayer;
    public Dictionary<LayerMask, Enemy> InsEnemies = new Dictionary<LayerMask, Enemy>();
    public void Start()
    {
        StartCoroutine(Generate());
    }
    private IEnumerator Generate()
    {
        GameObject generateObj;
        foreach (Enemy enemy in Enemies)
        {
            ObjectPool.Instance.AddPool(enemy.gameObject.tag);
            InsEnemies.Add(enemy.TypeLayer, enemy);
        }
        while (true)
        {
            EnemyStart.position = new Vector3(Random.Range(MinX, MaxX), ClampY, ClampZ);
            EnemyStart.rotation = Quaternion.Euler(0, InsEnemies[TypeLayer].CheckRotate(EnemyStart.position, Player.Instance.transform.position), 0);
            generateObj = ObjectPool.Instance.Pooling(EnemyStart.position, EnemyStart.rotation, InsEnemies[TypeLayer].gameObject);
            if (generateObj.transform.parent != transform)
                generateObj.transform.parent = transform;
            EnemyController.Instance.ReSetting();
            yield return Util.Delay05;
        }
    }
}
