using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy, Boss
{
    public BossType BossType { get; set; } = new();
    public List<Transform> RushPos;
    public Animator Animator;
    public Coroutine DragonSkill;
    public Transform FirePos;
    public GameObject WaringObj;
    public void Awake()
    {
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

    public override void ChangeType(IAttack attack)
    {
        base.ChangeType(attack);
        BossType.PatternOrder = (BossType.PatternOrder + 1 < BossType.Pattern.Count) ? BossType.PatternOrder += 1 : 0;
    }

    public void InsWaring(Transform pos)
    {
        Instantiate(WaringObj,pos.position,pos.rotation);
    }
}
