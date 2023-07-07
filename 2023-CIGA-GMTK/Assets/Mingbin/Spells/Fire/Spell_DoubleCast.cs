using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Fire/DoubleCast")]
public class Spell_DoubleCast : Spells
{
    public override void OnCast(Monster monster, bool castedByMonster = false)
    {
        base.OnCast(monster, castedByMonster);
        if (!castedByMonster)
        {
            BattleManager.AddPlayerCastQueue(() =>
            {
                BattleManager.AddPlayerBuff(Buff.BuffDoubleCast(2));
            });
        }
        else
        {
            BattleManager.AddMonsterCastQueue(() =>
            {
                BattleManager.AddMonsterBuff(monster, Buff.BuffDoubleCast(2));
            }); 
        }
    }
}
