using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleTone<GameManager>
{
    public bool GameStart;
    public float Progress = 0;
    private void Start()
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

}
