using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Lightning/AdvancedDoubleCasting")]
public class Spell_AdvanceDoubleCasting : Spells
{
    public override void OnCast(Monster monster, bool castedByMonster = false)
    {
        base.OnCast(monster, castedByMonster);

        if (!castedByMonster)
        {
            BattleManager.AddPlayerCastQueue(() =>
            {
                BattleManager.AddPlayerBuff(Buff.BuffDoubleCast(-1));
                int rand = Random.Range(0, 100);
                if (rand <= 20)
                {
                    BattleManager.AddMonsterBuff(monster, Buff.BuffParalysis(1));
                }
            });
        }
        else
        {
            BattleManager.AddMonsterCastQueue(() => 
            {
                BattleManager.AddMonsterBuff(monster, Buff.BuffDoubleCast(-1));

                int rand = Random.Range(0, 100);
                if (rand <= 20)
                {
                    BattleManager.AddPlayerBuff(Buff.BuffParalysis(1));
                }
            });

        }
    }
}
