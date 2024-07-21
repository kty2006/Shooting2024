using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitSet : MonoSingleTone<SitSet>
{
    public Action OnEnd = null;
    public float timer;
    public List<EventBusSet> Event = new();
    private int Index = 0;
    bool boundary;
    public void PlaySitSet(Action OnEnd)
    {
        this.OnEnd = OnEnd;
        ActionEvent();
    }

    public void ActionEvent()
    {
        Event[Index].PlayEventSet(() =>
        {
            if (EndCheckEventData())
                EndEventData();
            else
                ActionEvent();
        });
        //boundary = true;
        //while (boundary)
        //{
        //    Event[Index].PlayEventSet(() =>
        //    {
        //        if (EndCheckEventData())
        //            EndEventData();
        //    });
        //}
    }

    private bool EndCheckEventData()
    {
        return ++Index >= Event.Count;
    }

    private void EndEventData()
    {
        OnEnd?.Invoke();
        Index = 0;
        boundary = false;
    }
}
