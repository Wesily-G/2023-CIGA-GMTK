using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MonsterAction/FinalBoss")]
public class FinalBossMonsterAction : MonsterAction
{
    public override void Action(Monster monster)
    {
        int MonsterCost = BattleManager.GetRoundNumber()+ RoomManager.GetFloorNumber()-1;
        if(MonsterCost<=2){
            SpellsCast(randomSpells(2,new string[2]{"Siphon","LightningCannon"}),monster,true);
        }else if(MonsterCost<=4){
            SpellsCast(randomSpells(1,new string[1]{"AdvanceDoubleCasting"}),monster,true);
        }else if(MonsterCost<=6){
            SpellsCast(randomSpells(2,new string[2]{"SnowStorm","FireMinion"}),monster,true);
        }else if(MonsterCost<=8){
            SpellsCast(randomSpells(2,new string[2]{"FireFist","Judgement"}),monster,true);
        }else{
            SpellsCast(randomSpells(2,new string[2]{"LightningJudgement","PlasmaPool"}),monster,true);
        }

    }
}