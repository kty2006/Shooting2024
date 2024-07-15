using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using System.Linq;

public class Enemy : Unit
{
    public LayerMask TypeLayer;
    protected Image hpUI;
    public ItemData itemData;
    protected override void Start()
    {
        base.Start();
    }

    public virtual void OnEnable()
    {
        HpUISet();
    }

    public void OnDisable()
    {
        EnqueuHpUi();
    }

    protected override void Update()
    {
        base.Update();
        HpUIUpdate();
    }


    public override void Accept(IVisit visit)
    {
        visit.Visit(unitStates, transform.position, hpUI);
    }

    public void HpUISet()
    {
        //생성시 체력 초기화 해야함
        unitStates.Hp = unitStates.MaxHp;
        ObjectPool.Instance.AddPool(HpUIObj.HpUI.gameObject.tag);
        HpUIObj.HpUI = ObjectPool.Instance.Pooling(transform.position, Quaternion.identity, HpUIObj.HpUI.gameObject).ConvertTo<Image>();
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

    public override void HitEffectPlay()
    {
        ObjectPool.Instance.Pooling
            (transform.position, Quaternion.Euler(0, 180, 0), currentHitBullet.HitEffect.gameObject);
    }

    public void EnqueuEnemy()
    {
        ObjectPool.Instance.EnqueuePool(gameObject);
    }

    public void EnqueuHpUi()
    {
        ObjectPool.Instance.EnqueuePool(HpUIObj.HpUI.gameObject);
    }


    public void DropItem(int count)
    {
        if (count == 1)
        {
            int countn = Random.Range(0, itemData.Items.Count());
            Debug.Log(countn);
            ObjectPool.Instance.Pooling(transform.position, Quaternion.identity, itemData.Items[countn].gameObject);
        }
        else
        {
            for (int i = 0; i < count; i++)
                ObjectPool.Instance.Pooling(transform.position, Quaternion.identity, itemData.Items[Random.Range(0, itemData.Items.Count())].gameObject);
        }

    }

    public override void HitAction()
    {
        throw new NotImplementedException();
    }
}
