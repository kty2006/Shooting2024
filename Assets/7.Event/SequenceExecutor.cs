using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceExecutor : MonoBehaviour
{
    private int currentSequenceIndex = 0;
    public SitSet[] startPlaySequences;

    public IEnumerator PlaySequence(Action OnEnd = null)
    {
        for (int i = 0; i < startPlaySequences.Length; i++)
        {
            startPlaySequences[i].PlaySitSet(() =>
            {
                if (EndCheckSequence())
                    OnEnd?.Invoke();
            });
            yield return new WaitForSeconds(startPlaySequences[i].timer);
        }
        yield return null;
    }

    private bool EndCheckSequence()
    {
        return ++currentSequenceIndex >= startPlaySequences.Length;
    }
}
