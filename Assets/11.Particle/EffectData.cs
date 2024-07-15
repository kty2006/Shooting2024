using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectData", menuName = "EffectData", order = 1)]
public class EffectData : ScriptableObject
{
    public ParticleSystem ChargingEffect;
    public ParticleSystem CannonHitEffect;
    public ParticleSystem CannonNormalHitEffect;
    public ParticleSystem CannonLaserEffect;
    public GameObject LaserEffect;
    public ParticleSystem CreateChargingEffect(Vector3 pos, Quaternion rotate)
    {
        return Instantiate(ChargingEffect, pos, rotate);
    }
    public GameObject CreateLaserEffect(Vector3 pos, Quaternion rotate)
    {
        return Instantiate(LaserEffect, pos, rotate);
    }
}
