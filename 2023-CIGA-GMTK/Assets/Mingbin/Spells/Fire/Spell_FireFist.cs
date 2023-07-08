using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Fire/FireFist")]
public class Spell_FireFist : Spells
{
    public float damage = 20;
    public int burnSustainability = 3;

    public override void OnCastByMonster(Monster monster, bool castedByMonster = false)
    {
        base.OnCastByMonster(monster, castedByMonster);
        BattleManager.AddMonsterCastQueue(() =>
        {
            BattleManager.AttackPlayer(monster, damage, elementType);
            BattleManager.AddPlayerBuff(Buff.BuffBurn(burnSustainability));
            BattleManager.InterruptPlayer();
        });
    }

    public override void OnCastByPlayer()
    {
        base.OnCastByPlayer();
        BattleManager.AddPlayerCastQueue(() =>
        {
            BattleManager.AttackSelectedMonster(damage, elementType);
            BattleManager.AddSelectedMonsterBuff( Buff.BuffBurn(burnSustainability));
            BattleManager.InterruptSelectedMonster();
        });
    }
}
