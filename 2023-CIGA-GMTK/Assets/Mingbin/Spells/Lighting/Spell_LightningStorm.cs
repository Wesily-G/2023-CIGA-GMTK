using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Lightning/LightningStorm")]
public class Spell_LightningStorm : Spells
{
    public float damage = 28f;
    public override void OnCastByMonster(Monster monster, bool castedByMonster = false)
    {
        base.OnCastByMonster(monster, castedByMonster);

        BattleManager.AddMonsterCastQueue(() =>
        {
            BattleManager.AttackPlayer(monster, damage, elementType);
            BattleManager.AddPlayerBuff(Buff.BuffParalysis(1));
        });
    }

    public override void OnCastByPlayer()
    {
        base.OnCastByPlayer();
        BattleManager.AddPlayerCastQueue(() =>
        {
            BattleManager.AttackAllMonster(damage, elementType);
            BattleManager.AddAllMonsterBuff(Buff.BuffParalysis(1));
        });
    }
}
