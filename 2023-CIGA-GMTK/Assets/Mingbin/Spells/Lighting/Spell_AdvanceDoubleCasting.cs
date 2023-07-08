using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Lightning/AdvancedDoubleCasting")]
public class Spell_AdvanceDoubleCasting : Spells
{
    public override void OnCastByMonster(Monster monster, bool castedByMonster = false)
    {
        base.OnCastByMonster(monster, castedByMonster);

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

    public override void OnCastByPlayer()
    {
        base.OnCastByPlayer();

        BattleManager.AddPlayerCastQueue(() =>
        {
            BattleManager.AddPlayerBuff(Buff.BuffDoubleCast(-1));
            int rand = Random.Range(0, 100);
            if (rand <= 20)
            {
                BattleManager.AddSelectedMonsterBuff(Buff.BuffParalysis(1));
            }
        });
    }
}
