using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public interface IMonster : IHealth, IBuff
{
    
}
public class Monster : MonoBehaviour,IMonster
{
    public float maxHp = 10;
    private float _hp = 10;
    public string monsterName = "Monster";
    
    private readonly List<Buff> _buffs = new();
    
    public bool isSleep;
    
    public int vampireCount;

    public float Hp
    {
        get => _hp;
        set
        {
            if (value <= 0)
            {
                Kill();
            }
            else if (value > maxHp)
            {
                _hp = maxHp;
            }
            _hp = value;
        }
    }

    public int VampireCount
    {
        get => vampireCount;
        set => vampireCount = value<=0?0:value;
    }

    private void Start()
    {
        leking.UIManager.AddMonsterHpBar(this,Vector3.up);
        leking.UIManager.AddMonsterBuffBar(this,Vector3.down);
        _hp = maxHp;
    }

    public virtual void MonsterAction(){}
    public void Kill()
    {
        Destroy(gameObject);
    }
    
    public float Attack(float damage)
    {
        
        Hp -= damage;
        return damage;
    }

    public float Attack(float damage, ElementTypes type)
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
        return finalDamage;
    }

    public void Heal(float value)
    {
        Hp += value;
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
    }

    public float GetAttackMultiplier()
    {
        float magnification = 1;
        foreach (var buff in _buffs.OrderByDescending(a => a.priority))
        {
            switch (buff.type)
            {
                case BuffType.IncreasedInjury:
                    magnification += 0.2f;
                    break;
            }
        }

        return magnification;
    }
    public void ExecuteBuffs()
    {
        float damage = 0;
        var daleyDelete = new Queue<Buff>();
        foreach (var buff in _buffs.OrderByDescending(a => a.priority))
        {
            switch (buff.type)
            {
                case BuffType.Burn:
                    damage += maxHp * 0.05f;
                    break;
                case BuffType.Fragile:
                    damage += damage * buff.percentage;
                    break;
                case BuffType.Paralysis:
                    isSleep = true;
                    break;
                case BuffType.Sleep:
                    isSleep = true;
                    break;
                case BuffType.DelaySpell:
                    if (isSleep)
                    {
                        daleyDelete.Enqueue(buff);
                    }
                    else isSleep = true;
                    break;
            }
        }
        while (daleyDelete.Count>0)
        {
            _buffs.Remove(daleyDelete.Dequeue());
        }
        Hp -= damage;
    }

    public void RemoveBuff()
    {
        
    }
    public void RemoveBuffs(BuffType type)
    {
        for (int i = _buffs.Count - 1; i >= 0; i--)
        {
            if (_buffs[i].type == type)
            {
                _buffs.RemoveAt(i);
            }
        }
    }
    public void BuffNext()
    {
        for (int i = _buffs.Count - 1; i >= 0; i--)
        {
            if(_buffs[i].time == -1) continue;
            if (_buffs[i].time == 0)
            {
                _buffs[i].onBuffEnd();
                _buffs.RemoveAt(i);
            }
            else if (_buffs[i].time > 1)
            {
                _buffs[i].time -= 1;
            }
        }
    }
}