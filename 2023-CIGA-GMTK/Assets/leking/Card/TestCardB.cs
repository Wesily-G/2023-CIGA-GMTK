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
            BattleManager.AttackSelectedMonster(65,ElementTypes.Lighting);
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


    // public void OnPlayCast()
    // {
    //     BattleManager.AddPlayerBuff(Buff.BuffInvincible(1));
    //     BattleManager.AddPlayerBuff(Buff.BuffSleep(1));
    // }
    // public void OnMonsterCast(Monster monster)
    // {
    //     BattleManager.AddMonsterBuff(monster,Buff.BuffInvincible(1));
    //     BattleManager.AddMonsterBuff(monster,Buff.BuffSleep(1));
    // }
    
    public void OnPlayCast()
    {
        BattleManager.AddSelectedMonsterBuff(Buff.BuffInvincible(1));
        BattleManager.AddSelectedMonsterBuff(Buff.BuffSleep(1));
        BattleManager.AddSelectedMonsterBuff(Buff.BuffFragile(2,0.4f));
        
    }
    public void OnMonsterCast(Monster monster)
    {
        BattleManager.AddPlayerBuff(Buff.BuffInvincible(1));
        BattleManager.AddPlayerBuff(Buff.BuffSleep(1));
        BattleManager.AddPlayerBuff(Buff.BuffFragile(2,0.4f));
    }
}
