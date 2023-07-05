using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsManager : MonoBehaviour
{
    public List<Spells> learnedSpells = new List<Spells>();

    public void OnPlayerRoundStart()
    {
        //平A回到卡组
    }

    public void OnPlayerRoundEnd()
    {
        //预留
    }

    public void Initialize()
    {
        //初始化卡牌
    }
}
