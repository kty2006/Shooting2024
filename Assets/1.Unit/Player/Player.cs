using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;
using UnityEngine;
[Serializable]
public class UpgradeMesh
{
    public GameObject[] filter;
}
public class KillCount
{
    public int Enemy1, Enemy2, Enemy3;
}
public class Player : Unit
{
    public SequenceExecutor PlayerDie;
    public ParticleSystem Upgrade;
    public static Player Instance;
    public MeshRenderer[] Renderer;
    public GameObject Bomb;
    public KillCount killCount = new();
    public GameObject[] AssiantGuns;
    public Vector3 MaxLimit, MinLimit;
    public IEnumerator godTime;
    public bool CheatGod = false;
    public void Awake()
    {
        Instance = this;
        UIHp = UICommand.PlayerHP;
    }

    protected override void Update()
    {
        base.Update();
        Limit();
        //Accept(UIHp);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube((MaxLimit + MinLimit) / 2, MaxLimit - MinLimit);
    }
#endif
    public override void LvUp()
    {
        base.LvUp();
        if (GetStates().Lv == 13)
        {
            foreach (var render in Renderer)
            {
                render.materials[0].color = Color.yellow;
            }
        }
        else if (GetStates().Lv == 16)
        {
            foreach (var render in Renderer)
            {
                render.materials[0].color = Color.blue;
            }
        }
        else if (GetStates().Lv == 19)
        {
            foreach (var render in Renderer)
            {
                render.materials[0].color = Color.clear;
            }
        }
    }
    public void ExpUp(int exp)
    {
        unitStates.Exp += exp;
        if (unitStates.Exp >= unitStates.Lv * 10)
        {
            unitStates.Lv += 1;
            unitStates.Exp = 0;
            LvUp();

        }
    }

    public void KillCountUp(Unit enemy)
    {
        var states = enemy.GetStates();
        switch (enemy.gameObject.layer)
        {
            case 7:
                killCount.Enemy1 += 1;
                break;
            case 9:
                killCount.Enemy2 += 1;
                break;
            case 10:
                killCount.Enemy3 += 1;
                break;
        }
        ExpUp(states.Exp);
    }

    public override void Death()
    {
        if (unitStates.Hp <= 0)
        {
            //gameObject.SetActive(false);
            death = true;
            Collider[] AllObj = FindObjectsOfType<Collider>();
            foreach (var obj in AllObj)
            {
                Destroy(obj.gameObject);    
            }
            StartCoroutine(PlayerDie.PlaySequence(() => {}));
        }
    }

    public void Limit()
    {
        float x = Mathf.Clamp(transform.position.x, MinLimit.x, MaxLimit.x); ;
        float z = Mathf.Clamp(transform.position.z, MinLimit.z, MaxLimit.z); ;
        transform.position = new Vector3(x, transform.position.y, z);
    }

    public override void HitEffectPlay()
    {

    }

    public IEnumerator GodTime(Color color, float time)
    {
        float currentTime = 0;
        Debug.Log($"시작{time} : {currentTime}");
        foreach (var render in Renderer)
        {
            if (render.TryGetComponent(out MeshCollider collider))
            {
                collider.enabled = false;
            }
            else
            {
                Debug.Log("오류");
            }
        }
        yield return null;
        while (currentTime < time)
        {
            //foreach (var render in Renderer)
            //{
            //    render.materials[0].color = color;
            //    yield return null;
            //    Debug.Log("진행중");
            //    render.materials[0].color = Color.white;
            //}
            currentTime += Time.deltaTime;
            yield return null;
        }
        foreach (var render in Renderer)
        {
            //if (GetStates().Lv >= 13 && GetStates().Lv < 16)
            //{
            //    render.materials[0].color = Color.yellow;
            //}
            //else if (GetStates().Lv < 19 && GetStates().Lv >= 16)
            //{
            //    render.materials[0].color = Color.blue;
            //}
            //else if (GetStates().Lv >= 19)
            //{
            //    render.materials[0].color = Color.clear;
            //}
            if (render.TryGetComponent(out MeshCollider collider))
            {
                collider.enabled = true;
            }
            else
            {
                Debug.Log("오류");
            }
        }
        Debug.Log("무적 끝");
        yield return null;
        //TimeAgent godTime = new(time, (timeAgent) => { collider.enabled = false; }, (timeAgent) => {  },
        //    (timeAgent) => { mesh.material.color = Color.white; collider.enabled = true; });
        //TimerSystem.Instance.AddTimer(godTime);
    }

    //public void 

    public override void HitAction()
    {
    }

    public void ChangeWeapon(IAttack attack)
    {
        //IAttack Cannon = new PlayerBoomAttack(Player.Instance, 1);
        if (CurrentWeapon.ToString() != attack.ToString())
        {
            Debug.Log($"{attack.ToString()}:{CurrentWeapon.ToString()} 레벨업실패");
            Player.Instance.ChangeType(attack);
            unitStates.ItemLv = 1;
        }
        else
        {
            Debug.Log("레벨업성공");
            unitStates.ItemLv = Mathf.Clamp(unitStates.ItemLv + 1, 1, 3);
        }

    }

    public void OnParticleTrigger()
    {
        Debug.Log(gameObject.name);
    }


    public void CheatGodkey()
    {
        foreach (var render in Renderer)
        {
            if (render.TryGetComponent(out MeshCollider collider))
            {
                if (!CheatGod)
                    collider.enabled = false;
                else if (CheatGod)
                    if (!CheatGod)
                        collider.enabled = true;
            }
        }
        if (!CheatGod)
            CheatGod = true;
        else if (CheatGod)
            CheatGod = false;
    }
}
