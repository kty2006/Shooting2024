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
    }

    private void SettingType()
    {
        Player.Instance.ChangeType(new PlayerMove(Player.Instance));
        Player.Instance.ChangeType(new PlayerNormalAttack(Player.Instance));
    }

    
}
