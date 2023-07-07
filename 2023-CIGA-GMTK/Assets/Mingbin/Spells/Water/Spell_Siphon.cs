using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Water/Siphon")]
public class Spell_Siphon : Spells
{
    public float damage = 5f;

    public override void OnCast(Monster monster, bool castedByMonster = false)
    {
        base.OnCast(monster, castedByMonster);
        if (!castedByMonster)
        {
            BattleManager.AddPlayerVampire();
            BattleManager.AttackSelectedMonster(damage, elementType);
        }
        else
        {
            BattleManager.AddMonsterVampire(monster);
            BattleManager.AttackPlayer(monster, damage, elementType);
        }
    }
}
