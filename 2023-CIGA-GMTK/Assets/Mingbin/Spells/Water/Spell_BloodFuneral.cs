using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Water/BloodFuneral")]
public class Spell_BloodFuneral : Spells
{
    public override void OnCastByMonster(Monster monster, bool castedByMonster = false)
    {
        base.OnCastByMonster(monster, castedByMonster);
        //Place Holder
    }

    public override void OnCastByPlayer()
    {
        base.OnCastByPlayer();
    }
}
