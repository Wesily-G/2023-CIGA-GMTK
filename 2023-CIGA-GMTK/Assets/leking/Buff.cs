using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    Burn,
    Fragile,
    FireResistance,
    WaterResistance,
    LightingResistance,
    DelaySpell,
    Invincible
}
public class Buff
{
    public BuffType type;
    public int time;
    public int priority;
    public float percentage;
    public Action onBuffEnd;
    public Buff(BuffType type, int time, int priority)
    {
        this.type = type;
        this.time = time;
        this.priority = priority;
    }
    public Buff(BuffType type, int time, int priority,float percentage)
    {
        this.type = type;
        this.time = time;
        this.priority = priority;
        this.percentage = percentage;
    }
    public Buff(BuffType type, int time, int priority,Action onBuffEnd)
    {
        this.type = type;
        this.time = time;
        this.priority = priority;
        this.onBuffEnd = onBuffEnd;
    }
    public static Buff BuffBurn(int time)
    {
        return new Buff(BuffType.Burn, time, 1);
    }
    public static Buff BuffFragile(int time,float percentage)
    {
        if (percentage < 0) percentage = 0;
        else if (percentage > 1) percentage = 1;
        return new Buff(BuffType.Fragile, time, 0,percentage);
    }
    public static Buff BuffResistance(ElementTypes type,int time, float percentage)
    {
        if (percentage < 0) percentage = 0;
        else if (percentage > 1) percentage = 1;
        switch (type)
        {
            case ElementTypes.Fire:
                return new Buff(BuffType.FireResistance, time, 1,percentage);
            case ElementTypes.Water:
                return new Buff(BuffType.WaterResistance, time, 1,percentage);
            case ElementTypes.Lighting:
                return new Buff(BuffType.LightingResistance, time, 1,percentage);
        }
        return new Buff(BuffType.FireResistance, time, 0,percentage);;
    }
    public static Buff BuffDelaySpell(int time, Action spell)
    {
        return new Buff(BuffType.DelaySpell, time, 0,spell);
    }
    public static Buff BuffInvincible(int time)
    {
        return new Buff(BuffType.Invincible, 1, 0);
    }
}
