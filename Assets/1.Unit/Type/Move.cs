using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : IMove
{
    public float Horizontal;
    public float Vertical;
    public Vector3 Dir;
    public Unit Unit;

    public PlayerMove(Unit unit)
    {
        this.Unit = unit;
    }

    public void Move()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        Unit.transform.rotation = Quaternion.Euler(0, 0, (Horizontal * -1) * 20);
        Dir = new(Horizontal, 0, Vertical);
        Dir.Normalize();
        Unit.transform.Translate(Dir * Unit.unitStates.MoveSpeed * Time.deltaTime, Space.World);
    }
}

public class EnemyMove : IMove
{
    public float Horizontal;
    public float Vertical;
    public Unit Unit;

    public EnemyMove(Unit unit)
    {
        this.Unit = unit;
    }
    public void Move()
    {
        //Unit.transform.rotation = Quaternion.Euler(0, Unit.CheckRotate(Player.Instance.transform.position), 0);
        Unit.transform.Translate(Vector3.forward * Unit.unitStates.MoveSpeed * Time.fixedDeltaTime);
    }
}


