using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Water/Freeze")]
public class Spell_FreezeSelf : Spells
{
    public float fragilePercentage = 0.4f;

    public override void OnCast(Monster monster, bool castedByMonster = false)
    {
        base.OnCast(monster, castedByMonster);
        if (!castedByMonster)
        {
            BattleManager.AddPlayerCastQueue(() =>
            {
                BattleManager.AddPlayerBuff(Buff.BuffInvincible(1));
                BattleManager.AddPlayerBuff(Buff.BuffSleep(1));
            });
        }
        else
        {
            BattleManager.AddMonsterCastQueue(() =>
            {
                BattleManager.AddMonsterBuff(monster, Buff.BuffInvincible(1));
                BattleManager.AddMonsterBuff(monster, Buff.BuffSleep(1));
            });
        }
    }
}
