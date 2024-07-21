using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SequenceDataEnd : MonoBehaviour
{
    public float timer;

    public void SendEnd(Action OnEnd)
    {
        StartCoroutine(StartEndTimer(OnEnd));
    }

    private IEnumerator StartEndTimer(Action OnEnd)
    {
        yield return new WaitForSeconds(timer);
        OnEnd?.Invoke();
    }
}
