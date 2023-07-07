using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Lightning/LightningCannon")]
public class Spell_LightningWrath : Spells
{
    public override void OnCast(Monster monster, bool castedByMonster = false)
    {
        base.OnCast(monster, castedByMonster);

        if (!castedByMonster)
        {
            BattleManager.AddPlayerBuff(Buff.BuffCriticalStrike(2, 0.5f));
        }
        else
        {
            BattleManager.AddMonsterBuff(monster, Buff.BuffCriticalStrike(2, 0.5f));
        }
    }
}