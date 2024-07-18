using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NormalEnemy : Enemy
{
    public Coroutine NormalEnemyTime;
    public void OnEnable()
    {
        HpUISet();
        NormalEnemyTime = StartCoroutine(EnemyTime());
    }

    protected override void Start()
    {
        base.Start();
        ChangeType(new EnemyMove(this));
        ChangeType(new NormalEnemyAttack(this, 1, 0));
    }

    protected override void Update()
    {
        base.Update();
        HpUIUpdate();
    }
    public void OnDisable()
    {
        EnqueuHpUI();
        StopCoroutine(NormalEnemyTime);
    }

    public override void Death()
    {
        DropItem(1);
        EnqueuEnemy();
        base.Death();
    }

    public IEnumerator EnemyTime()
    {
        yield return new WaitForSeconds(10);
        ObjectPool.Instance.EnqueuePool(gameObject);
    }

    public void HpUISet()
    {
        //������ ü�� �ʱ�ȭ �ؾ���
        unitStates.Hp = unitStates.MaxHp;
        ObjectPool.Instance.AddPool(HpUIObj.HpUI.gameObject.tag);
        if (ObjectPool.Instance.Pooling(transform.position, Quaternion.identity, HpUIObj.HpUI.gameObject).TryGetComponent(out Image HPUHD))
        {
            HpUIObj.HpUI = HPUHD;
        }
        UIBundle bundle = HpUIObj.HpUI.GetComponentInChildren<UIBundle>();
        HpUIObj.Hp = bundle.GetComponentInChildren<Text>();
        HpUIObj.HpFill = bundle.GetComponentInChildren<Image>();
        HpUI();
        HpUIObj.HpUI.transform.parent = UIManager.Instance.transform;
    }

    public void HpUIUpdate()
    {
        HpUIObj.HpUI.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 10, 0));
    }

    public void EnqueuHpUI()
    {
        ObjectPool.Instance.EnqueuePool(HpUIObj.HpUI.gameObject);
    }
}
