using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MonsterAction/Boss")]
public class BossMonsterAction : MonsterAction
{
    public override void Action(Monster monster)
    {
        int MonsterCost = BattleManager.GetRoundNumber()+ RoomManager.GetFloorNumber()-1;
        switch(monster.elementTypes){
            case ElementTypes.Fire:
                if(MonsterCost<=2){
                    SpellsCast(randomSpells(2,new string[2]{"DoubleCast","FireHealing"}),monster,true);
                }
                break;
            case ElementTypes.Water:
                if(MonsterCost<=2){
                    SpellsCast(randomSpells(2,new string[2]{"IceLance","siphon"}),monster,true);
                }
                break;
            case ElementTypes.Lighting:
                if(MonsterCost<=2){
                    SpellsCast(randomSpells(2,new string[2]{"LightningStruck","LightningSaber"}),monster,true);
                }
                break;

        }
    }
}