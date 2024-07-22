using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : Enemy
{
    public BossType BossType = new();
    public Animator Animator;
    public void Awake()
    {
        PatternAdd();
    }

    private void PatternAdd()
    {
        BossType.Pattern.Add(new Boss2Patternk(this, "Pattern"));
        BossType.Pattern.Add(new Boss2Patternk(this, "Pattern1"));
        BossType.Pattern.Add(new Boss2Patternk(this, "Pattern2"));
        ChangeType(BossType.Pattern[BossType.PatternOrder]);
    }

    public void ChangePattern(IAttack attack)
    {
        BossType.PatternOrder = (BossType.PatternOrder + 1 < BossType.Pattern.Count) ? BossType.PatternOrder += 1 : 0;
        ChangeType(BossType.Pattern[BossType.PatternOrder]);
    }

    public override void HpUI()
    {
        HpUIObj.Hp.text = $"HP {((int)unitStates.Hp)}";
        HpUIObj.HpFill.fillAmount = unitStates.Hp / unitStates.MaxHp;
    }

    public override void CountHp(float power)
    {
        unitStates.Hp -= power;
    }

    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
        if (collision.transform.TryGetComponent(out Player player))
        {
            //player.GetStates().Hp -= unitStates.Power;
            StartCoroutine(player.GodTime(Color.clear, 1));
            CameraShake.Instance.Shake(0.25f, 0.6f);
            UIManager.Instance.HitCheck();
        }
    }

    public override void Death()
    {
        if (unitStates.Hp <= 0)
        {
            Player.Instance.KillCountUp(this);
            GameManager.Instance.Stage2Clear = true;
            death = true;
            BossController.Instance.StopBossCoroutine = true;
            GameManager.Score += 100;
        }
    }
}
