using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        SettingType();
    }

    private void Update()
    {
        Player.Instance.MoveType.Move();
        Player.Instance.CurrentWeapon.Attack();
        if(Input.GetKey(KeyCode.Z))
            Player.Instance.SkillType.Skill();

    }

    private void SettingType()
    {
        Player.Instance.ChangeType(new PlayerMove(Player.Instance));
        Player.Instance.ChangeType(new PlayerNormalAttack(Player.Instance, 50, 0));
        Player.Instance.ChangeType(new LaserSkill(Player.Instance, 0.1f, 2));
        //Player.Instance.ChangeType(new PlayerBoomAttack(Player.Instance, 1));
        //Player.Instance.ChangeType(new PlayerAssiantAttack(Player.Instance, 50, 0, Player.Instance.AssiantGuns));
    }


}
