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
            CurrentCoolTime += Time.deltaTime;
        else
        {
            CurrentCoolTime -= Time.deltaTime;
            check = false;
        }
        if (!check && SkillFill.fillAmount <= 0)
        {
            check = true;
        }
        SkillFill.fillAmount = CurrentCoolTime / time;
        SkillPercent.text = $"{((CurrentCoolTime / time) * 100).ConvertTo<int>()}";
    }
}
