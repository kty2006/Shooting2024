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

public interface Boss
{
    BossType BossType { get; set; }
}

//플레이어,적에 관한 인터페이스가 적음 플레이어 혹은 적을 가져와 사용해야 하는경우 인터페이스 혹은 추상화를 제대로 하지 않았기 때문에 구현에 직접적으로 연결하기 때문에 의존성이 높아진다.
//예를 들어 IAttack에서 공격을 구현할떄 unit혹은 dragon등을 가져와서 구현을 하는데 직접적으로 클래스를 가져와서 구현하기 때문에 boss,player등으로 직접적으로 구현되어있지않은 인터페이스를
//만들고 필요한 기능들을 함수화해 사용하는것이 객체지향원칙을 지키는 방법인거 같다.

public interface IVisit
{
    public void Visit(States element) { }
    public void Visit(NormalAttack element) { } //강제X
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
