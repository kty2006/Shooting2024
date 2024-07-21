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
        laserObj = Unit.EffectData.CreateLaserEffect(Unit.transform.position + new Vector3(0, 0, 30), Quaternion.identity);
        laserObj.transform.parent = Unit.transform;
        TimerSystem.Instance.AddTimer(SkillTimeAgent);
        //Laser = laserObj.GetComponentInChildren<LineRenderer>();
        //Charging = laserObj.GetComponentInChildren<ParticleSystem>();
    }

    public void Skill()
    {

        //TimerSystem.Instance.AddTimer(SkillTimeAgent);
        SkillTimeAgent = new(CoolTime, (timeAgent) => IsCheck = false, (timeAgent) => { }, (timeAgent) => IsCheck = true);
        if (laserObj.TryGetComponent(out LaserObj laser))
        {
            laser.Power = Unit.unitStates.Power * times * 0.2f;
            if (Input.GetKey(KeyCode.Z) && IsCheck)
            {
                laser.gameObject.SetActive(true);
                if (laser.PlayerLaserCoroutine == null)
                {
                    laser.PlayerLaserCoroutine = laser.StartCoroutine(laser.PlayerDuringLaser());
                    IsCheck = false;
                    //TimerSystem.Instance.AddTimer(SkillTimeAgent);
                }
            }
            if (laser.PlayerLaserCheck)
            {
                if(SkillUi.Instance.SkillUiFill(CoolTime))
                {
                    IsCheck = true;
                }
            }
        }
    }

}
