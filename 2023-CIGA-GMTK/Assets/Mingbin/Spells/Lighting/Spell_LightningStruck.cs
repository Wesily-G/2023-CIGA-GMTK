using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Lightning/LightningStruck")]
public class Spell_LightningStruck : Spells
{
    public float damage;
    public float criticalPercentage = 0.5f;

    public override void OnCast(Monster monster, bool castedByMonster = false)
    {
        base.OnCast(monster, castedByMonster);

        //Calculation for critical damage


        if (!castedByMonster)
        {
            BattleManager.AddPlayerCastQueue(() =>
            {
                BattleManager.AddPlayerBuff(Buff.BuffCriticalStrike(0, criticalPercentage));
                BattleManager.AttackSelectedMonster(damage, elementType);
            });
        }
        else
        {
            BattleManager.AddMonsterCastQueue(() =>
            {
                BattleManager.AddMonsterBuff(monster, Buff.BuffCriticalStrike(0, criticalPercentage));
                BattleManager.AttackPlayer(monster, damage, elementType);
            });
        }
    }
}
