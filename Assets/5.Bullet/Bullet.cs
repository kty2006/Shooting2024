using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public TimeAgent BulletTimeAgent;
    public ParticleSystem HitEffect;
    public Vector3 Dir;
    public int Speed;
    public float Power;
    public bool IsCheck;
    public void OnEnable()
    {
        DirCheck();
        BulletTimeAgent = new(10, (timeAgent) => { }, (timeAgent) => transform.Translate(Dir * Time.fixedDeltaTime * Speed), (timeAgent) => ObjectPool.Instance.EnqueuePool(gameObject)); //�̰� ����ȭ
        TimerSystem.Instance.AddTimer(BulletTimeAgent);
        ObjectPool.Instance.AddPool(HitEffect.gameObject.tag);
    }

    public void DirCheck()
    {
        Dir = IsCheck switch
        {
            true => Vector3.forward,
            false => -Vector3.forward,
        };
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out Player unit))
        {
            CameraShake.Instance.Shake(0.25f, 0.6f);
            UIManager.Instance.HitCheck();
            StartCoroutine(unit.GodTime(Color.clear, 1));
            Debug.Log("��Ÿ��");
            //Accept(EffectCommand.PlayerBoomHit); //EffectCommand�� ��� ������ �ϳ� ����� ���Ⱑ �ٲ𶧸��� hit��ƼŬ�� �ٲ�� ���� �� ����
        }
    }
}
