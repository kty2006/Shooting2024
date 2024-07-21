using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Random = UnityEngine.Random;
using System.Xml.Linq;

[Serializable]
public class States
{
    public float MaxHp = 100;
    public float Hp;
    public static int MaxLv = 20;
    public int Lv;
    public int ItemLv;
    public int AttackSpeed;
    public int MoveSpeed;
    public int Exp;
    public float Power;
    public float[] SkillCoolTime;

    public void Set(float value) //스텟 사용할때마다 변경점이 있는지 확인
    {
        if (Power == value)
        { return; };
        Power = value;
    }

    public void Set(int value)
    {
        if (Power == value)
        { return; };
        Power = value;
    }
}

[Serializable]
public class HpUIObj
{
    public Text Hp;
    public Image HpFill;
    public Image HpUI;
}

public abstract class Unit : MonoBehaviour, IVisitElement 
//unit에서 플레이어 적을 자식으로 두니까 적에서 일반/보스 또 나누어질때 구조가 복잡해진다고 느껴서 unit을 만들지 않고
//플레이어, 적을 만들고 공통적인 요소는 그냥 클래스를 만들던가 인터페이스로 분리해 사용하는것이 더 좋을거 같다.
{
    public States unitStates = new(); //직접적으로 스텟 변수를 접근 하지 못하게 함수를 사용한 접근 변수를 null로 한다던가 접근 못함
    public HpUIObj HpUIObj = new();
    public IAttack CurrentWeapon;
    public IMove MoveType;
    public ISkill SkillType;
    public IVisit UIHp;
    public EffectData EffectData;
    public Bullet NormalBulletPrefab;
    protected Bullet currentHitBullet;

    protected virtual void Start()
    {
        BulletPool();
    }

    protected virtual void Update()
    {
        HpUI();
        if (unitStates.Hp <= 0)
            Death();
    }
    public virtual void ChangeType(IAttack attack)
    {
        CurrentWeapon = attack;
    }

    public void ChangeType(IMove move)
    {
        MoveType = move;
    }

    public void ChangeType(ISkill skill)
    {
        SkillType = skill;
    }

    public float CheckRotate(Vector3 startPos, Vector3 targetPos)
    {
        Vector3 dir; //계속 생성되지만 다른데서 사용하지 않기 때문에 
        dir = targetPos - startPos;
        dir.Normalize();
        return Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
    }

    public virtual void BulletPool()
    {
        ObjectPool.Instance.AddPool(NormalBulletPrefab.gameObject.tag);
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.TryGetComponent(out Bullet bullet))
        {
            currentHitBullet = bullet;
            unitStates.Hp -= currentHitBullet.Power;
            HitEffectPlay();
        }
    }
    public virtual void Death()
    {
        if (gameObject.layer != 8) //적유닛이 죽을때 플레이어에게 경험치 적용 적유닛에서 플레이어에 접근하는것이기 때문에 객체지향 위반 
        {
            Player.Instance.KillCountUp(this);
        }
    }

    [ContextMenu("LvUp")]
    public void LvUp() // ContexttMenu사용 때문에 따로 스텟 클래스에서 빼서 사용
    {
        unitStates.MaxHp = 90;
        unitStates.MaxHp += Mathf.Clamp(10 * unitStates.Lv, 0, 200);
        unitStates.Hp = unitStates.MaxHp;
        unitStates.Power = Mathf.Clamp(unitStates.Lv, 0, 20);
        HpUI();
    }

    public States GetStates() //스텟을 사용할때 스텟 변수에 직접적인 접근을 막기 위해 사용
    {
        return unitStates;
    }

    public virtual void Accept(IVisit visit)
    {
        visit.Visit(unitStates);
    }
    public virtual void HpUI()
    {
        HpUIObj.Hp.text = $"HP {((int)unitStates.Hp)}";
        HpUIObj.HpFill.fillAmount = unitStates.Hp / unitStates.MaxHp;
    }

    public abstract void HitEffectPlay();

    public abstract void HitAction();
}