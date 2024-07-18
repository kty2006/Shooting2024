using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossType : MonoBehaviour
{
    public List<IAttack> Pattern = new();
    public int PatternOrder = 0;
}
