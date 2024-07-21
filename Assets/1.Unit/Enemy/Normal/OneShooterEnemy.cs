using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShooterEnemy : NormalEnemy
{
    protected override void Start()
    {
        base.Start();
        ChangeType(new EnemyMove(this));
        ChangeType(new NormalEnemyAttack(this, 1, 0));
    }
}
