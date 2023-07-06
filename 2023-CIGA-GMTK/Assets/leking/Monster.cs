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

    public float Hp
    {
        get => _hp;
        set
        {
            if (value <= 0)
            {
                Kill();
            }
            _hp = value;
        }
    }

    private void Start()
    {
        leking.UIManager.AddMonsterHpBar(this,Vector3.up);
        leking.UIManager.AddMonsterBuffBar(this,Vector3.down);
    }

    public void MonsterAction()
    {
        
    }
    public void Kill()
    {
        Destroy(gameObject);
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
                    if (type == ElementTypes.Fire) finalDamage *= buff.percentage;
                    break;
                case BuffType.WaterResistance:
                    if (type == ElementTypes.Water) finalDamage *= buff.percentage;
                    break;
                case BuffType.LightingResistance:
                    if (type == ElementTypes.Lighting) finalDamage *= buff.percentage;
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
                    damage += damage * buff.percentage;
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
    }
}
