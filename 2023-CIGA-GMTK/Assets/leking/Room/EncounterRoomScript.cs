using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterRoomScript : MonoBehaviour
{
    public void HealingHalf(){
        ChangePlayerHpPercentage((float)0.5);
    }

    public void CapacityImprove(int count){
        SpellsManager.GetInstance().magicCostLimit+=count;
    }

    public void RecoverMagic(int addMagicAmount){
        int _magicAmount = SpellsManager.GetMagicAmount();
        _magicAmount+=60;
        SpellsManager.addMagicAmount(addMagicAmount);
    }

    public void FixedLowSpell(){
        List<Spells> learnedSpells =  SpellsManager.GetInstance().learnedSpells;
        List<Spells> learnedLowSpells = new List<Spells>();

        foreach(Spells spell in learnedSpells){
            if(spell.magicCost==1)
                learnedLowSpells.Add(spell);
        }

        SpellsManager.GetInstance().AddFixSpell(RandomSpells(learnedLowSpells));

        ChangePlayerHpPercentage((float)-0.2);
    }

    public void FixedMediumSpell(){
        List<Spells> learnedSpells =  SpellsManager.GetInstance().learnedSpells;
        List<Spells> learnedMediumSpells = new List<Spells>();

        foreach(Spells spell in learnedSpells){
            if(spell.magicCost==2)
                learnedMediumSpells.Add(spell);
        }

        SpellsManager.GetInstance().AddFixSpell(RandomSpells(learnedMediumSpells));

        ChangePlayerHpPercentage((float)-0.5);
    }

    public void ChangePlayerHpPercentage(float ratio){
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        float hp = player.GetHp();
        hp += ratio*player.maxHp;
        player.Hp = hp;
    }

    public Spells RandomSpells(List<Spells> spells){
        int randomNumber = Random.Range(0,spells.Count);
        return spells[randomNumber];
    }


}
