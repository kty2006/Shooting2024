using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy
{
    public TimeAgent NormalTimeAgent;

    public override void Start()
    {
        base.Start();
        ChangeType(new EnemyMove(this));
        ChangeType(new NormalEnemyAttack(this));
    }
    public void OnEnable()
    {
        NormalTimeAgent = new(15, (timeAgent) => { }, (timeAgent) => { }, (timeAgent) => ObjectPool.Instance.EnqueuePool(gameObject, TypeLayer)); //이거 최적화
        TimerSystem.Instance.AddTimer(NormalTimeAgent);
    }

}
