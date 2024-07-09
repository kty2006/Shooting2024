using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IMove
{
    public void Move();
}

public interface IAttack
{
    public void Attack();
}

public interface ISkill
{
    public void Skill();
}

public interface IVisit
{
    public void Visit(IVisitElement element);
}

public interface IVisitElement
{
    public void Accept(IVisit visit);
}