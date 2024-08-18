using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoSingleTone<BossController>
{
    public List<Enemy> Enemies = new();
    public int waittTime = 3;
    public int EndIndex = 2;
    public int startIndex = 0;
    public Coroutine Bosscoroutine;
    public bool StopBossCoroutine;
    public void OnEnable()
    {
        //InstantiateBoss();
        StopBossCoroutine = false;
        Bosscoroutine = StartCoroutine(BossAction());
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
        while (!StopBossCoroutine)
        {
            for (int i = startIndex; i < EndIndex; i++)
            {
                Enemies[i].CurrentWeapon.Attack();
                Debug.Log("보스공격");
            }
            yield return new WaitForSeconds(waittTime);
            //foreach (Enemy enemy in Enemies)
            //{
            //    enemy.CurrentWeapon.Attack();
            //}
        }
    }
}
