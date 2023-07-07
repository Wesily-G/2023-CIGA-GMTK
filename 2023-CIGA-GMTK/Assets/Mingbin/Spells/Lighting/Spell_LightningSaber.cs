using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Lightning/LightningSaber")]
public class Spell_LightningSaber : Spells
{
    public int paralysisConsistance = 3;
    public override void OnCast(Monster monster, bool castedByMonster = false)
    {
        base.OnCast(monster, castedByMonster);
        if (!castedByMonster)
        {
            BattleManager.AddPlayerCastQueue(() =>
            {
                BattleManager.AddMonsterBuff(monster, Buff.BuffParalysis(paralysisConsistance));
            });
        }
        else
        {
            BattleManager.AddMonsterCastQueue(() => 
            {
                BattleManager.AddPlayerBuff(Buff.BuffParalysis(paralysisConsistance));
            });
        }
    }
}
