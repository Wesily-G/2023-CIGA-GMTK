using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Lightning/LightningStorm")]
public class Spell_LightningStorm : Spells
{
    public float damage = 28f;
    public override void OnCast(Monster monster, bool castedByMonster = false)
    {
        base.OnCast(monster, castedByMonster);

        if (!castedByMonster)
        {
            BattleManager.AddPlayerCastQueue(() =>
            {
                BattleManager.AttackAllMonster(damage, elementType);
                BattleManager.AddAllMonsterBuff(Buff.BuffParalysis(1));
            });

        }
        else
        {
            BattleManager.AddMonsterCastQueue(() =>
            {
                BattleManager.AttackPlayer(monster, damage, elementType);
                BattleManager.AddPlayerBuff(Buff.BuffParalysis(1));
            });
        }
    }
}
