using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BossType 
{
    public List<IAttack> Pattern = new();
    public int PatternOrder = 0;
}
