using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObj : MonoBehaviour
{
    public LineRenderer Laser;
    public ParticleSystem Charging;
    public GameObject laserObj;
    public float currentTime;

    //public LaserObj(GameObject laserObj)
    //{
    //    this.laserObj = laserObj;
    //}

    public IEnumerator DuringLaser()
    {
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
            RaycastHit[] hit = Physics.SphereCastAll(transform.position, 50, Vector3.forward, 100, LayerMask.GetMask("Enemy"));
            foreach (var ray in hit)
            {
                //if(ray)
            }
        }
    }
}
