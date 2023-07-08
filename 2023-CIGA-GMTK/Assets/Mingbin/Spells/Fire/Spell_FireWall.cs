using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Fire/FireWall")]
public class Spell_FireWall : Spells
{
    public int burnSustainability = 1;

    public override void OnCastByMonster(Monster monster, bool castedByMonster = false)
    {
        base.OnCastByMonster(monster, castedByMonster);

        BattleManager.AddMonsterCastQueue(() =>
        {
            BattleManager.AddMonsterBuff(monster, Buff.BuffResistance(ElementTypes.Fire, burnSustainability, 1));
            BattleManager.AddMonsterBuff(monster, Buff.BuffResistance(ElementTypes.Lighting, burnSustainability, 1));
            BattleManager.AddMonsterBuff(monster, Buff.BuffResistance(ElementTypes.Water, burnSustainability, 0.5f));
        });
    }

    public override void OnCastByPlayer()
    {
        base.OnCastByPlayer();
        BattleManager.AddPlayerCastQueue(() =>
        {
            BattleManager.AddPlayerBuff(Buff.BuffResistance(ElementTypes.Fire, burnSustainability, 1));
            BattleManager.AddPlayerBuff(Buff.BuffResistance(ElementTypes.Lighting, burnSustainability, 1));
            BattleManager.AddPlayerBuff(Buff.BuffResistance(ElementTypes.Water, burnSustainability, 0.5f));
        });
    }
}
