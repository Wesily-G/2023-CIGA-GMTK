using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MonsterAction/Normal")]
public class NormalMonsterAction : MonsterAction
{
    public override void Action(Monster monster)
    {
        int MonsterCost = BattleManager.GetRoundNumber()+ RoomManager.GetFloorNumber()-1;
        switch(monster.elementTypes){
            case ElementTypes.Fire:
                if(MonsterCost<=2){
                    SpellsCast(randomSpells(1,new string[1]{"FireBall"}),monster,true);
                }
                break;
            case ElementTypes.Water:
                if(MonsterCost<=2){
                    SpellsCast(randomSpells(1,new string[1]{"IceLance"}),monster,true);
                }
                break;
            case ElementTypes.Lighting:
                if(MonsterCost<=2){
                    SpellsCast(randomSpells(1,new string[1]{"LightningStruck"}),monster,true);
                }
                break;

        }
    }
}
