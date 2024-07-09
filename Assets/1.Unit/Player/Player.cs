using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public static Player Instance;

    public void Awake()
    {
        Instance = this;
    }
}
