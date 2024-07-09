using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalAttack : IAttack
{
    public Unit Unit;
    public bool IsCheck = true;
    public TimeAgent AttackTimeAgent;
    public PlayerNormalAttack(Unit unit)
    {
        this.Unit = unit;
    }
    public void Attack()
    {
        Unit.BulletPrefab.Speed = Unit.unitStates.AttackSpeed;
        Unit.BulletPrefab.Power = Unit.unitStates.Power;
        AttackTimeAgent = new(Unit.unitStates.SkillCoolTime[0], (timeAgent) => IsCheck = false, (timeAgent) => { }, (timeAgent) => IsCheck = true);
        if (Input.GetKey(KeyCode.Space) && IsCheck)
        {
            TimerSystem.Instance.AddTimer(AttackTimeAgent);
            ObjectPool.Instance.Pooling(Unit.transform, Unit.BulletPrefab.gameObject, Unit.BulletPrefab.Layer);
        }
    }
}

public class NormalEnemyAttack : IAttack
{
    public Unit Unit;
    public bool IsCheck = true;
    public TimeAgent AttackTimeAgent;
    public NormalEnemyAttack(Unit unit)
    {
        this.Unit = unit;
    }
    public void Attack()
    {
        Unit.BulletPrefab.Speed = Unit.unitStates.AttackSpeed;
        Unit.BulletPrefab.Power = Unit.unitStates.Power;
        AttackTimeAgent = new(Unit.unitStates.SkillCoolTime[0], (timeAgent) => IsCheck = false, (timeAgent) => { }, (timeAgent) => IsCheck = true);
        if (IsCheck)
        {
            TimerSystem.Instance.AddTimer(AttackTimeAgent);
            ObjectPool.Instance.Pooling(Unit.transform, Unit.BulletPrefab.gameObject, Unit.BulletPrefab.Layer);
        }
    }
}
