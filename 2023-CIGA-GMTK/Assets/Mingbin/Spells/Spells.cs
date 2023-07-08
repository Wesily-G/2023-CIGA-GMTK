using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ElementTypes //List of Elements
{
    Fire,
    Water,
    Lighting
}

[Serializable]
public struct PreSkill
{
    public int decmemoryCost; //学习前驱后的记忆力减少量
    public Spells prev;
}

public class Spells : ScriptableObject //Base class of all spells
{
    public ElementTypes elementType = ElementTypes.Fire;
    public string Name;
    [Tooltip("Cost in Battle")]
    public int cost = 1;  //魔法量消耗
    
    [Tooltip("Cost of Memory Slot")]
    public int memoryCost = 0;  //记忆力消耗
    public int magicCost = 0; //法术占用量
    public int finalMemoryCost = 0;

    [Header("Spell Special Effects")]
    public bool isFixed = false; // Fixed spells will return to hand

    public Sprite skillSprite;
    public Sprite cardSprite;
    
    //学习次数
    public int learnNum;
    [TextArea(1 , 8)]
    public string spellDescription = "";
    
    public List<PreSkill> preSkills = new List<PreSkill>();

    public virtual void OnAdding()
    {
        finalMemoryCost = memoryCost;
        //Call when adding spell to list
        if (preSkills.Count > 0)
        {
            foreach (PreSkill preskill in preSkills)
            {
                Spells spell = preskill.prev;
                if (SpellsManager.GetInstance().learnedSpells.Contains(preskill.prev))
                {
                    spell.finalMemoryCost -= preskill.decmemoryCost;
                }
            }
        }
    }

    public virtual void OnRemoved()
    {
        //Call when removing spell from list
    }

    public virtual void OnCastByMonster(Monster monster, bool castedByMonster = true)
    {
        //Call when casting spell by monster
    }

    public virtual void OnCastByPlayer()
    {
        //Call when casting spell to monster in a fight
    }
}
