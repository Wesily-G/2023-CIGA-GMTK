using System.Collections;
using System.Collections.Generic;
using GameplayTest.Scripts;
using UnityEngine;

public class TestCardB : Card
{
    private void Start()
    {
        describe = $"This is Test Card :{_testId}";
        _testId++;
        _initColor = GetComponent<SpriteRenderer>().color;
    }
    public override void OnUseCard()
    {
        BattleManager.AddCastQueue(() =>
        {
            BattleManager.AttackAllMonster(65,ElementTypes.Lighting);
            BattleManager.AddAllMonsterBuff(Buff.BuffFragile(2,1));
            BattleManager.AddPlayerBuff(Buff.BuffCriticalStrike(2,0.5f));
            BattleManager.AddPlayerBuff(Buff.BuffExplosiveInjury(2,0.5f));
        });
        base.OnUseCard();
    }
    private void LateUpdate()
    {
        if (isHighlight) Highlight();
        else GetComponent<SpriteRenderer>().color = _initColor;
        isHighlight = false;
    }
}