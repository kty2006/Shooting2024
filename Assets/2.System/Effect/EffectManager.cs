using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerBoomParticle : IVisit
{
    public ParticleSystem ChargingEffect;
    public PlayerBoomParticle(out IVisit visit)
    {
        visit = this;
    }
    public void Visit(NormalAttack element)
    {
        if (element.times != 0)
        {
            if (!ChargingEffect.isPlaying)
                ChargingEffect.Play();
            ChargingEffect.transform.position = element.Unit.transform.position + new Vector3(0, 0, 31);
        }
        else if (element.times == 0)
        {
            ChargingEffect.Stop();
        }
    }
}

[Serializable]
public class PlayerBoomHitParticle : IVisit //비지터 패턴에 대한 생각을 한번더//고쳐야함
{
    public ParticleSystem ChargingEffect;
    public PlayerBoomHitParticle(out IVisit visit)
    {
        visit = this;
    }
    public void Visit(Vector3 Pos)
    {
        ChargingEffect.transform.position = Pos;
        ChargingEffect.Play();
    }
    public class EffectManager : MonoBehaviour
    {
        public PlayerBoomParticle BoomParticle = new(out EffectCommand.PlayerBoom);
        public PlayerBoomHitParticle BoomHitParticle = new(out EffectCommand.PlayerBoomHit);
    }
}
