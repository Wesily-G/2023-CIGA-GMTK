using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    //普通Buff
    Burn,//灼烧
    Fragile,//脆弱
    FireResistance,//火焰抗性
    WaterResistance,//水抗性
    LightingResistance,//雷抗性
    Paralysis,//麻痹
    IncreasedInjury,//伤害增加
    Invincible,//无敌
    CriticalStrike,//暴击率
    ExplosiveInjury,//暴击伤害加成
    DoubleCast,//双重施法

    //隐藏/全局Buff
    DelaySpell,//延迟施法
    Sleep,//睡眠
}
public class Buff
{
    public BuffType type;
    public int time;
    public int priority;
    public float percentage;
    public bool isGlobal; 
    public Action onBuffEnd = () => {};
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
        return new Buff(BuffType.Burn, time, 0);
    }
    public static Buff BuffFragile(int time,float percentage)
    {
        if (percentage < 0) percentage = 0;
        else if (percentage > 1) percentage = 1;
        return new Buff(BuffType.Fragile, time, 8,percentage);
    }
    public static Buff BuffIncreasedInjury(int time,float percentage)
    {
        if (percentage < 0) percentage = 0;
        return new Buff(BuffType.IncreasedInjury, time, 8,percentage);
    }
    public static Buff BuffResistance(ElementTypes type,int time, float percentage)
    {
        if (percentage < 0) percentage = 0;
        else if (percentage > 1) percentage = 1;
        switch (type)
        {
            case ElementTypes.Fire:
                return new Buff(BuffType.FireResistance, time, 9,percentage);
            case ElementTypes.Water:
                return new Buff(BuffType.WaterResistance, time, 9,percentage);
            case ElementTypes.Lighting:
                return new Buff(BuffType.LightingResistance, time, 9,percentage);
        }
        return new Buff(BuffType.FireResistance, time, 0,percentage);
    }
    public static Buff BuffDelaySpell(int time, Action spell)
    {
        return new Buff(BuffType.DelaySpell, time, 10,spell);
    }
    public static Buff BuffInvincible(int time)
    {
        return new Buff(BuffType.Invincible, time, 10);
    }
    public static Buff BuffParalysis(int time)
    {
        return new Buff(BuffType.Paralysis, time, 10);
    }
    public static Buff BuffSleep(int time)
    {
        return new Buff(BuffType.Sleep, time, 10);
    }
    public static Buff BuffDoubleCast(int time)
    {
        return new Buff(BuffType.DoubleCast, time, 1);
    }
    public static Buff BuffCriticalStrike(int time,float percentage)
    {
        if (percentage < 0) percentage = 0;
        else if (percentage > 1) percentage = 1;
        return new Buff(BuffType.CriticalStrike, time, 7,percentage);
    }
    public static Buff BuffExplosiveInjury(int time,float percentage)
    {
        if (percentage < 0) percentage = 0;
        return new Buff(BuffType.ExplosiveInjury, time, 7,percentage);
    }
}
