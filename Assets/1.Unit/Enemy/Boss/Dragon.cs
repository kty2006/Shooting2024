using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
    public BossType BossType  = new();
    public List<Transform> RushPos;
    public Transform FirePos;
    public Transform WindPos;
    public Transform TornadoPos;
    public Animator Animator;
    public Coroutine DragonSkill;
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

    public void ChangePattern(IAttack attack)
    {
        BossType.PatternOrder = (BossType.PatternOrder + 1 < BossType.Pattern.Count) ? BossType.PatternOrder += 1 : 0;
        ChangeType(BossType.Pattern[BossType.PatternOrder]);
    }

    public void InsWaring(Transform pos)
    {
        Instantiate(WaringObj, pos.position, pos.rotation);
    }
}
