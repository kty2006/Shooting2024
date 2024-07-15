using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICommand : MonoBehaviour
{
    public static IVisit PlayerHP;
    public static IVisit EnemyHP;
}

public class EffectCommand : MonoBehaviour
{
    public static IVisit PlayerBoom;
    public static IVisit PlayerBoomHit;
}
