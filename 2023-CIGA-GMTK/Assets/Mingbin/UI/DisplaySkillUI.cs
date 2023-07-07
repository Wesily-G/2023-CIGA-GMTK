using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySkillUI : MonoBehaviour
{
    private Image skillImage;
    private Text skillDes;
    private void Start()
    {
        skillImage = GameObject.Find("SkillImage").GetComponent<Image>();
        skillDes = GameObject.Find("TxSkill").GetComponent<Text>();
    }

    public void DisPlaySkill(string SkillName)
    {
        //显示技能详情
        Game.uiManager.ShowUI<DisplaySkillUI>("UIDisplaySkillUI");

        Spells skill = Resources.Load<Spells>("Resources/SkillData/" + SkillName);
        
        if(skill == null) Debug.LogError("寻找不到"+SkillName +"序列化脚本");

        if (skill.skillSprite != null) skillImage = skill.skillSprite;

        skillDes.text = skill.Name + "\n魔法量消耗:" + skill.cost +
                        "\n记忆力消耗:" + skill.memoryCost +
                        "\n法术容量占用\n" + skill.magicCost + skill.spellDescription;
        
    }
}
