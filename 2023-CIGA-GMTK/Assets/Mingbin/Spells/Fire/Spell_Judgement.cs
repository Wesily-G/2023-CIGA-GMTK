using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Fire/Judgement")]
public class Spell_Judgement : Spells
{
    public float damage = 99999f;

    public override void OnCast(Monster monster, bool castedByMonster = false)
    {
        base.OnCast(monster, castedByMonster);
        if (!castedByMonster)
        {
            BattleManager.AddPlayerCastQueue(() =>
            {
                BattleManager.AttackSelectedMonster(damage, elementType);
            });
        }
        else
        {
            BattleManager.AddMonsterCastQueue(() =>
            {
                BattleManager.AttackPlayer(monster, damage, elementType);
            });
        }
    }
}
