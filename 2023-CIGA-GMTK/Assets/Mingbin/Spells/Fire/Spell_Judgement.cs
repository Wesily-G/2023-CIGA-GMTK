using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Fire/Judgement")]
public class Spell_Judgement : Spells
{
    public float damage = 99999f;

    public override void OnCastByMonster(Monster monster, bool castedByMonster = false)
    {
        base.OnCastByMonster(monster, castedByMonster);
        BattleManager.AddMonsterCastQueue(() =>
        {
            BattleManager.AttackPlayer(monster, damage, elementType);
        });
    }

    public override void OnCastByPlayer()
    {
        base.OnCastByPlayer();
        BattleManager.AddPlayerCastQueue(() =>
        {
            BattleManager.AttackSelectedMonster(damage, elementType);
        });
    }
}
