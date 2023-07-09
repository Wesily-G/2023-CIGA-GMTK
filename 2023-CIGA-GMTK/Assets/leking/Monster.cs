using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


public interface IMonster : IHealth, IBuff
{
    
}
public class Monster : MonoBehaviour,IMonster
{
    public float maxHp = 1000;
    private float _hp = 1000;
    public string monsterName = "Monster";
    public ElementTypes elementTypes;
    public MonsterAction action;

    private readonly List<Buff> _buffs = new();
    public bool isHighlight;
    public bool isSelected;
    public static bool isNotfirstDeath;
    public MemoryPoint memoryPoint;
    
    [NonSerialized] 
    public bool isSleep;
    [NonSerialized] 
    public int vampireCount;
    [NonSerialized] 
    public bool actionCompleted;

    public float Hp
    {
        get => _hp;
        set
        {
            if (value <= 0)
            {
                if (!isNotfirstDeath)
                {
                    isNotfirstDeath = false;
                    DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"从怪物身上掉出来了一个亮点");
                    DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"触碰一下会发生什么吗");
                    DialogueManager.StartDialogue();
                }
                Instantiate(memoryPoint).transform.position = transform.position;
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
        initColor = GetComponent<SpriteRenderer>().color;
        _hp = maxHp;
    }

    private Color initColor;
    private void Highlight()
    {
        
        GetComponent<SpriteRenderer>().color = Color.gray;
        print(name);
    }

    private void OnMouseOver()
    {
        isHighlight = true;
    }

    private void OnMouseExit()
    {
        isHighlight = false;
    }

    private void Update()
    {
        if (isHighlight)
        {
            Highlight();
        }
        else if(isSelected)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = initColor;
        }
    }

    private void LateUpdate()
    {
        // if (isHighlight) Highlight();
        // GetComponent<SpriteRenderer>().color = Color.white;
        // isHighlight = false;
    }
    public void MonsterAction()
    {
        Task.Delay(1000).GetAwaiter().OnCompleted(() =>
        {
            if (action != null)
            {
                action.Action(this);
            }
            actionCompleted = true;
        });
    }
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
        }
        _buffs.Add(buff);
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
                    criticalHitRate += buff.percentage;
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
            _buffs.RemoveAt(i);
        }
    }
}