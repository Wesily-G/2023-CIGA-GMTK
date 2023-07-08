using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Lightning/LightningWrath")]
public class Spell_LightningWrath : Spells
{
    public float criticalPercentage = 0.5f;
    public override void OnCastByMonster(Monster monster, bool castedByMonster = false)
    {
        base.OnCastByMonster(monster, castedByMonster);

        BattleManager.AddMonsterCastQueue(() =>
        {
            BattleManager.AddMonsterBuff(monster, Buff.BuffCriticalStrike(2, criticalPercentage));
        });
    }

    public override void OnCastByPlayer()
    {
        base.OnCastByPlayer();

        BattleManager.AddPlayerCastQueue(() =>
        {
            BattleManager.AddPlayerBuff(Buff.BuffCriticalStrike(2, 0.5f));
        });
    }
}
