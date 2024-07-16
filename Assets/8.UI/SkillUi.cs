using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillUi : MonoSingleTone<SkillUi>
{
    public Image SkillFill;
    public Text SkillPercent;
    public Text PressZ;
    public float CurrentCoolTime;
    private bool check = true;
    public void SkillUiFill(float time)
    {
        if (SkillFill.fillAmount < 1)
        {
            Debug.Log("올라감");
            CurrentCoolTime += Time.deltaTime;
        }
        SkillFill.fillAmount = CurrentCoolTime / time;
        SkillPercent.text = $"{((CurrentCoolTime / time) * 100).ConvertTo<int>()}";
    }

    public void UnSkillUiFill(float currentTime)
    {
        //if (SkillFill.fillAmount < 1)
        //{
        //    Debug.Log("올라감");
        //    CurrentCoolTime += Time.deltaTime;
        //}
        SkillFill.fillAmount = currentTime / 20;
        SkillPercent.text = $"{((currentTime / 20) * 100).ConvertTo<int>()}";
    }
}
