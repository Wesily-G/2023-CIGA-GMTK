using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Fire/FireWall")]
public class Spell_FireWall : Spells
{
    public int burnSustainability = 1;

    public override void OnCast(Monster monster, bool castedByMonster = false)
    {
        base.OnCast(monster, castedByMonster);

        if (!castedByMonster)
        {
            BattleManager.AddPlayerBuff(Buff.BuffResistance(ElementTypes.Fire, burnSustainability, 1));
            BattleManager.AddPlayerBuff(Buff.BuffResistance(ElementTypes.Lighting, burnSustainability, 1));
            BattleManager.AddPlayerBuff(Buff.BuffResistance(ElementTypes.Water, burnSustainability, 0.5f));
        }
        else
        {
            BattleManager.AddMonsterBuff(monster, Buff.BuffResistance(ElementTypes.Fire, burnSustainability, 1));
            BattleManager.AddMonsterBuff(monster, Buff.BuffResistance(ElementTypes.Lighting, burnSustainability, 1));
            BattleManager.AddMonsterBuff(monster, Buff.BuffResistance(ElementTypes.Water, burnSustainability, 0.5f));
        }
    }
}
