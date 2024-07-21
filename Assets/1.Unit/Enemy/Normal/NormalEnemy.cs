using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NormalEnemy : Enemy
{
    public Coroutine NormalEnemyTime;
    public int DieTime;
    public void OnEnable()
    {
        HpUISet();
        NormalEnemyTime = StartCoroutine(EnemyTime());
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
        base.Death();
        DropItem(1);
        EnqueuEnemy();
        Debug.Log("hpui죽음");
    }

    public IEnumerator EnemyTime()
    {
        yield return new WaitForSeconds(13);
        EnqueuEnemy();
    }

    public void HpUISet()
    {
        //생성시 체력 초기화 해야함
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

    public virtual void EnqueuHpUI()
    {
        ObjectPool.Instance.EnqueuePool(HpUIObj.HpUI.gameObject);
    }
}
