using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Water/FreezeSelf")]
public class Spell_FreezeSelf : Spells
{
    public float fragilePercentage = 0.4f;

    public override void OnCastByMonster(Monster monster, bool castedByMonster = false)
    {
        base.OnCastByMonster(monster, castedByMonster);
        BattleManager.AddMonsterCastQueue(() =>
        {
            BattleManager.AddMonsterBuff(monster, Buff.BuffInvincible(1));
            BattleManager.AddMonsterBuff(monster, Buff.BuffSleep(1));
        });
    }

    public override void OnCastByPlayer()
    {
        base.OnCastByPlayer();

        BattleManager.AddPlayerCastQueue(() =>
        {
            BattleManager.AddPlayerBuff(Buff.BuffInvincible(1));
            BattleManager.AddPlayerBuff(Buff.BuffSleep(1));
        });
    }
}
