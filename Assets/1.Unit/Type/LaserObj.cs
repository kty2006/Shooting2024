using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObj : MonoBehaviour
{
    public LineRenderer Laser;
    public ParticleSystem Charging;
    public float currentTime;
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
        for (; currentTime > 0; currentTime -= 0.02f)
        {
            Laser.SetWidth(currentTime, currentTime);
            SkillUi.Instance.SkillUiFill(5);
            yield return null;
            RaycastHit[] hit = Physics.SphereCastAll(transform.position, 50, Vector3.forward, 100, LayerMask.GetMask("Enemy"));
            foreach (var ray in hit)
            {
                //if(ray)
            }
        }
    }
}
