using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleTone<GameManager>
{
    public bool GameReStart = false;
    public bool Stage1Clear = false;
    public bool GameStart;
    public bool BossGameStart;
    public float Progress = 0;
    public SequenceExecutor boss1Sequence;
    public SequenceExecutor boss2Sequence;
    public SequenceExecutor reStartSequence;
    private void OnEnable()
    {

        StartCoroutine(Boss1Sequence());
        StartCoroutine(ReStartMapSequence());
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
            if (obj.transform.gameObject.layer == 14 || obj.transform.gameObject.layer == 6 || obj.transform.gameObject.layer == 12)
            {
                ObjectPool.Instance.EnqueuePool(obj.gameObject);
            }
        }
        StartCoroutine(boss1Sequence.PlaySequence(() => { BossGameStart = true; Debug.Log("º¸½ºÀü"); }));
    }

    public IEnumerator ReStartMapSequence()
    {
        yield return new WaitUntil(() => GameReStart);
        CameraShake.Instance.Shake(2, 6f);
        StopCoroutine(BossController.Instance.Bosscoroutine);
        Collider[] AllObj = FindObjectsOfType<Collider>();
        foreach (var obj in AllObj)
        {
            if (obj.transform.gameObject.layer == 6 || obj.transform.gameObject.layer == 12)
            {
                Destroy(obj.gameObject);
            }
        }
        GameStart = false;
        BossGameStart = false;
        StartCoroutine(reStartSequence.PlaySequence(() => { GameStartSequence(); Progress = 0; GameStart = true; Stage1Clear = true; }));
        StartCoroutine(Boss2Sequence());
    }

    public IEnumerator Boss2Sequence()
    {
        yield return new WaitUntil(() => Progress >= 120 && Stage1Clear);
        CameraShake.Instance.Shake(8f, 6f);
        Collider[] AllObj = FindObjectsOfType<Collider>();
        foreach (var obj in AllObj)
        {
            if (obj.transform.gameObject.layer == 14 || obj.transform.gameObject.layer == 6 || obj.transform.gameObject.layer == 12)
            {
                ObjectPool.Instance.EnqueuePool(obj.gameObject);
            }
        }
        StartCoroutine(boss2Sequence.PlaySequence(() =>
        {
            BossController.Instance.waittTime = 7;
            BossController.Instance.startIndex = 2;
            BossController.Instance.EndIndex = 3;
            BossGameStart = true;
        }));
    }
}
