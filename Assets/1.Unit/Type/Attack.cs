using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;


public class NormalAttack : IAttack
{
    public Unit Unit;
    public bool IsCheck = true;
    public TimeAgent AttackTimeAgent;
    public float times;
    public int index;
    public virtual void Attack()
    {
        Unit.NormalBulletPrefab.Speed = Unit.unitStates.AttackSpeed;
        Unit.NormalBulletPrefab.Power = Unit.unitStates.Power * times;
        AttackTimeAgent = new(Unit.unitStates.SkillCoolTime[index], (timeAgent) => IsCheck = false, (timeAgent) => { }, (timeAgent) => IsCheck = true);
    }
}
public class PlayerNormalAttack : NormalAttack
{
    public PlayerNormalAttack(Unit unit, int times, int index)
    {
        this.Unit = unit;
        this.times = times;
        this.index = index;
    }
    public override void Attack()
    {
        base.Attack();
        if (Input.GetKey(KeyCode.Space) && IsCheck)
        {
            TimerSystem.Instance.AddTimer(AttackTimeAgent);
            ObjectPool.Instance.Pooling(Unit.transform.position, Quaternion.Euler(0, 180, 0), Unit.NormalBulletPrefab.gameObject);
        }
    }
}

public class PlayerAssiantAttack : NormalAttack
{
    States states;
    GameObject[] AssianntGun;
    public PlayerAssiantAttack(Unit unit, int times, int index, GameObject[] AssianntGun)
    {
        this.Unit = unit;
        this.times = times;
        this.index = index;
        this.AssianntGun = AssianntGun;
        states = Unit.GetStates();
    }
    public override void Attack()
    {
        base.Attack();
        if (Input.GetKey(KeyCode.Space) && IsCheck)
        {
            TimerSystem.Instance.AddTimer(AttackTimeAgent);
            for (int i = 0; i < states.ItemLv; i++)
            {
                ObjectPool.Instance.Pooling(AssianntGun[i].transform.position, Quaternion.Euler(0, 180, 0), Unit.NormalBulletPrefab.gameObject);
            }
        }
    }
}

public class PlayerBoomAttack : NormalAttack
{
    ParticleSystem Charging;
    public PlayerBoomAttack(Unit unit, int index)
    {
        this.Unit = unit;
        this.index = index;
        //ObjectPool.Instance.AddPool(Unit.EffectData.ChargingEffect.gameObject.tag);
        Charging = Unit.EffectData.CreateChargingEffect(unit.transform.position, Quaternion.identity);
    }

    public override void Attack()
    {
        base.Attack();
        if (IsCheck)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                times = Mathf.Clamp((times + Time.deltaTime * 10), 1, 200);
                Unit.NormalBulletPrefab.Power = Unit.unitStates.Power * times;
                EffectPlay();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                TimerSystem.Instance.AddTimer(AttackTimeAgent);
                ObjectPool.Instance.Pooling(Unit.transform.position, Quaternion.Euler(0, 180, 0), Unit.NormalBulletPrefab.gameObject);
                times = 0;
                EffectPlay();
            }
        }
    }
    public void EffectPlay()
    {
        if (times != 0)
        {
            if (!Charging.isPlaying)
                Charging.Play();
            Charging.transform.position = Unit.transform.position + new Vector3(0, 0, 31);
        }
        else if (times == 0)
        {
            Charging.Stop();
        }
    }
}
public class NormalEnemyAttack : NormalAttack
{
    public NormalEnemyAttack(Unit unit, int times, int index)
    {
        this.Unit = unit;
        this.times = times;
        this.index = index;
    }
    public override void Attack()
    {
        if (IsCheck)
        {
            base.Attack();
            TimerSystem.Instance.AddTimer(AttackTimeAgent);
            ObjectPool.Instance.Pooling(Unit.transform.position, Unit.transform.rotation, Unit.NormalBulletPrefab.gameObject);
        }
    }
}

