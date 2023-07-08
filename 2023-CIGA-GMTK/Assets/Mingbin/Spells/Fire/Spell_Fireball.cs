using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Fire/Fireball")]
public class Spell_Fireball : Spells
{
    public float damage = 5f;
    public GameObject fireballVFX;

    public override void OnCastByMonster(Monster monster, bool castedByMonster = false)
    {
        base.OnCastByMonster(monster);
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
