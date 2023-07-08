using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillTreeNode : MonoBehaviour
{
    public Spells skill;
    public Transform displaySkillUI;
    private Button displayButton;
    
    private void Start()
    {
        displayButton = gameObject.GetComponent<Button>();
        displayButton.onClick.AddListener(()=>OnSkillButtonClick(skill));
    }

    
    private void OnSkillButtonClick(Spells skill)
    {
        if (displaySkillUI.gameObject.activeSelf == false)
            displaySkillUI.gameObject.SetActive(true);
        
        //判断是否学习
        bool isLearn = SpellsManager.GetInstance().SpellLearned(skill.Name);
        
        DisplaySkillUI.DisplaySkill(skill,isLearn);
    }
}
