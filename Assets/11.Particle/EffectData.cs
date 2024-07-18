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
    public ParticleSystem DragonFireEffect;
    public ParticleSystem WindEffect;
    public ParticleSystem Tornado;
    public GameObject LaserEffect;
    public ParticleSystem CreateChargingEffect(Vector3 pos, Quaternion rotate)
    {
        return Instantiate(ChargingEffect, pos, rotate);
    }
    public GameObject CreateLaserEffect(Vector3 pos, Quaternion rotate)
    {
        return Instantiate(LaserEffect, pos, rotate);
    }
    public ParticleSystem CreateFire(Vector3 pos, Quaternion rotate)
    {
        return Instantiate(DragonFireEffect, pos, rotate);
    }
    public ParticleSystem CreateWind(Vector3 pos, Quaternion rotate)
    {
        return Instantiate(WindEffect, pos, rotate);
    }
    public ParticleSystem CreateTornado(Vector3 pos, Quaternion rotate)
    {
        return Instantiate(Tornado, pos, rotate);
    }
}
