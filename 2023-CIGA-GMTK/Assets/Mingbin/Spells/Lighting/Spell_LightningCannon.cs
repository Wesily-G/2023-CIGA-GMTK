using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Lightning/LightningCannon")]
public class Spell_LightningCannon : Spells
{
    public float damage;
    public int paralysisConsistance = 1;
    public float criticalPercentage = 0.3f;

    public override void OnCastByMonster(Monster monster, bool castedByMonster = false)
    {
        base.OnCastByMonster(monster, castedByMonster);

        BattleManager.AddMonsterCastQueue(() =>
        {
            BattleManager.AddPlayerBuff(Buff.BuffParalysis(paralysisConsistance));
            BattleManager.AddMonsterBuff(monster, Buff.BuffCriticalStrike(0, criticalPercentage));
            BattleManager.AttackPlayer(monster, damage, elementType);
        });
    }

    public override void OnCastByPlayer()
    {
        base.OnCastByPlayer();
        BattleManager.AddPlayerCastQueue(() =>
        {
            BattleManager.AddSelectedMonsterBuff(Buff.BuffParalysis(paralysisConsistance));
            BattleManager.AddPlayerBuff(Buff.BuffCriticalStrike(0, criticalPercentage));
            BattleManager.AttackSelectedMonster(damage, elementType);
        });
    }
}
