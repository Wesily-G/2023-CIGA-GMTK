using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Spells preSkill;
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
    public Sprite skillSprite;
    
    //学习次数
    public int learnNum;
    [TextArea(1 , 8)]
    public string spellDescription = "";
    
    public List<PreSkill> prevSpellsOnTree = new List<PreSkill>();

    public virtual void OnAdding()
    {
        //Call when adding spell to list
    }

    public virtual void OnRemoved()
    {
        //Call when removing spell from list
    }

    public virtual void OnCast(Monster monster, bool castedByMonster = false)
    {
        //Call when casting spell to monster in a fight
    }
}
