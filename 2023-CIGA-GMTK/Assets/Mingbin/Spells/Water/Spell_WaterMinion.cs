using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Water/WaterMinion")]
public class Spell_WaterMinion : Spells
{
    public override void OnCastByMonster(Monster monster, bool castedByMonster = false)
    {
        base.OnCastByMonster(monster, castedByMonster);
        //Place Holder
    }

    public override void OnCastByPlayer()
    {
        base.OnCastByPlayer();
        BattleManager.CreateAvatar(elementType, () =>
        {
            BattleManager.AddAllMonsterBuff(Buff.BuffFragile(2, 0.3f));
        });
    }
}
