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
    public void Set(float value) //���� ����Ҷ����� �������� �ִ��� Ȯ��
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
//unit���� �÷��̾� ���� �ڽ����� �δϱ� ������ �Ϲ�/���� �� ���������� ������ ���������ٰ� ������ unit�� ������ �ʰ�
//�÷��̾�, ���� ����� �������� ��Ҵ� �׳� Ŭ������ ������� �������̽��� �и��� ����ϴ°��� �� ������ ����.
{
    public States unitStates = new(); //���������� ���� ������ ���� ���� ���ϰ� �Լ��� ����� ���� ������ null�� �Ѵٴ��� ���� ����
    public HpUIObj HpUIObj = new();
    public IAttack CurrentWeapon;
    public IMove MoveType;
    public ISkill SkillType;
    public IVisit UIHp;
    public EffectData EffectData;
    public Bullet NormalBulletPrefab;
    protected Bullet currentHitBullet;
    public bool death;
    protected virtual void Start()
    {
        BulletPool();
    }

    protected virtual void Update()
    {
        HpUI();
        if(!death)
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
        Vector3 dir; //��� ���������� �ٸ����� ������� �ʱ� ������ 
        dir = targetPos - startPos;
        dir.Normalize();
        return Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
    }

    public virtual void BulletPool()
    {
        ObjectPool.Instance.AddPool(NormalBulletPrefab.gameObject.tag);
    }

    public virtual void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.TryGetComponent(out Bullet bullet))
        {
            currentHitBullet = bullet;
            CountHp(currentHitBullet.Power);
            HitEffectPlay();
        }
    }

    public virtual void CountHp(float power)
    {
        unitStates.Hp -= power;
    }

    public virtual void Death()
    {
        
    }

    [ContextMenu("LvUp")]
    public void LvUp() // ContexttMenu��� ������ ���� ���� Ŭ�������� ���� ���
    {
        unitStates.MaxHp = 90;
        unitStates.MaxHp += 10 * unitStates.Lv;
        unitStates.Hp = unitStates.MaxHp;
        unitStates.Power = unitStates.Lv;
        HpUI();
    }

    public States GetStates() //������ ����Ҷ� ���� ������ �������� ������ ���� ���� ���
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