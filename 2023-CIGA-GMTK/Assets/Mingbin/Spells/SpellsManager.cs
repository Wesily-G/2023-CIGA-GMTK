using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsManager : MonoBehaviour
{
    private static SpellsManager _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    //学习技能List
    public List<Spells> learnedSpells = new List<Spells>();

    //All Spells
    public List<Spells> allSpells = new List<Spells>();

    public static Spells GetSpell(string name)
    {
        var spell = Resources.Load<Spells>($"SkillData/{name}");
        return spell;
    }

    //TODO:添加学习法术接口

    public bool LearnSpell(string name)
    {
        Spells targetSpell;
        foreach (Spells spell in allSpells)
        {
            if (spell.name == name)
            {
                targetSpell = spell;
                if (!learnedSpells.Contains(spell))
                {
                    //If playerCost >= spell.cost
                    learnedSpells.Add(spell);
                    return true;
                }
            }
        }
        return false;
    }

    public bool SpellLearned(string name)
    {
        foreach (Spells spell in learnedSpells)
        {
            if (spell.name == name)
            {
                return true;
            }
        }

        return false;
    }

    public void RemoveSpell(string name)
    {
        foreach (Spells spell in learnedSpells)
        {
            if (spell.name == name)
            {
                learnedSpells.Remove(spell);
                return;
            }
        }
    }

    public void OnPlayerRoundStart()
    {
        //When Round Starts 
    }

    public void OnPlayerRoundEnd()
    {
        //When Round Ends
    }

    public static void Initialize()
    {
        //Init
    }
}
