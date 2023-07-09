using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public struct Node
{

}

public class SkillTreeManager : MonoBehaviour
{
    private static SkillTreeManager _instance;

    public GameObject skillInfoUI;
    public SpellNode[] spellNodes;
    
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public static SkillTreeManager GetInstance()
    {
        return _instance;
    }

    private void Start()
    {
        spellNodes = FindObjectsOfType<SpellNode>();
        //RefreshNodes();
    }

    public void DisplayInfoUI(Spells skill)
    {
        //Assign Info to UI
        skillInfoUI.SetActive(true);

        string description = skill.spellName + "\n魔法量消耗" + skill.cost
        + "\n记忆力消耗" + skill.memoryCost
        + "\n法术容量占用" + skill.magicCost + skill.spellDescription;

        Sprite sprite;
        if (skill.skillSprite != null)
            sprite = skill.skillSprite;

        skillInfoUI.transform.Find("SpellDescription").GetComponent<Text>().text = description;

        //Assign close UI event
        skillInfoUI.transform.Find("LearnBtn").GetComponent<Button>().onClick.RemoveAllListeners();
        skillInfoUI.transform.Find("CloseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            RefreshNodes();
            skillInfoUI.SetActive(false);
        });

        //Assign learn button event
        if (SpellsManager.GetInstance().SpellLearned(skill.name))
        {
            skillInfoUI.transform.Find("LearnBtn").gameObject.SetActive(false);
        }
        else
        {
            Button btnSure = skillInfoUI.transform.Find("LearnBtn").GetComponent<Button>();
            btnSure.gameObject.SetActive(true);
            btnSure.onClick.RemoveAllListeners();
            btnSure.onClick.AddListener(() =>
            {
                SpellsManager.GetInstance().LearnSpell(skill.name);
                RefreshNodes();
                skillInfoUI.SetActive(false);
            });      
        }
    }

    public void RefreshNodes()
    {
        for (int i = 0; i < spellNodes.Length; i++)
        {
            if (SpellsManager.GetInstance().SpellLearned(spellNodes[i].name))
            {
                //spell is learned
                spellNodes[i].GetComponent<Image>().color = Color.red;
            }
            else
            {
                //spel is not learned
                spellNodes[i].GetComponent<Image>().color = Color.white;
            }
        }
    }
}
