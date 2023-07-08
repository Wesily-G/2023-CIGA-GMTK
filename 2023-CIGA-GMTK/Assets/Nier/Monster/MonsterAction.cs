using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAction : ScriptableObject
{
    public ElementTypes type;
    public virtual void Action(Monster monster)
    {
        
    }

    public string randomSpells(int number,string[] spellsName){
        int randomNumber = Random.Range(0,number);
        return spellsName[randomNumber];
    }

    public static void SpellsCast(string name,Monster monster,bool castedByMonster = false){
        Spells spell = SpellsManager.GetSpell(name);
        spell.OnCast(monster,castedByMonster);
    }
}
