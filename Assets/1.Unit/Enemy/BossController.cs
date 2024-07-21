using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public List<Enemy> Enemies = new();
    public Transform StartPos;
    

    public void Start()
    {
        //InstantiateBoss();
        StartCoroutine(BossAction());
    }

    //public void InstantiateBoss()
    //{
    //    if (Instantiate(Enemies, StartPos.position, Quaternion.identity).TryGetComponent(out Enemy enemy))
    //    {
    //        Enemies = enemy;
    //    }
    //}

    public IEnumerator BossAction()
    {
        yield return new WaitUntil(() => GameManager.Instance.BossGameStart);
        while (true)
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.CurrentWeapon.Attack();
            }
            yield return new WaitForSeconds(3);
        }
    }
}
