using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy
{
    public TimeAgent NormalTimeAgent;

    protected override void Start()
    {
        base.Start();
        ChangeType(new EnemyMove(this));
        ChangeType(new NormalEnemyAttack(this, 1, 0));
    }
    public override void OnEnable()
    {
        base.OnEnable();
        NormalTimeAgent = new(15, (timeAgent) => { }, (timeAgent) => { }, (timeAgent) => { ObjectPool.Instance.EnqueuePool(gameObject); Debug.Log("����"); }); //�̰� ����ȭ
        TimerSystem.Instance.AddTimer(NormalTimeAgent);
    }

    public override void Death()
    {
        DropItem(1);
        EnqueuEnemy();
        Debug.Log("����");
        //EnqueuHpUi();
        base.Death();
    }
}
