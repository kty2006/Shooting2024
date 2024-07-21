using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UIManager : MonoSingleTone<UIManager>
{
    public SequenceExecutor executor;
    public Button PlayButton;
    public Image ProgressUI;
    public Image HitPanel;
    public void Start()
    {
        PlayButton.onClick.AddListener(() => StartCoroutine(executor.PlaySequence(() => { GameManager.Instance.GameStartSequence(); })));
    }

    public void Update()
    {
        ProgressUI.fillAmount = GameManager.Instance.Progress / 120;
    }

    public void HitCheck()
    {
        if (HitPanel.TryGetComponent(out Animator ani))
        {
            ani.SetTrigger("Hit");
        }
    }
}
