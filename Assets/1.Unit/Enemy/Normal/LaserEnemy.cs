using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : NormalEnemy
{
    protected override void Start()
    {
        base.Start();
        ChangeType(new EnemyMove(this));
        ChangeType(new LaserEnemyAttack(this, 1, 0));
    }
}
