using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public interface IPlayer : IHealth, IBuff
{
    
}
public class Player : MonoBehaviour,IPlayer
{
    public float maxHp = 10;
    private float _hp = 10;
    public bool IsDead => Hp <= 0;

    public float Hp
    {
        get => _hp;
        set => _hp = value<=0?0:value;
    }

    private readonly List<Buff> _buffs = new();

    public void Kill()
    {
        
    }

    public void Attack(float damage)
    {
        Hp -= damage;
    }

    public void Attack(float damage, ElementTypes type)
    {
        float finalDamage = damage;
        foreach (var buff in _buffs.OrderByDescending(a => a.priority))
        {
            switch (buff.type)
            {
                case BuffType.Fragile:
                    finalDamage += finalDamage * buff.percentage;
                    break;
                case BuffType.FireResistance:
                    if (type == ElementTypes.Fire) finalDamage *= (1-buff.percentage);
                    break;
                case BuffType.WaterResistance:
                    if (type == ElementTypes.Water) finalDamage *= (1-buff.percentage);
                    break;
                case BuffType.LightingResistance:
                    if (type == ElementTypes.Lighting) finalDamage *= (1-buff.percentage);
                    break;
                case BuffType.Invincible:
                    finalDamage = 0;
                    break;
            }
        }
        Hp -= finalDamage;
    }

    public float GetHp()
    {
        return Hp;
    }

    public List<Buff> GetBuffs()
    {
        return _buffs;
    }

    public void AddBuff(Buff buff)
    {
        _buffs.Add(buff);
        leking.UIManager.UpdatePlayerBuffUI(this);
    }

    public void ExecuteBuffs()
    {
        float damage = 0;
        foreach (var buff in _buffs.OrderByDescending(a => a.priority))
        {
            switch (buff.type)
            {
                case BuffType.Burn:
                    damage += maxHp * 0.05f;
                    break;
                case BuffType.Fragile:
                    damage += damage * (1+buff.percentage);
                    break;
                case BuffType.DamageIncrease:
                    damage += damage * (1+buff.percentage);
                    break;
            }
        }
        Hp -= damage;
    }

    public void RemoveBuff()
    {
        
    }

    public void BuffNext()
    {
        for (int i = _buffs.Count - 1; i >= 0; i--)
        {
            if(_buffs[i].time == -1) continue;
            if (_buffs[i].time - 1 == 0 || _buffs[i].time == 0)
            {
                _buffs.RemoveAt(i);
            }
            else if (_buffs[i].time > 1)
            {
                _buffs[i].time -= 1;
            }
        }

        leking.UIManager.UpdatePlayerBuffUI(this);
    }
}
