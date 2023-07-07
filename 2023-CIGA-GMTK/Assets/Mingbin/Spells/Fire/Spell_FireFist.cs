using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Fire/FireFist")]
public class Spell_FireFist : Spells
{
    public float damage = 20;
    public int burnSustainability = 3;

    public override void OnCast(Monster monster, bool castedByMonster = false)
    {
        base.OnCast(monster, castedByMonster);
        if (!castedByMonster)
        {
            BattleManager.AttackSelectedMonster(damage, elementType);
            BattleManager.AddMonsterBuff(monster, Buff.BuffBurn(burnSustainability));
            BattleManager.InterruptMonster(monster);
        }
        else
        {
            BattleManager.AttackPlayer(monster, damage, elementType);
            BattleManager.AddPlayerBuff(Buff.BuffBurn(burnSustainability));
            BattleManager.InterruptPlayer();
        }
    }
}
