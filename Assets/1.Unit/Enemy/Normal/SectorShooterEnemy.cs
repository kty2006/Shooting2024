using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorShooterEnemy : NormalEnemy
{
    protected override void Start()
    {
        base.Start();
        ChangeType(new EnemyMove(this));
        ChangeType(new SectorEnemyShooter(this, 1, 0));
    }
}