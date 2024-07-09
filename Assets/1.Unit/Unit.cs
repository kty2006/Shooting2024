using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class States
{
    public Unit unit;
    public float hp;
    public float Hp { get => hp; set { hp -= value; if (hp <= 0) { unit.Death(); }; } }
    public int Lv;
    public int AttackSpeed;
    public int MoveSpeed;
    public float Exp;
    public float Power; /*{ get => Power; set {  }*/
    public float[] SkillCoolTime;

    public float GetPower()
    {
        return Power;
    }

    public void SetPower(float value)
    {
        if (Power == value)
        { return; };
        Power = value;
    }

    public States(Unit unit)
    {
        this.unit = unit;
    }
}
public class Unit : MonoBehaviour
{
    public States unitStates; //���������� ���� ������ ���� ���� ���ϰ� �Լ��� ����� ���� ������ null�� �Ѵٴ��� ���� ����
    public IAttack CurrentWeapon;
    public IMove MoveType;
    public ISkill SkillType;
    public Bullet BulletPrefab;

    public virtual void Start()
    {
        BulletPool();
        unitStates = new States(this);
    }

    public void ChangeType(IAttack attack)
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
        ObjectPool.Instance.AddPool(BulletPrefab.Layer);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Bullet bullet))
        {
            Color saveColor = Color.black;
            gameObject.TryGetComponent(out MeshRenderer mesh);
            TimeAgent hit = new(1, (timeAgent) => { saveColor = mesh.material.color; }, (timeAgent) => { mesh.material.color = Random.ColorHSV(); }, (timeAgent) => { mesh.material.color = saveColor; }); ;
            unitStates.Hp -= bullet.Power;
            TimerSystem.Instance.AddTimer(hit);
        }
    }

    public virtual void Death()
    {
        //Unit u = new();
        //var check = u.GetStates();
        //check.Hp = 0;
    }

    public States GetStates()
    {
        return unitStates;
    }

    public
}