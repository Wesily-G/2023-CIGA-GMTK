using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillTreeUI : MonoBehaviour
{
    private Button closeBtn;
    private GameObject skillBtns;
    private void Start()
    {
        //获取组件
        closeBtn = GameObject.Find("BtnClose").GetComponent<Button>();
        skillBtns = GameObject.Find("SkillBtns");
        Debug.Log(skillBtns.name);
        //添加事件
        for (int i = 0; i < skillBtns.transform.childCount; i++)
        {
            Button skillBtn = skillBtns.transform.GetChild(i).GetComponent<Button>();
            //skillBtn空引用
            if(skillBtn == null) Debug.LogError("空引用");
            skillBtn.onClick.AddListener(()=>onskillBtnClick(skillBtn.name));
        }

        closeBtn.onClick.AddListener(oncloseBtnClick);
    }

    private void oncloseBtnClick()
    {
        Game.uiManager.CloseUI("SkillTreeUI");
    }

    private void onskillBtnClick(string skillName)
    {
        
        DisplaySkillUI displaySkillUI = Game.uiManager.ShowUI<DisplaySkillUI>("DisplaySkillUI");
        Debug.Log("点击技能");
        displaySkillUI.DisPlaySkill(skillName);
    }
}
