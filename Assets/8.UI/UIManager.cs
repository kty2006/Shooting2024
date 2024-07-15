using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerUI : IVisit
{
    public Text PlayerHp;
    public Image PlayerHpFill;

    public PlayerUI(out IVisit visit)
    {
        visit = this;
    }
    public void Visit(States element)
    {
        PlayerHp.text = $"HP {element.Hp}";
        PlayerHpFill.fillAmount = element.Hp / element.MaxHp;
    }
}

[Serializable]
public class EnemyUI : IVisit
{
    //public Text EnemyHp;
    //public Image EnemyHpFill;
    public EnemyUI(out IVisit visit)
    {
        visit = this;
        //UseEnemy1 = Instantiate();
    }
    public void Visit(States element, Vector3 element2, Image HpUI)
    {
        Debug.Log(element2);
        HpUI.transform.position = Camera.main.WorldToScreenPoint(element2);
        //HpUI.TryGetComponent(out Text EnemyHp); HpUI.TryGetComponent(out Image EnemyHpFill); HpUI.GetComponentInChildren<Text>();
        //EnemyHp.text = $"HP {element.Hp}";
        //EnemyHpFill.fillAmount = element.Hp / element.MaxHp;
    }
}
public class UIManager : MonoSingleTone<UIManager>
{
    public PlayerUI PlayerUI = new(out UICommand.PlayerHP);
    public EnemyUI EnemyUI = new(out UICommand.EnemyHP);
}
