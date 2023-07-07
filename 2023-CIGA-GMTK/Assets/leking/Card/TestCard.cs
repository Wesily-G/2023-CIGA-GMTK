using System;
using System.Collections;
using System.Collections.Generic;
using GameplayTest.Scripts;
using UnityEngine;

public class TestCard : Card
{
    private void Start()
    {
        describe = $"This is Test Card :{_testId}";
        _testId++;
        _initColor = GetComponent<SpriteRenderer>().color;
    }
    public override void OnUseCard()
    {
        //硬编码的技能，实际应该动态获取
        var newCardObject = KGameObject.Instantiate(gameObject);
        newCardObject.transform.localScale = Vector3.one * 2.5f;
        newCardObject.transform.position = Vector3.zero;
        var newCard = newCardObject.GetComponent<Card>();
        newCard.gameObject.SetActive(false);
        Action spellSet = ()=>
        {
            BattleManager.AttackSelectedMonster(5, ElementTypes.Fire);
            BattleManager.AddFirstCommand(() =>
            {
                //在这里添加技能添加的卡
                newCard.gameObject.SetActive(true);
                print($"Get : newCard.name");
                CardManager.AddCard(newCard);
            });
        };
        BattleManager.AddPlayerCastQueue(spellSet);
        base.OnUseCard();
    }
    private void LateUpdate()
    {
        if (isHighlight) Highlight();
        else GetComponent<SpriteRenderer>().color = _initColor;
        isHighlight = false;
    }
}
