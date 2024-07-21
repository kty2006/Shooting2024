using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoSingleTone<EnemyController>
{
    public List<Enemy> Enemies = new();

    public void Update()
    {
        ReSetting();
        foreach (Enemy enemy in Enemies)
        {
            enemy.MoveType.Move();
            enemy.CurrentWeapon.Attack();
        }
    }

    public void ReSetting()
    {
        Enemies = gameObject.GetComponentsInChildren<Enemy>().ToList().Where(x=>x.gameObject.activeSelf).ToList();
    }

    public void DequeueEnemy()
    {
        foreach (Enemy enemy in Enemies)
        {
            if (!enemy.gameObject.activeSelf)
                Enemies.Remove(enemy);
        }
    }
}
