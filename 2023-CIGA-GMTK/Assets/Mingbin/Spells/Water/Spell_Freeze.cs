using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Water/Freeze")]
public class Spell_Freeze : Spells
{
    public float fragilePercentage = 0.4f;

    public override void OnCastByMonster(Monster monster, bool castedByMonster = false)
    {
        base.OnCastByMonster(monster, castedByMonster);
        BattleManager.AddMonsterCastQueue(() =>
        {
            BattleManager.AddPlayerBuff(Buff.BuffInvincible(1));
            BattleManager.AddPlayerBuff(Buff.BuffSleep(1));
            BattleManager.AddMonsterBuff(monster, Buff.BuffFragile(2, fragilePercentage));
        });
    }

    public override void OnCastByPlayer()
    {
        base.OnCastByPlayer();
        BattleManager.AddPlayerCastQueue(() =>
        {
            BattleManager.AddSelectedMonsterBuff(Buff.BuffInvincible(1));
            BattleManager.AddSelectedMonsterBuff(Buff.BuffSleep(1));
            BattleManager.AddSelectedMonsterBuff(Buff.BuffFragile(2, fragilePercentage));
        });
    }
}
