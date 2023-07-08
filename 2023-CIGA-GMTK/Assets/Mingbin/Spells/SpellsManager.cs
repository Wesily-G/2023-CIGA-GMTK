using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsManager : MonoBehaviour
{
    public static SpellsManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    //学习技能List
    public List<Spells> learnedSpells = new List<Spells>();

    //All Spells
    public List<Spells> allSpells = new List<Spells>();

    public Spells getSpell(string name){
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

    public static void Initialize()
    {
        //Init
    }
}
