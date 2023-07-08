using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Water/SnowStorm")]
public class Spell_SnowStorm : Spells
{
    public float damage = 12f;
    public float fragilePercentage = 0.3f;

    public override void OnCastByMonster(Monster monster, bool castedByMonster = false)
    {
        base.OnCastByMonster(monster, castedByMonster);
        BattleManager.AddMonsterCastQueue(() =>
        {
            BattleManager.AttackPlayer(monster, damage, elementType);
            BattleManager.AddPlayerBuff(Buff.BuffFragile(2, fragilePercentage));
        });
    }

    public override void OnCastByPlayer()
    {
        base.OnCastByPlayer();

        BattleManager.AddPlayerCastQueue(() =>
        {
            BattleManager.AttackAllMonster(damage, elementType);
            BattleManager.AddAllMonsterBuff(Buff.BuffFragile(2, fragilePercentage));
        });
    }
}
