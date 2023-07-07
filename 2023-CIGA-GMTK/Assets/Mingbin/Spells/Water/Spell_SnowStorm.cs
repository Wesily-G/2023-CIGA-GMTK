using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Water/SnowStorm")]
public class Spell_SnowStorm : Spells
{
    public float damage = 12f;
    public float fragilePercentage = 0.3f;

    public override void OnCast(Monster monster, bool castedByMonster = false)
    {
        base.OnCast(monster, castedByMonster);
        if (!castedByMonster)
        {
            BattleManager.AddPlayerCastQueue(() =>
            {
                BattleManager.AttackAllMonster(damage, elementType);
                BattleManager.AddAllMonsterBuff(Buff.BuffFragile(2, fragilePercentage));
            });
        }
        else
        {
            BattleManager.AddMonsterCastQueue(() =>
            {
                BattleManager.AttackPlayer(monster, damage, elementType);
                BattleManager.AddPlayerBuff(Buff.BuffFragile(2, fragilePercentage));
            });
        }
    }
}
