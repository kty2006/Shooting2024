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
    protected Image hpUI;
    public ItemData itemData;
    protected override void Start()
    {
        base.Start();
    }

    public override void HitEffectPlay()
    {
        ObjectPool.Instance.Pooling
            (transform.position, Quaternion.Euler(0, 180, 0), currentHitBullet.HitEffect.gameObject);
    }

    public virtual void EnqueuEnemy()
    {
        ObjectPool.Instance.EnqueuePool(gameObject);
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
