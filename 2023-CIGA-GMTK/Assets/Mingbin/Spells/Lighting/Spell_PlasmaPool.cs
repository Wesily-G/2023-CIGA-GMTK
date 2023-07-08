using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Water/PlasmaPool")]
public class Spell_PlasmaPool : Spells
{
    public float damage;
    public override void OnCastByMonster(Monster monster, bool castedByMonster = false)
    {
        base.OnCastByMonster(monster, castedByMonster);
        //Place Holder
        BattleManager.AttackPlayer(monster, damage, elementType);
        BattleManager.AddPlayerBuff(Buff.BuffParalysis(2));
        BattleManager.AddMonsterVampire(monster);
    }

    public override void OnCastByPlayer()
    {
        base.OnCastByPlayer();
        BattleManager.AttackSelectedMonster(damage, elementType);
        BattleManager.AddSelectedMonsterBuff(Buff.BuffParalysis(2));
        BattleManager.AddPlayerVampire();
    }
}
