using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class EventBusSet : MonoBehaviour
{
    public UnityEvent[] ActionSet;

    public void PlayEventSet(Action LastAction)
    {
        int count = 0;
        while (count < ActionSet.Length)
        {
            ActionSet[count]?.Invoke();
            count++;
        }
        LastAction?.Invoke();
    }
}
