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
    public Button GameRankButton;
    public Button ExitButton;
    public Text Score;
    public Image ProgressUI;
    public Image HitPanel;
    public Text Lv;
    public Text ItemLv;


    public Image Panel;
    public Text CurrentExp;
    public Text CurrentLv;
    public Text LeftExp;
    public void Start()
    {
        PlayButton.onClick.AddListener(() => StartCoroutine(executor.PlaySequence(() => { GameManager.Instance.GameStartSequence(); })));
        RankButton.onClick.AddListener(() => GameManager.SceneSave.Scene(1));
        GameRankButton.onClick.AddListener(() => GameManager.SceneSave.Scene(1));
        ExitButton.onClick.AddListener(() => { Application.Quit(); });
    }

    public void Update()
    {
        ProgressUI.fillAmount = GameManager.Instance.Progress / 80;
        Score.text = $"{GameManager.Score}";
        Lv.text = $"{Player.Instance.GetStates().Lv}";
        ItemLv.text = $"ItemLv : {Player.Instance.GetStates().ItemLv}";
        CurrentExp.text = $"현재경험치 : {Player.Instance.GetStates().Exp}";
        CurrentLv.text = $"현재레벨 : {Player.Instance.GetStates().Lv}";
        LeftExp.text = $"다음레벨까지 : {(Player.Instance.GetStates().Lv * 10) - Player.Instance.GetStates().Exp}";
    }

    public void HitCheck()
    {
        if (HitPanel.TryGetComponent(out Animator ani))
        {
            ani.SetTrigger("Hit");
        }
    }
}
