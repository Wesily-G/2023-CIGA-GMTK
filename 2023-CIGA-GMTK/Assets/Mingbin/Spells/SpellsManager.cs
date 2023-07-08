using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsManager : MonoBehaviour
{
    //学习技能List
    public List<Spells> learnedSpells = new List<Spells>();

    //All Spells
    public static List<Spells> allSpells = new List<Spells>();

    public static Spells getSpell(string name){
        // switch(name){
        //     case "":
        //         break;
        //     default:
        //         return null;
        // }

        foreach (Spells spell in allSpells)
        {
            if (spell.name == name) return spell;
        }

        return null;
        
    }

    //TODO:添加学习法术接口
    public void OnPlayerRoundStart()
    {
        //When Round Starts 
    }

    public void OnPlayerRoundEnd()
    {
        //When Round Ends
    }

    public void Initialize()
    {
        //Init
    }
}
