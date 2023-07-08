using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Water/FireMinion")]
public class Spell_FireMinion : Spells
{
    public float explosionDamage = 40f;
    public override void OnCastByMonster(Monster monster, bool castedByMonster = false)
    {
        base.OnCastByMonster(monster, castedByMonster);
        //Summon minion
    }

    public override void OnCastByPlayer()
    {
        base.OnCastByPlayer();
        BattleManager.CreateAvatar(elementType, () =>
        {
            BattleManager.AttackAllMonster(explosionDamage, elementType);
            BattleManager.AddAllMonsterBuff(Buff.BuffBurn(2));
        });
    }
}
