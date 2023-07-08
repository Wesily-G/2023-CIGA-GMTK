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
                }else if(MonsterCost<=4){
                    SpellsCast(randomSpells(2,new string[2]{"FireBall","FireHealing"}),monster,true);
                }else if(MonsterCost<=6){
                    SpellsCast(randomSpells(2,new string[2]{"DoubleCast","FireWall"}),monster,true);
                }else if(MonsterCost<=8){
                    SpellsCast(randomSpells(1,new string[1]{"FireFist"}),monster,true);
                }else{
                    SpellsCast(randomSpells(1,new string[1]{"FireFist"}),monster,true);
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
                    SpellsCast(randomSpells(1,new string[1]{"SnowStorm"}),monster,true);
                }else{
                    SpellsCast(randomSpells(2,new string[2]{"SnowStorm","Siphon"}),monster,true);
                }
                break;
            case ElementTypes.Lighting:
                if(MonsterCost<=2){
                    SpellsCast(randomSpells(1,new string[1]{"LightningStruck"}),monster,true);
                }else if(MonsterCost<=4){
                    SpellsCast(randomSpells(1,new string[1]{"Lightning Saber"}),monster,true);
                }else if(MonsterCost<=6){
                    SpellsCast(randomSpells(2,new string[2]{"LightningCannon","LightningWrath"}),monster,true);
                }else if(MonsterCost<=8){
                    SpellsCast(randomSpells(2,new string[2]{"ThunderStorm","LightningWrath"}),monster,true);
                }else{
                    SpellsCast(randomSpells(2,new string[2]{"ThunderStorm","LightningWrath"}),monster,true);
                }
                break;

        }
    }
}
