using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class UIManager : MonoSingleTone<UIManager>
{
    public SequenceExecutor executor;
    public Button PlayButton;
    public Button RankButton;
    public Text Score;
    public Image ProgressUI;
    public Image HitPanel;
    public void Start()
    {
        PlayButton.onClick.AddListener(() => StartCoroutine(executor.PlaySequence(() => { GameManager.Instance.GameStartSequence(); })));
        RankButton.onClick.AddListener(() => SceneManager.LoadScene(1));
    }

    public void Update()
    {
        ProgressUI.fillAmount = GameManager.Instance.Progress / 120;
        Score.text = $"{GameManager.Score}";
    }

    public void HitCheck()
    {
        if (HitPanel.TryGetComponent(out Animator ani))
        {
            ani.SetTrigger("Hit");
        }
    }
}
