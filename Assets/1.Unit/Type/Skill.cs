using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserSkill : ISkill
{
    //public LineRenderer Laser;
    //public ParticleSystem Charging;
    //public float currentTime;

    public Unit Unit;
    public TimeAgent SkillTimeAgent;
    public bool IsCheck = true;
    public float times;
    public int index;
    private GameObject laserObj;
    private float CoolTime;
    public LaserSkill(Unit unit, float times, int index)
    {
        this.Unit = unit;
        this.times = times;
        this.index = index;
        CoolTime = Unit.unitStates.SkillCoolTime[index];
        SkillTimeAgent = new(CoolTime, (timeAgent) => IsCheck = false, (timeAgent) => { SkillUi.Instance.SkillUiFill(CoolTime); }, (timeAgent) => IsCheck = true);
        laserObj = Unit.EffectData.CreateLaserEffect(Unit.transform.position + new Vector3(0, 0, 20), Quaternion.identity);
        TimerSystem.Instance.AddTimer(SkillTimeAgent);
        laserObj.transform.parent = Unit.transform;
        //Laser = laserObj.GetComponentInChildren<LineRenderer>();
        //Charging = laserObj.GetComponentInChildren<ParticleSystem>();
    }

    public void Skill()
    {
        SkillTimeAgent = new(Unit.unitStates.SkillCoolTime[index], (timeAgent) => IsCheck = false, (timeAgent) => { }, (timeAgent) => IsCheck = true);
        Unit.NormalBulletPrefab.Power = Unit.unitStates.Power * times;
        if (Input.GetKey(KeyCode.Z) && IsCheck)
        {
            if (laserObj.TryGetComponent(out LaserObj laser))
            {
                laser.StartCoroutine(laser.DuringLaser());
                Debug.Log("∑π¿Ã¿˙");
                TimerSystem.Instance.AddTimer(SkillTimeAgent);
            }

        }
    }

}
