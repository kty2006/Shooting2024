using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

//공격 유형에 따라 묶는게 더 좋을거 같음 ex)여러방향으로 나가는 공격 ,한방향으로 나가는 공격
//공격 유형을 인터페이스를 통해 묶어서 사용 ex)여러방향으로 나가는 공격,특수 공격,공격 공통 작업
public class NormalAttack : IAttack
{
    public Unit Unit;
    public bool IsCheck = true;
    public TimeAgent AttackTimeAgent;
    public float times;
    public int index;
    public virtual void Attack()
    {
        AttackTimeAgent = new(Unit.unitStates.SkillCoolTime[index], (timeAgent) => IsCheck = false, (timeAgent) => { }, (timeAgent) => IsCheck = true);
    }
}

public class PlayerNormalAttack : NormalAttack
{
    public PlayerNormalAttack(Unit unit, int times, int index)
    {
        this.Unit = unit;
        this.times = times;
        this.index = index;
    }
    public override void Attack()
    {
        base.Attack();
        Unit.NormalBulletPrefab.Speed = Unit.unitStates.AttackSpeed;
        Unit.NormalBulletPrefab.Power = Unit.unitStates.Power * times;
        if (Input.GetKey(KeyCode.X) && IsCheck)
        {
            TimerSystem.Instance.AddTimer(AttackTimeAgent);
            ObjectPool.Instance.Pooling(Unit.transform.position + new Vector3(0, 0, 30), Quaternion.Euler(0, 180, 0), Unit.NormalBulletPrefab.gameObject);
        }
    }
}

public class PlayerAssiantAttack : NormalAttack
{
    States states;
    GameObject[] AssianntGun;
    public PlayerAssiantAttack(Unit unit, int times, int index, GameObject[] AssianntGun)
    {
        this.Unit = unit;
        this.times = times;
        this.index = index;
        this.AssianntGun = AssianntGun;
        states = Unit.GetStates();
    }
    public override void Attack()
    {
        base.Attack();
        Unit.NormalBulletPrefab.Speed = Unit.unitStates.AttackSpeed;
        Unit.NormalBulletPrefab.Power = Unit.unitStates.Power * times;
        int sign = 1;
        int count = 0;
        int angleProduct = 0;
        if (Input.GetKey(KeyCode.X) && IsCheck)
        {
            TimerSystem.Instance.AddTimer(AttackTimeAgent);
            ObjectPool.Instance.Pooling(Unit.transform.position + new Vector3(0, 0, 30), Quaternion.Euler(0, 180, 0), Unit.NormalBulletPrefab.gameObject);
            for (int i = 1; i <= states.ItemLv; i++)
            {
                if (i == 3)
                {
                    ObjectPool.Instance.Pooling(Unit.transform.position + new Vector3(0, 0, 30), Quaternion.Euler(0, -1 * (170 - 5), 0), Unit.NormalBulletPrefab.gameObject);
                    angleProduct = -5;
                }

                ObjectPool.Instance.Pooling(Unit.transform.position + new Vector3(0, 0, 30), Quaternion.Euler(0, sign * 170 + angleProduct, 0), Unit.NormalBulletPrefab.gameObject);
                sign = sign switch
                {
                    1 => -1,
                    -1 => 1
                };
            }

            //for (int i = 0; i < states.ItemLv; i++)
            //{
            //    ObjectPool.Instance.Pooling(AssianntGun[i].transform.position + new Vector3(0, 0, 30), Quaternion.Euler(0, 180, 0), Unit.NormalBulletPrefab.gameObject);
            //}
        }
    }
}

public class PlayerBoomAttack : NormalAttack
{
    ParticleSystem Charging;
    States states;
    public PlayerBoomAttack(Unit unit, int index)
    {
        this.Unit = unit;
        this.index = index;
        states = Unit.GetStates();
        Charging = Unit.EffectData.CreateChargingEffect(unit.transform.position, Quaternion.identity);
    }

    public override void Attack()
    {
        base.Attack();
        Unit.NormalBulletPrefab.Speed = Unit.unitStates.AttackSpeed;
        Unit.NormalBulletPrefab.Power = Unit.unitStates.Power * times;
        if (IsCheck)
        {
            if (Input.GetKey(KeyCode.X))
            {
                times = Mathf.Clamp((times + Time.deltaTime * 30 * states.ItemLv), 1, 200);
                Unit.NormalBulletPrefab.Power = Unit.unitStates.Power * times;
                EffectPlay();
            }
            if (Input.GetKeyUp(KeyCode.X))
            {
                TimerSystem.Instance.AddTimer(AttackTimeAgent);
                ObjectPool.Instance.Pooling(Unit.transform.position + new Vector3(0, 0, 30), Quaternion.Euler(0, 180, 0), Unit.NormalBulletPrefab.gameObject);
                times = 0;
                EffectPlay();
            }
        }
    }
    public void EffectPlay()
    {
        if (times != 0)
        {
            if (!Charging.isPlaying)
                Charging.Play();
            Charging.transform.position = Unit.transform.position + new Vector3(0, 0, 31);
        }
        else if (times == 0)
        {
            Charging.Stop();
        }
    }
}

public class NormalEnemyAttack : NormalAttack
{
    public NormalEnemyAttack(Unit unit, int times, int index)
    {
        this.Unit = unit;
        this.times = times;
        this.index = index;
    }
    public override void Attack()
    {
        if (IsCheck)
        {
            base.Attack();
            Unit.NormalBulletPrefab.Speed = Unit.unitStates.AttackSpeed;
            Unit.NormalBulletPrefab.Power = Unit.unitStates.Power * times;
            TimerSystem.Instance.AddTimer(AttackTimeAgent);
            if (ObjectPool.Instance.Pooling(Unit.transform.position + new Vector3(0, 0, -30), Unit.transform.rotation, Unit.NormalBulletPrefab.gameObject).
                TryGetComponent(out Bullet bullet))
            { bullet.IsCheck = true; bullet.DirCheck(); }
        }
    }
}

public class LaserEnemyAttack : NormalAttack
{
    private GameObject laserObj;
    public LaserEnemyAttack(Unit unit, int times, int index)
    {
        this.Unit = unit;
        this.times = times;
        this.index = index;
        laserObj = Unit.EffectData.CreateLaserEffect(Unit.transform.position + new Vector3(0, 0, -20), Quaternion.Euler(0, 180, 0));
        laserObj.transform.parent = Unit.transform;
    }
    public override void Attack()
    {
        if (IsCheck && laserObj.TryGetComponent(out LaserObj laser) && laser.EnemyLaserCoroutine == null)
        {
            base.Attack();
            laser.Power = Unit.unitStates.Power * times * 0.07f;
            laser.EnemyLaserCoroutine = laser.StartCoroutine(laser.MonsterDuringLaser());
            //TimerSystem.Instance.AddTimer(AttackTimeAgent);
        }

    }
}

public class SectorEnemyShooter : NormalAttack
{
    public SectorEnemyShooter(Unit unit, int times, int index)
    {
        this.Unit = unit;
        this.times = times;
        this.index = index;
    }
    public override void Attack()
    {
        if (IsCheck)
        {
            base.Attack();
            Unit.NormalBulletPrefab.Speed = Unit.unitStates.AttackSpeed;
            Unit.NormalBulletPrefab.Power = Unit.unitStates.Power * times;
            TimerSystem.Instance.AddTimer(AttackTimeAgent);
            int sign = 1;
            int count = 0;
            int angleProduct = 1;
            ObjectPool.Instance.Pooling(Unit.transform.position + new Vector3(0, 0, -30), Quaternion.identity, Unit.NormalBulletPrefab.gameObject);
            for (int i = 0; i < 4; i++)
            {
                count += 1;
                if ((count + 1) % 2 == 0 && count != 1)
                {
                    angleProduct += 1;
                }
                ObjectPool.Instance.Pooling(Unit.transform.position + new Vector3(0, 0, -30), Quaternion.Euler(0, sign * 10 * angleProduct, 0), Unit.NormalBulletPrefab.gameObject);
                sign = sign switch
                {
                    1 => -1,
                    -1 => 1
                };
            }

        }
    }
}

public class DragonAttack : IAttack
{
    public Dragon dragon;
    public float currentTime;
    public float maxTime;
    public virtual void Attack()
    {
        if (dragon.DragonSkill == null)
        {
            currentTime = 0;
            dragon.DragonSkill = dragon.StartCoroutine(AttackDelay());
            dragon.ChangePattern(dragon.BossType.Pattern[dragon.BossType.PatternOrder]);
        }
    }
    public virtual IEnumerator AttackDelay()
    {
        yield return null;
    }
}

public class DragonFire : DragonAttack
{

    public DragonFire(Dragon dragon)
    {
        this.dragon = dragon;
        maxTime = dragon.unitStates.SkillCoolTime[0];
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override IEnumerator AttackDelay()
    {
        ParticleSystem FireEffect =
            dragon.EffectData.CreateFire(dragon.FirePos.transform.position, Quaternion.Euler(0, 180, 0));
        FireEffect.transform.parent = dragon.FirePos.transform;
        FireEffect.transform.localScale = new Vector3(25, 30, 20);
        if (FireEffect.TryGetComponent(out Fire fire))
            fire.Power = dragon.unitStates.Power * 0.2f;
        //FireEffect.transform.position = dragon.FirePos.transform.position;
        FireEffect.transform.rotation = dragon.FirePos.rotation;
        while (currentTime < maxTime)
        {
            dragon.Animator.SetBool("Fire", true);
            FireEffect.Play();
            if (dragon.Animator.GetCurrentAnimatorStateInfo(0).IsName("Vox_Dragon_Breath_F 0"))
            {
                if (dragon.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                {
                    dragon.Animator.SetBool("Fire", false);
                    yield return new WaitForSeconds(1);
                    FireEffect.Stop();
                    Debug.Log($"{currentTime}:{maxTime}");
                    currentTime += 1;
                }
            }
            yield return null;
        }
        dragon.DragonSkill = null;
    }
}

public class DragonRush : DragonAttack
{

    public DragonRush(Dragon dragon)
    {
        this.dragon = dragon;
        maxTime = dragon.unitStates.SkillCoolTime[1];
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override IEnumerator AttackDelay()
    {
        float time = 0;
        while (dragon.transform.position.y < (-683))
        {
            dragon.transform.position = Vector3.Lerp
                (dragon.transform.position, new Vector3(dragon.transform.position.x, -678.9f, dragon.transform.position.z), 0.02f);
            yield return null;
        }
        while (currentTime < maxTime)
        {
            time = 0;
            int count = Random.Range(1, dragon.RushPos.Count);
            dragon.InsWaring(dragon.RushPos[count]);
            while (time < 2)
            {
                dragon.transform.position = Vector3.Lerp(dragon.transform.position, dragon.RushPos[count].position, 0.05f);
                dragon.transform.rotation = Quaternion.Lerp(dragon.transform.rotation, dragon.RushPos[count].rotation, 0.05f);
                time += Time.deltaTime;
                yield return null;
            }
            dragon.Animator.SetBool("Rush", true);
            time = 0;
            yield return new WaitForSeconds(1);
            while (time < 3)
            {
                dragon.transform.Translate(Vector3.forward * Time.deltaTime * dragon.GetStates().AttackSpeed);
                time += Time.deltaTime;
                yield return null;
            }
            if (dragon.Animator.GetCurrentAnimatorStateInfo(0).IsName("Vox_Dragon_Rush 0"))
            {
                if (dragon.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                {
                    dragon.Animator.SetBool("Rush", false);
                    currentTime += 1;
                }
            }
        }
        time = 0;
        while (time < 2)
        {
            dragon.transform.position = Vector3.Lerp(dragon.transform.position, dragon.RushPos[0].position, 0.05f);
            dragon.transform.rotation = Quaternion.Lerp(dragon.transform.rotation, dragon.RushPos[0].rotation, 0.05f);
            time += Time.deltaTime;
            yield return null;
        }
        dragon.DragonSkill = null;
    }
}

public class DragonGust : DragonAttack
{

    public DragonGust(Dragon dragon)
    {
        this.dragon = dragon;
        maxTime = dragon.unitStates.SkillCoolTime[2];
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override IEnumerator AttackDelay()
    {
        while (currentTime < maxTime)
        {
            dragon.Animator.SetBool("Gust", true);
            dragon.EffectData.CreateWind(dragon.WindPos.position, dragon.WindPos.rotation);
            yield return new WaitForSeconds(4);
            if (dragon.Animator.GetCurrentAnimatorStateInfo(0).IsName("Vox_Dragon_Breath_Fw 0"))
            {
                if (dragon.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                {
                    dragon.Animator.SetBool("Gust", false);
                    currentTime += 1;
                }
            }
            yield return null;
        }
        dragon.DragonSkill = null;
    }
}

public class DragonTornado : DragonAttack
{

    public DragonTornado(Dragon dragon)
    {
        this.dragon = dragon;
        maxTime = dragon.unitStates.SkillCoolTime[3];
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override IEnumerator AttackDelay()
    {
        while (currentTime < maxTime)
        {
            dragon.Animator.SetBool("Tornado", true);
            CreateTornado();
            yield return new WaitForSeconds(4);
            if (dragon.Animator.GetCurrentAnimatorStateInfo(0).IsName("Vox_Dragon_Breath_Fw"))
            {
                if (dragon.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                {
                    dragon.Animator.SetBool("Tornado", false);
                    yield return new WaitForSeconds(1);
                    currentTime += 1;
                }
            }
            yield return null;
        }
        dragon.DragonSkill = null;
    }

    private void CreateTornado()
    {
        int sign = 1;
        ParticleSystem tornado = dragon.EffectData.CreateTornado(dragon.TornadoPos.position, Quaternion.Euler(-90, 0, 0));
        if (tornado.TryGetComponent(out Tornado tornado1))
            tornado1.Power = dragon.unitStates.Power * 0.2f;
        for (int i = 0; i < 2; i++)
        {
            tornado = dragon.EffectData.CreateTornado(dragon.TornadoPos.position, Quaternion.Euler(-90, sign * 80, 0));
            if (tornado.TryGetComponent(out Tornado tornado2))
                tornado2.Power = dragon.unitStates.Power * 0.2f;
            sign = sign switch
            {
                1 => -1,
                -1 => 1
            };
        }
    }
}


public class Boss2Attack : IAttack
{
    public Boss2 boss;
    public string aniName;
    public virtual void Attack()
    {
        boss.Animator.SetTrigger(aniName);
        boss.ChangePattern(boss.BossType.Pattern[boss.BossType.PatternOrder]);
    }
}

public class Boss2Patternk : Boss2Attack
{
    public Boss2Patternk(Boss2 boss, string aniName)
    {
        this.boss = boss;
        this.aniName = aniName;
        Debug.Log(aniName);
    }

    public override void Attack()
    {
        base.Attack();
    }
}