using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Fire/FireHealing")]
public class Spell_FireHealing : Spells
{
    public float healAmount = 5f;

    public override void OnCast(Monster monster, bool castedByMonster = false)
    {
        base.OnCast(monster, castedByMonster);
        if (!castedByMonster)
        {
            BattleManager.HealPlayer(healAmount);
        }
        else
        {
            BattleManager.HealMonster(monster, healAmount);
        }
    }
}
