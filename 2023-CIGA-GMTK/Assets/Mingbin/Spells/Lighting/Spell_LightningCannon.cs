using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Lightning/LightningCannon")]
public class Spell_LightningCannon : Spells
{
    public float damage;
    public int paralysisConsistance = 1;
    public float criticalPercentage = 0.3f;

    public override void OnCast(Monster monster, bool castedByMonster = false)
    {
        base.OnCast(monster, castedByMonster);

        if (!castedByMonster)
        {
            BattleManager.AddPlayerCastQueue(() =>
            {
                BattleManager.AddMonsterBuff(monster, Buff.BuffParalysis(paralysisConsistance));
                BattleManager.AddPlayerBuff(Buff.BuffCriticalStrike(0, criticalPercentage));
                BattleManager.AttackSelectedMonster(damage, elementType);
            });
        }
        else
        {
            BattleManager.AddMonsterCastQueue(() =>
            {
                BattleManager.AddPlayerBuff(Buff.BuffParalysis(paralysisConsistance));
                BattleManager.AddMonsterBuff(monster, Buff.BuffCriticalStrike(0, criticalPercentage));
                BattleManager.AttackPlayer(monster, damage, elementType);
            });
        }
    }
}
