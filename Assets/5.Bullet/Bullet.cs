using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem HitEffect;
    public Vector3 Dir;
    public int Speed;
    public float Power;
    public bool IsCheck;
    public void OnEnable()
    {
        DirCheck();
        StartCoroutine(BulletTimeAgent());
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
        if (!other.transform.TryGetComponent(out Bullet bullet))
        {
            ObjectPool.Instance.EnqueuePool(gameObject);
        }
    }

    public void FixedUpdate()
    {
        transform.Translate(Dir * Time.fixedDeltaTime * Speed);
    }
    public IEnumerator BulletTimeAgent()
    {
        yield return new WaitForSeconds(2);
        ObjectPool.Instance.EnqueuePool(gameObject);
    }
}
