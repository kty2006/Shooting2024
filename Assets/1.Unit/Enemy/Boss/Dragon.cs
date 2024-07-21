using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
    public BossType BossType = new();
    public List<Transform> RushPos;
    public Transform FirePos;
    public Transform WindPos;
    public Transform TornadoPos;
    public Animator Animator;
    public Coroutine DragonSkill;
    public GameObject WaringObj;
    public static States onlyUnitStates; //직접적으로 스텟 변수를 접근 하지 못하게 함수를 사용한 접근 변수를 null로 한다던가 접근 못함
    public void Awake()
    {
        onlyUnitStates = unitStates;
        PatternAdd();
    }

    private void PatternAdd()
    {
        BossType.Pattern.Add(new DragonFire(this));
        BossType.Pattern.Add(new DragonRush(this));
        BossType.Pattern.Add(new DragonGust(this));
        BossType.Pattern.Add(new DragonTornado(this));
        ChangeType(BossType.Pattern[BossType.PatternOrder]);
    }

    public void ChangePattern(IAttack attack)
    {
        BossType.PatternOrder = (BossType.PatternOrder + 1 < BossType.Pattern.Count) ? BossType.PatternOrder += 1 : 0;
        ChangeType(BossType.Pattern[BossType.PatternOrder]);
    }

    public override void HpUI()
    {
        HpUIObj.Hp.text = $"HP {((int)onlyUnitStates.Hp)}";
        HpUIObj.HpFill.fillAmount = onlyUnitStates.Hp / onlyUnitStates.MaxHp;
    }

    public override void CountHp(float power)
    {
        onlyUnitStates.Hp -= power;
    }

    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
        if (collision.transform.TryGetComponent(out Player player))
        {
            player.GetStates().Hp -= onlyUnitStates.Power;
            StartCoroutine(player.GodTime(Color.clear, 1));
            CameraShake.Instance.Shake(0.25f, 0.6f);
            UIManager.Instance.HitCheck();
        }
    }

    public override void Death()
    {
        if (onlyUnitStates.Hp <= 0)
        {
            Player.Instance.KillCountUp(this);
            GameManager.Instance.GameReStart = true;
        }
    }
    public void InsWaring(Transform pos)
    {
        Instantiate(WaringObj, pos.position, pos.rotation);
    }
}
