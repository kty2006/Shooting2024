using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour, IVisitElement
{
    public TimeAgent BulletTimeAgent;
    public LayerMask Layer;
    public ParticleSystem HitEffect;
    public Vector3 Dir;
    public int Speed;
    public float Power;
    public bool IsCheck;
    public void OnEnable()
    {
        Dir = IsCheck switch
        {
            true => Vector3.forward,
            false => -Vector3.forward,
        };
        BulletTimeAgent = new(10, (timeAgent) => { }, (timeAgent) => transform.Translate(Dir * Time.fixedDeltaTime * Speed), (timeAgent) => ObjectPool.Instance.EnqueuePool(gameObject)); //�̰� ����ȭ
        TimerSystem.Instance.AddTimer(BulletTimeAgent);
        ObjectPool.Instance.AddPool(HitEffect.gameObject.tag);
    }

    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.TryGetComponent(out Unit unit))
    //    {
    //        //Accept(EffectCommand.PlayerBoomHit); //EffectCommand�� ��� ������ �ϳ� ����� ���Ⱑ �ٲ𶧸��� hit��ƼŬ�� �ٲ�� ���� �� ����
    //    }
    //}

    public void Accept(IVisit visit)
    {
        visit.Visit(transform.position);
    }
}
