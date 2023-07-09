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
    private static Player _instants;
    public float maxHp = 40;
    private float _hp = 40;
    public bool IsDead => Hp <= 0;
    [NonSerialized] 
    public bool isSleep;
    [NonSerialized] 
    public int vampireCount;

    public Animator animator;
    
    private readonly List<Buff> _buffs = new();

    private void Awake()
    {
        if (_instants == null)
        {
            _instants = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    public float Hp
    {
        get => _hp;
        set => _hp = value<=0?0:value>maxHp?maxHp:value;
    }

    public int VampireCount
    {
        set => vampireCount = value<=0?0:value;
    }

    public static void PlayerWalk()
    {
        _instants.animator.SetBool("IsWalk",true);
    }
    public static void PlayerIdle()
    {
        _instants.animator.SetBool("IsWalk",false);
    }

    private float _recordMaxHp;
    private float _recordHp;
    private int _recordMagicAmount;
    private int _recordMemory;
    public void RecordState()
    {
        _recordMaxHp = maxHp;
        _recordHp = _hp;
        _recordMagicAmount = SpellsManager.GetMagicAmount();
        _recordMemory = SpellsManager.GetInstance().currentMemory;
    }

    public void RollbackState()
    {
        maxHp = _recordMaxHp;
        _hp = _recordHp;
        SpellsManager.SetMagicAmount(_recordMagicAmount);
        SpellsManager.GetInstance().currentMemory = _recordMemory;
    }
    public void Kill()
    {
        
    }
    public float Attack(float damage)
    {
        Hp -= damage;
        return damage;
    }
    public float GetAttackMultiplier()
    {
        float magnification = 1;
        float criticalHitRate = 0;
        float explosiveInjury = 0;
        var daleyDelete = new Queue<Buff>();
        foreach (var buff in _buffs.OrderByDescending(a => a.priority))
        {
            if (buff.time == 0)
            {
                daleyDelete.Enqueue(buff);
            }
            switch (buff.type)
            {
                case BuffType.IncreasedInjury:
                    magnification += buff.percentage;
                    break;
                case BuffType.CriticalStrike:
                    criticalHitRate += buff.percentage;
                    break;
                case BuffType.ExplosiveInjury:
                    explosiveInjury += buff.percentage;
                    break;
            }
        }
        while (daleyDelete.Count>0)
        {
            _buffs.Remove(daleyDelete.Dequeue());
        }
        if (Ulit.Randomizer(criticalHitRate))
        {
            //先算暴击X2
            magnification *= 2;
            //再算爆伤倍率
            magnification *= 1 + explosiveInjury;
        }
        return magnification;
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

    public void AddMaxUp(float value)
    {
        maxHp += value;
    }

    public List<Buff> GetBuffs()
    {
        return _buffs;
    }

    public void AddBuff(Buff buff)
    {
        switch (buff.type)
        {
            case BuffType.Paralysis:
                isSleep = true;
                break;
            case BuffType.Sleep:
                isSleep = true;
                break;
            case BuffType.DelaySpell:
                isSleep = true;
                break;
        }
        _buffs.Add(buff);
        leking.UIManager.UpdatePlayerBuffUI(this);
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
                    damage += damage * (1+buff.percentage);
                    break;
                case BuffType.IncreasedInjury:
                    damage += damage * (1+buff.percentage);
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
            if (_buffs[i].time - 1 == 0||_buffs[i].time==0)
            {
                _buffs[i].onBuffEnd();
                _buffs.RemoveAt(i);
            }
            else if (_buffs[i].time > 1)
            {
                _buffs[i].time -= 1;
            }
        }

        leking.UIManager.UpdatePlayerBuffUI(this);
    }

    public bool CheckBuff(BuffType type)
    {
        for (int i = _buffs.Count-1; i >= 0; i--)
        {
            if (_buffs[i].type == type) return true;
        }

        return false;
    }

    public void CleanTempBuff()
    {
        for (int i = _buffs.Count-1; i >= 0; i--)
        {
            if (_buffs[i].isGlobal) continue;
            print(_buffs[i].type);
            _buffs.RemoveAt(i);
        }
        leking.UIManager.UpdatePlayerBuffUI(this);
    }

    public void OnOutBattle()
    {
        
    }
}