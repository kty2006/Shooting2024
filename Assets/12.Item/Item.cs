using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ItemType { HpUp, GodTime, Bomb, Cannon, AssiantGun }
public class Item : MonoBehaviour
{
    public ItemType Type;
    public Action<Player> Action;
    public int speed;
    public float MinZPos;
    public void Awake()
    {
        ItemSelect();
    }
    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 5, 0) * transform.rotation;
        if (transform.position.z > MinZPos)
            transform.Translate(-Vector3.forward * speed, Space.World);
    }
    public void ItemSelect()
    {
        Action = Type switch
        {
            ItemType.HpUp => (Player unit) => { unit.GetStates().Hp = unit.GetStates().MaxHp; unit.HpUI(); }
            ,

            ItemType.GodTime => (Player unit) => { unit.GodTime(Color.yellow, 7); }
            ,

            ItemType.Bomb => (Player unit) => { Instantiate(unit.Bomb, unit.transform.position, Quaternion.identity); }
            ,

            ItemType.Cannon => (Player unit) => { unit.ChangeWeapon(new PlayerBoomAttack(unit, 1)); }
            ,

            ItemType.AssiantGun => (Player unit) => { unit.ChangeWeapon(new PlayerAssiantAttack(unit, 50, 0, unit.AssiantGuns)); }

        };
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player unit))
        {
            Action(unit);
            ObjectPool.Instance.EnqueuePool(gameObject);
        }
    }
}
