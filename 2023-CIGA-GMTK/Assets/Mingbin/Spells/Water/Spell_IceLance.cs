using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Water/IceLance")]
public class Spell_IceLance : Spells
{
    public float damage = 1f;
    public float fragilePercentage = 0.1f;

    public override void OnCastByMonster(Monster monster, bool castedByMonster = false)
    {
        base.OnCastByMonster(monster, castedByMonster);
        BattleManager.AddMonsterCastQueue(() =>
        {
            BattleManager.AttackPlayer(monster, damage, elementType);
            BattleManager.AddPlayerBuff(Buff.BuffFragile(1, 0.1f));
        });
    }

    public override void OnCastByPlayer()
    {
        base.OnCastByPlayer();

        BattleManager.AddPlayerCastQueue(() =>
        {
            BattleManager.AddPlayerVampire();
            BattleManager.AttackSelectedMonster(damage, elementType);
            BattleManager.AddSelectedMonsterBuff(Buff.BuffFragile(1, fragilePercentage));
        });
    }
}
