using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Water/IceLance")]
public class Spell_IceLance : Spells
{
    public float damage = 1f;
    public float fragilePercentage = 0.1f;

    public override void OnCast(Monster monster, bool castedByMonster = false)
    {
        base.OnCast(monster, castedByMonster);
        if (!castedByMonster)
        {
            BattleManager.AttackSelectedMonster(damage, elementType);
            BattleManager.AddMonsterBuff(monster, Buff.BuffFragile(1, fragilePercentage));
            BattleManager.AddPlayerVampire();
        }
        else
        {
            BattleManager.AttackPlayer(monster,damage,elementType);
            BattleManager.AddPlayerBuff(Buff.BuffFragile(1, 0.1f));
        }
    }
}
