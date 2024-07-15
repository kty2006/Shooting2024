using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    public void Visit(States element) { }
    public void Visit(NormalAttack element) { } //°­Á¦X
    public void Visit(Vector3 element) { }
    public void Visit(States element, Vector3 element2, Image HpUI) { }
}

public interface IVisitElement
{
    public void Accept(IVisit visit);
}

public interface ICommand
{
    public void Execute();
}
