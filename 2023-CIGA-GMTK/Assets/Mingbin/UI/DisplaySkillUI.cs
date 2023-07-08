using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySkillUI : MonoBehaviour
{
    private Image skillImage;
    private Text skillDes;
    private Button sureBtn;
    private Button closeBtn;
    private void Start()
    {
        //获取组件
        
        sureBtn = GameObject.Find("BtnSure").GetComponent<Button>();
        closeBtn = GameObject.Find("BtnClose").GetComponent<Button>();
        
        //添加
        sureBtn.onClick.AddListener(onsureBtnClick);
        closeBtn.onClick.AddListener(oncloseBtnClick);
    }

    public void DisPlaySkill(string SkillName)
    {
        sureBtn = GameObject.Find("BtnSure").GetComponent<Button>();
        closeBtn = GameObject.Find("BtnClose").GetComponent<Button>();
        sureBtn.onClick.AddListener(onsureBtnClick);
        closeBtn.onClick.AddListener(oncloseBtnClick);
        
        Spells skill = Resources.Load<Spells>("SkillData/" + SkillName);
        
        if(skill == null) Debug.LogError("寻找不到"+SkillName +"序列化脚本");
        skillImage = GameObject.Find("SkillImage").GetComponent<Image>();
        skillDes = GameObject.Find("TxSkill").GetComponent<Text>();
        if (skill.skillSprite != null)skillImage = skill.skillSprite;
        if(skillDes != null)
            skillDes.text = skill.Name + "\n魔法量消耗:" + skill.cost +
                        "\n记忆力消耗:" + skill.memoryCost +
                        "\n法术容量占用" + skill.magicCost + 
                        "\n" + skill.spellDescription;
        
    }

    private void onsureBtnClick()
    {
        //TODO:调用学习技能函数
    }

    private void oncloseBtnClick()
    {
        Debug.Log("关闭");
        Game.uiManager.CloseUI("DisplaySkillUI");
    }
}
