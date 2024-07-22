using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObj : MonoBehaviour
{
    public LineRenderer Laser;
    public ParticleSystem Charging;
    public GameObject laserObj;
    public float currentTime;
    public float Power;
    public Coroutine EnemyLaserCoroutine;
    public Coroutine PlayerLaserCoroutine;
    public bool PlayerLaserCheck = false;
    public Vector3 dir;
    RaycastHit[] hit;
    public void OnDisable()
    {
        if (EnemyLaserCoroutine != null)
            StopCoroutine(EnemyLaserCoroutine);
        if (PlayerLaserCoroutine != null)
            StopCoroutine(PlayerLaserCoroutine);
        currentTime = 0;
        EnemyLaserCoroutine = null;
    }
    public void Update()
    {
        if (EnemyLaserCoroutine != null)
        {
            hit = Physics.SphereCastAll(transform.position, 10, dir, 700, LayerMask.GetMask("Unit"));
            foreach (var ray in hit)
            {
                if (ray.collider.gameObject.TryGetComponent(out Player unit))
                {
                    unit.GetStates().Hp -= Power;
                    CameraShake.Instance.Shake(0.25f, 0.6f);
                    UIManager.Instance.HitCheck();
                    StartCoroutine(unit.GodTime(Color.clear, 0.1f));
                }
            }
        }
        if (PlayerLaserCoroutine != null && Laser.gameObject.activeSelf)
        {
            hit = Physics.SphereCastAll(transform.position, 10, dir, 500, LayerMask.GetMask("Enemy"));
            foreach (var ray in hit)
            {
                if (ray.collider.gameObject.TryGetComponent(out Unit unit))
                {
                    unit.GetStates().Hp -= Power;
                }
            }
        }
    }
    public IEnumerator PlayerDuringLaser()
    {
        PlayerLaserCheck = false;
        dir = Vector3.forward;
        for (currentTime = 0; currentTime <= 10 && Input.GetKey(KeyCode.Z); currentTime += 0.02f)
        {
            //Debug.Log(currentTime);
            Charging.gameObject.SetActive(true);
            yield return null;
        }
        Charging.gameObject.SetActive(false);
        Laser.gameObject.SetActive(true);
        yield return null;
        for (; currentTime > 0; currentTime -= 0.02f)
        {
            Laser.SetWidth(currentTime, currentTime);
            SkillUi.Instance.UnSkillUiFill(currentTime);
            yield return null;
        }
        PlayerLaserCoroutine = null;
        PlayerLaserCheck = true;
        gameObject.SetActive(false);
    }

    public IEnumerator MonsterDuringLaser()
    {
        dir = -Vector3.forward;
        for (currentTime = 0; currentTime <= 7; currentTime += 0.02f)
        {
            Charging.gameObject.SetActive(true);
            yield return null;
        }
        Charging.gameObject.SetActive(false);
        Laser.gameObject.SetActive(true);
        yield return null;
        for (; currentTime > 0; currentTime -= 0.02f)
        {
            Laser.SetWidth(currentTime, currentTime);
            yield return null;
        }

        yield return null;
        EnemyLaserCoroutine = null;
    }
}
