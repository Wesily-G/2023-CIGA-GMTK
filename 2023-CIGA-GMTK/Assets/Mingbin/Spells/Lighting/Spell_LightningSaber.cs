using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Lightning/LightningSaber")]
public class Spell_LightningSaber : Spells
{
    public int paralysisConsistance = 3;
    public override void OnCastByMonster(Monster monster, bool castedByMonster = false)
    {
        base.OnCastByMonster(monster, castedByMonster);
        BattleManager.AddMonsterCastQueue(() =>
        {
            BattleManager.AddPlayerBuff(Buff.BuffParalysis(paralysisConsistance));
        });
    }

    public override void OnCastByPlayer()
    {
        base.OnCastByPlayer();

        BattleManager.AddPlayerCastQueue(() =>
        {
            BattleManager.AddSelectedMonsterBuff(Buff.BuffParalysis(paralysisConsistance));
        });
    }
}
