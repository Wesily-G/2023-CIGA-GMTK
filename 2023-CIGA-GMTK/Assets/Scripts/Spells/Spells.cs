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
    public int cost = 1;
    public int costDeduction = 1;
    public string spellDescription = "";

    public virtual void OnAdding()
    {
        //Call when adding spell to list
    }

    public virtual void OnRemoved()
    {
        //Call when removing spell from list
    }

    public virtual void OnCast()
    {
        //Call when casting spell in a fight
    }
}
