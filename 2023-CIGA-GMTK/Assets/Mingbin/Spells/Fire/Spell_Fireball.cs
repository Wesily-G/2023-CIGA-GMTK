using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Fire/Fireball")]
public class Spell_Fireball : Spells
{
    public float damage = 5f;
    public GameObject fireballVFX;

    public override void OnCast(Monster monster, bool castedByMonster = false)
    {
        base.OnCast(monster);
        if (!castedByMonster) //Casted by player
        {
            InstantiateVFX(monster.transform.position);
            BattleManager.AttackSelectedMonster(damage, elementType);
        }
        else
        {
            InstantiateVFX(GameObject.FindGameObjectWithTag("Player").transform.position);
            BattleManager.AttackPlayer(monster, damage, elementType);
        }
    }

    private void InstantiateVFX(Vector2 targetPos)
    {
        //Placeholder
        GameObject vfx = Instantiate(fireballVFX, targetPos, Quaternion.identity);
    }
}
