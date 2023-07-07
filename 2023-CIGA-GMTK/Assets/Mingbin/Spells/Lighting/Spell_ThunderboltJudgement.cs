using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Lightning/ThunderboltJudgement")]
public class Spell_ThunderboltJudgement : Spells
{
    public float damage = 65;
    public float fragilePercentage = 1f;
    public float criticalPercentage = 0.5f;
    public float explosiveInjuryPercentage = 0.5f;

    public override void OnCast(Monster monster, bool castedByMonster = false)
    {
        base.OnCast(monster, castedByMonster);

        if (!castedByMonster)
        {
            BattleManager.AddPlayerCastQueue(() =>
            {
                BattleManager.AttackAllMonster(damage, elementType);
                BattleManager.AddAllMonsterBuff(Buff.BuffFragile(2, fragilePercentage));
                BattleManager.AddPlayerBuff(Buff.BuffCriticalStrike(2, criticalPercentage));
                BattleManager.AddPlayerBuff(Buff.BuffExplosiveInjury(2, explosiveInjuryPercentage));
            });
        }
        else
        {
            BattleManager.AddMonsterCastQueue(() =>
            {
                BattleManager.AttackPlayer(monster, damage, elementType);
                BattleManager.AddPlayerBuff(Buff.BuffFragile(2, fragilePercentage));
                BattleManager.AddMonsterBuff(monster, Buff.BuffCriticalStrike(2, criticalPercentage));
                BattleManager.AddMonsterBuff(monster, Buff.BuffExplosiveInjury(2, explosiveInjuryPercentage));
            });
        }
    }
}
