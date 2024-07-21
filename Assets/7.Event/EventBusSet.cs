using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using Unity.VisualScripting;

public class EventBusSet : MonoBehaviour
{
    public SequenceDataEnd sequenceDataEndSend;
    public UnityEvent[] ActionSet;
    private void Awake()
    {
        sequenceDataEndSend = GetComponent<SequenceDataEnd>();
    }
    public void PlayEventSet(Action LastAction)
    {
        foreach (var sequence in ActionSet)
            sequence?.Invoke();
        sequenceDataEndSend.SendEnd(LastAction);
    }
}
