using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectEnemy : NormalEnemy
{
    protected override void Start()
    {
        base.Start();
        ChangeType(new EnemyMove(this));
        ChangeType(new NormalEnemyAttack(this, 1, 0));
    }

    //public override void EnqueuHpUI()
    //{
    //    ObjectPool.Instance.EnqueuePool(HpUIObj.HpUI.gameObject);
    //}

    public override void EnqueuEnemy()
    {
        gameObject.SetActive(false);
    }
}
