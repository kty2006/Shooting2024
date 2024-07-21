using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleTone<GameManager>
{
    public bool GameStart;
    public bool BossGameStart;
    public float Progress = 0;
    public SequenceExecutor boss1Sequence;
    private void OnEnable()
    {

        StartCoroutine(Boss1Sequence());
    }

    public void Start()
    {
        SettingType();
    }
    private void Update()
    {
        if (GameStart)
        {
            Progress += Time.deltaTime;
            Player.Instance.MoveType.Move();
            Player.Instance.CurrentWeapon.Attack();
            Player.Instance.SkillType.Skill();

        }
    }

    private void SettingType()
    {
        Player.Instance.ChangeType(new PlayerMove(Player.Instance));
        Player.Instance.ChangeType(new PlayerNormalAttack(Player.Instance, 50, 0));
        Player.Instance.ChangeType(new LaserSkill(Player.Instance, 0.1f, 2));
    }

    public void GameStartSequence()
    {
        GameStart = true;
        GameObject EnemyManager = transform.GetChild(0).gameObject;
        EnemyManager.SetActive(true);
    }


    public IEnumerator Boss1Sequence()
    {
        yield return new WaitUntil(() => Progress >= 120);
        CameraShake.Instance.Shake(14.5f, 6f);
        Collider[] AllObj = FindObjectsOfType<Collider>();
        foreach (var obj in AllObj)
        {
           if(obj.transform.gameObject.layer == 14 || obj.transform.gameObject.layer == 6)
            {
                ObjectPool.Instance.EnqueuePool(obj.gameObject);
            }
        }
        StartCoroutine(boss1Sequence.PlaySequence(() => { BossGameStart = true; Debug.Log("º¸½ºÀü"); }));
    }
}
