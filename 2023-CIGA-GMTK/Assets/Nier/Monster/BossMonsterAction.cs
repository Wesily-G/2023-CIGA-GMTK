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
                }else if(MonsterCost<=4){
                    SpellsCast(randomSpells(2,new string[2]{"FireWall","FireFist"}),monster,true);
                }else if(MonsterCost<=6){
                    SpellsCast(randomSpells(2,new string[2]{"FireMinion","AdvanceDoubleCast"}),monster,true);
                }else if(MonsterCost<=8){
                    SpellsCast(randomSpells(2,new string[2]{"SnowStorm","FireFist"}),monster,true);
                }else{
                    SpellsCast(randomSpells(1,new string[1]{"Judgement"}),monster,true);
                }
                break;
            case ElementTypes.Water:
                if(MonsterCost<=2){
                    SpellsCast(randomSpells(2,new string[2]{"IceLance","Siphon"}),monster,true);
                }else if(MonsterCost<=4){
                    SpellsCast(randomSpells(2,new string[2]{"Freeze","WaterMinion"}),monster,true);
                }else if(MonsterCost<=6){
                    SpellsCast(randomSpells(2,new string[2]{"SnowStorm","Siphon"}),monster,true);
                }else if(MonsterCost<=8){
                    SpellsCast(randomSpells(1,new string[1]{"BloodFuneral"}),monster,true);
                }else{
                    SpellsCast(randomSpells(2,new string[2]{"BloodFuneral","Freeze"}),monster,true);
                }
                break;
            case ElementTypes.Lighting:
                if(MonsterCost<=2){
                    SpellsCast(randomSpells(2,new string[2]{"LightningStruck","LightningSaber"}),monster,true);
                }else if(MonsterCost<=4){
                    SpellsCast(randomSpells(1,new string[1]{"LightningCannon"}),monster,true);
                }else if(MonsterCost<=6){
                    SpellsCast(randomSpells(2,new string[2]{"ThunderStorm","AdvanceDoubleCast"}),monster,true);
                }else if(MonsterCost<=8){
                    SpellsCast(randomSpells(2,new string[2]{"ThunderStorm","LightningWrath"}),monster,true);
                }else{
                    SpellsCast(randomSpells(1,new string[1]{"LightningJudgement"}),monster,true);
                }
                break;

        }
    }
}