using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Water/Siphon")]
public class Spell_Siphon : Spells
{
    public float damage = 5f;

    public override void OnCastByMonster(Monster monster, bool castedByMonster = false)
    {
        base.OnCastByMonster(monster, castedByMonster);
        BattleManager.AddMonsterCastQueue(() =>
        {
            BattleManager.AddMonsterVampire(monster);
            BattleManager.AttackPlayer(monster, damage, elementType);
        });
    }

    public override void OnCastByPlayer()
    {
        base.OnCastByPlayer();
        BattleManager.AddPlayerCastQueue(() =>
        {
            BattleManager.AddPlayerVampire();
            BattleManager.AttackSelectedMonster(damage, elementType);
        });
    }
}
