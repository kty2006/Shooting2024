using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Enemy Enemies = new();
    public Transform StartPos;
    

    public void Start()
    {
        //InstantiateBoss();
        StartCoroutine(BossAction());
    }

    public void InstantiateBoss()
    {
        if (Instantiate(Enemies, StartPos.position, Quaternion.identity).TryGetComponent(out Enemy enemy))
        {
            Enemies = enemy;
        }
    }

    public IEnumerator BossAction()
    {
        while (true)
        {
            Enemies.CurrentWeapon.Attack();
            yield return new WaitForSeconds(3);
        }
    }
}
