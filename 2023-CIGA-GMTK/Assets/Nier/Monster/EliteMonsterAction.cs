using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MonsterAction/Elite")]
public class EliteMonsterAction : MonsterAction
{
    public override void Action(Monster monster)
    {
        int MonsterCost = BattleManager.GetRoundNumber()+ RoomManager.GetFloorNumber()-1;
        switch(monster.elementTypes){
            case ElementTypes.Fire:
                if(MonsterCost<=2){
                    SpellsCast(randomSpells(2,new string[2]{"FireBall","FireHealing"}),monster,true);
                }else if(MonsterCost<=4){
                    SpellsCast(randomSpells(2,new string[2]{"FireWall","DoubleCast"}),monster,true);
                }else if(MonsterCost<=6){
                    SpellsCast(randomSpells(2,new string[2]{"FireHealing","FireFist"}),monster,true);
                }else if(MonsterCost<=8){
                    SpellsCast(randomSpells(2,new string[2]{"FireWall","FireFist"}),monster,true);
                }else{
                    SpellsCast(randomSpells(3,new string[3]{"AdvanceDoubleCast","FireWall","FireFist"}),monster,true);
                }
                break;
            case ElementTypes.Water:
                if(MonsterCost<=2){
                    SpellsCast(randomSpells(1,new string[1]{"IceLance"}),monster,true);
                }else if(MonsterCost<=4){
                    SpellsCast(randomSpells(2,new string[2]{"Siphon","IceLance"}),monster,true);
                }else if(MonsterCost<=6){
                    SpellsCast(randomSpells(2,new string[2]{"Freeze","FreezeSelf"}),monster,true);
                }else if(MonsterCost<=8){
                    SpellsCast(randomSpells(2,new string[2]{"SnowStorm","Siphon"}),monster,true);
                }else{
                    SpellsCast(randomSpells(3,new string[3]{"PlasmaPool","Siphon","SnowStorm"}),monster,true);
                }
                break;
            case ElementTypes.Lighting:
                if(MonsterCost<=2){
                    SpellsCast(randomSpells(2,new string[2]{"LightningStruck","LightningSaber"}),monster,true);
                }else if(MonsterCost<=4){
                    SpellsCast(randomSpells(1,new string[1]{"LightningCannon"}),monster,true);
                }else if(MonsterCost<=6){
                    SpellsCast(randomSpells(2,new string[2]{"ThunderStorm","LightningWrath"}),monster,true);
                }else if(MonsterCost<=8){
                    SpellsCast(randomSpells(2,new string[2]{"ThunderStorm","LightningWrath"}),monster,true);
                }else{
                    SpellsCast(randomSpells(3,new string[3]{"PlasmaPool","LightningWrath","AdvanceDoubleCasting"}),monster,true);
                }
                break;

        }
    }
}
