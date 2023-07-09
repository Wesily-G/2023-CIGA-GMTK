using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplaySkillUI : MonoBehaviour
{
    public static Image SkillImage;
    public static Text skillText;
    public static Button sureBtn;
    public Button closeBtn;


    private void Start()
    {
        SkillImage = transform.Find("SkillImage").GetComponent<Image>();
        skillText = transform.Find("TxSkill").GetComponent<Text>();
        sureBtn = transform.Find("BtnSure").GetComponent<Button>();
        
        
        closeBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public static void DisplaySkill(Spells skill , bool isLearn = false)
    {
        sureBtn.onClick.RemoveAllListeners();
        
        if (skill.skillSprite != null) SkillImage.sprite = skill.skillSprite;
        skillText.text = skill.spellName + "\n魔法量消耗" + skill.cost
                         + "\n记忆力消耗" + skill.memoryCost
                         + "\n法术容量占用" + skill.magicCost + skill.spellDescription;

        if (isLearn) sureBtn.gameObject.SetActive(false);
        else sureBtn.gameObject.SetActive(true);
        
        sureBtn.onClick.AddListener(() =>
        {
            SpellsManager.GetInstance().LearnSpell(skill.spellName);
        });
    }
}
