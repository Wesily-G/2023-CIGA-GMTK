using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementTypes //List of Elements
{
    Fire,
    Water,
    Lighting
}
public class Spells : ScriptableObject //Base class of all spells
{
    public ElementTypes elementType = ElementTypes.Fire;
    [Tooltip("Cost in Battle")]
    public int cost = 1;
    [Tooltip("Cost of Memory Slot")]
    public int memoryCost = 0;
    public string spellDescription = "";

    public List<Spells> prevSpellsOnTree = new List<Spells>();
    public List<Spells> nextSpellsOnTree = new List<Spells>();

    public virtual void OnAdding()
    {
        //Call when adding spell to list
    }

    public virtual void OnRemoved()
    {
        //Call when removing spell from list
    }

    public virtual void OnCast(Monster monster, bool castedByMonster = false)
    {
        //Call when casting spell to monster in a fight
    }
}
