using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameplayTest.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public enum StageType
{
    RoundStart,
    PlayerPart,
    PlayerPartEnd,
    MonsterPart,
    RoundEnd
}
public class BattleManager : MonoBehaviour
{
    private static BattleManager _instants;
    
    public Player player;
    public Transform monsterSpawnStart;
    public Transform monsterSpawnEnd;
    public Transform avatarPos;
    private readonly List<Monster> _monsters = new();
    private StageType _currentStage;
    private int _roundCount;
    
    private int _selectMonsterIndex;

    private Avatar _currentAvatar;
    public Avatar avatarPrefab;
    public static event Action buffChange;
    //命令队列
    private readonly Queue<Action> _firstCommandQueue = new();
    private readonly Queue<Action> _roundCommandQueue = new();
    private readonly Queue<Action> _playerCastQueue = new();
    private readonly Queue<Action> _monsterCastQueue = new();

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

    private readonly Queue<GameObject> _monsterSpawnQueue = new();
    public static void AddMonster(GameObject monster)
    {
        _instants._monsterSpawnQueue.Enqueue(monster);
    }

    private bool _battleOver;
    private bool _inBattle;
    
    public static void KillAllMonster()
    {
        if(_instants == null) return;
        foreach (var monster in _instants._monsters)
        {
            monster.Kill();
        }
    }

    public static int GetRoundNumber()
    {
        return _instants._roundCount;
    }
    //添加命令
    public static void AddFirstCommand(Action action)
    {
        _instants._firstCommandQueue.Enqueue(action);
    }
    public static void AddRoundCommand(Action action)
    {
        _instants._roundCommandQueue.Enqueue(action);
    }
    public static void AddPlayerCastQueue(Action spell)
    {
        _instants._playerCastQueue.Enqueue(spell);
    }
    public static void AddMonsterCastQueue(Action spell)
    {
        _instants._monsterCastQueue.Enqueue(spell);
    }
    //全局
    public static void IncreasePlayerMaxHp(float value)
    {
        _instants.player.AddMaxUp(value);
    }
    public static void IncreaseMonsterMaxHp(Monster monster,float value)
    {
        monster.AddMaxUp(value);
    }
    public static void IncreaseSelectedMonsterMaxHp(float value)
    {
        _instants._monsters[_instants._selectMonsterIndex].AddMaxUp(value);
    }
    public static void IncreaseAllMonsterMAxHp(float value)
    {
        for (int i = _instants._monsters.Count - 1; i >= 0; i--)
        {
            _instants._monsters[i].AddMaxUp(value);
        }
    }
    //分身
    public static void CreateAvatar(ElementTypes type, Action onDead)
    {
        if (_instants._currentAvatar != null)
        {
            _instants._currentAvatar.Kill();
        }
        _instants._currentAvatar = Instantiate(_instants.avatarPrefab, _instants.avatarPos.transform);
        _instants._currentAvatar.SetHp(_instants.player.maxHp * 0.3f);
        _instants._currentAvatar.type = type;
        _instants._currentAvatar.onDead = onDead;
    }
    //回复
    public static void HealPlayer(float value)
    {
        _instants.player.Heal(value);
    }
    public static void HealMonster(Monster monster,float value)
    {
        monster.Heal(value);
    }
    public static void HealSelectedMonster(float value)
    {
        _instants._monsters[_instants._selectMonsterIndex].Heal(value);
    }
    public static void HealAllMonster(float value)
    {
        foreach (var monster in _instants._monsters)
        {
            monster.Heal(value);
        }
    }
    //Buff相关
    public static void AddPlayerBuff(Buff buff)
    {
        _instants.player.AddBuff(buff);
        buffChange?.Invoke();
    }
    public static void AddMonsterBuff(Monster monster, Buff buff)
    {
        monster.AddBuff(buff);
        buffChange?.Invoke();
    }
    public static void AddSelectedMonsterBuff(Buff buff)
    {
        _instants._monsters[_instants._selectMonsterIndex].AddBuff(buff);
        buffChange?.Invoke();
    }
    public static void AddAllMonsterBuff(Buff buff)
    {
        foreach (var monster in _instants._monsters)
        {
            monster.AddBuff(buff);
        }
        buffChange?.Invoke();
    }
    public static void RemovePlayerBuffs(BuffType type)
    {
        _instants.player.RemoveBuffs(type);
        buffChange?.Invoke();
    }
    public static void RemoveMonsterBuffs(Monster monster, BuffType type)
    {
        monster.RemoveBuffs(type);
        buffChange?.Invoke();
    }
    public static void RemoveSelectedMonsterBuffs(BuffType type)
    {
        _instants._monsters[_instants._selectMonsterIndex].RemoveBuffs(type);
        buffChange?.Invoke();
    }
    public static void RemoveAllMonsterBuffs(BuffType type)
    {
        foreach (var monster in _instants._monsters)
        {
            monster.RemoveBuffs(type);
        }
        buffChange?.Invoke();
    }
    public static bool CheckPlayerBuff(BuffType type)
    {
        return _instants.player.CheckBuff(type);
    }
    public static bool CheckMonsterBuff(Monster monster, BuffType type)
    {
        return monster.CheckBuff(type);
    }
    public static bool CheckSelectedMonsterBuff(BuffType type)
    {
        return _instants._monsters[_instants._selectMonsterIndex].CheckBuff(type);
    }
    public static bool CheckAllMonsterBuff(BuffType type)
    {
        for (int i = _instants._monsters.Count; i >= 0; i--)
        {
            if (_instants._monsters[i].CheckBuff(type)) return true;
        }
        return false;
    }
    //攻击相关
    public static float AttackPlayer(Monster monster,float damage,ElementTypes type)
    {
        var d = monster.GetAttackMultiplier()*damage;
        float rd = d;
        if (_instants._currentAvatar != null)
        {
            rd = _instants._currentAvatar.Attack(d, type);
        }
        else
        {
            rd = _instants.player.Attack(d, type);
        }
        if (monster.vampireCount > 0)
        {
            monster.Heal(rd);
        }
        buffChange?.Invoke();
        return rd;
    }
    public static float AttackMonster(Monster monster,float damage,ElementTypes type)
    {
        var d = _instants.player.GetAttackMultiplier()*damage;
        var rd = monster.Attack(d, type);
        if (_instants.player.vampireCount > 0)
        {
            _instants.player.Heal(rd);
        }
        buffChange?.Invoke();
        return rd;
    }
    public static float AttackSelectedMonster(float damage,ElementTypes type)
    {
        var d = _instants.player.GetAttackMultiplier()*damage;
        var rd = _instants._monsters[_instants._selectMonsterIndex].Attack(d, type);
        if (_instants.player.vampireCount > 0)
        {
            _instants.player.Heal(rd);
        }
        buffChange?.Invoke();
        return rd;
    }
    public static float AttackAllMonster(float damage,ElementTypes type)
    {
        float endDamage = 0;
        var d = _instants.player.GetAttackMultiplier()*damage;
        for (int i = _instants._monsters.Count - 1; i >= 0; i--)
        {
            var rd =_instants._monsters[i].Attack(d, type);
            endDamage += rd;
            if (_instants.player.vampireCount > 0)
            {
                _instants.player.Heal(rd);
            }
        }
        buffChange?.Invoke();
        return endDamage;
    }
    //吸血
    public static void AddPlayerVampire()
    {
        _instants.player.vampireCount += 1;
    }
    public static void AddMonsterVampire(Monster monster)
    {
        monster.vampireCount += 1;
    }
    public static void AddSelectedMonsterVampire()
    {
        _instants._monsters[_instants._selectMonsterIndex].vampireCount += 1;
    }
    public static void AddAllMonsterVampire()
    {
        for (int i = _instants._monsters.Count - 1; i >= 0; i--)
        {
            var rd = _instants._monsters[_instants._selectMonsterIndex].vampireCount += 1;
        }
    }
    //延迟施法
    public static void DelaySpellPlayer(int time,Action spell)
    {
        AddPlayerBuff(Buff.BuffDelaySpell(time,spell));
    }
    public static void DelaySpellMonster(Monster monster,int time,Action spell)
    {
        AddMonsterBuff(monster,Buff.BuffDelaySpell(time,spell));
    }
    public static void DelaySpellSelectedMonster(int time,Action spell)
    {
        AddSelectedMonsterBuff(Buff.BuffDelaySpell(time,spell));
    }
    public static void DelaySpellAllMonster(int time,Action spell)
    {
        AddAllMonsterBuff(Buff.BuffDelaySpell(time,spell));
    }
    public static void InterruptPlayer()
    {
        RemovePlayerBuffs(BuffType.DelaySpell);
    }
    public static void InterruptMonster(Monster monster)
    {
        RemoveMonsterBuffs(monster,BuffType.DelaySpell);
    }
    public static void InterruptSelectedMonster()
    {
        RemoveMonsterBuffs(_instants._monsters[_instants._selectMonsterIndex],BuffType.DelaySpell);
    }
    public static void InterruptAllMonster()
    {
        RemoveAllMonsterBuffs(BuffType.DelaySpell);
    }
    
    
    

    public void SpawnMonster()
    {
        while (_monsterSpawnQueue.Count>0)
        {
            var monster = KGameObject.Instantiate(_monsterSpawnQueue.Dequeue()).GetComponent<Monster>();
            monster.name = monster.monsterName;
            _monsters.Add(monster);
        }

        UpdateMonstersPos();
    }
    public void UpdateMonstersPos()
    {
        for (var i = 0; i<_monsters.Count; i++)
        {
            var monster = _monsters[i];
            //显示怪物
            monster.gameObject.SetActive(true);
            //设置怪物的排序层
            monster.gameObject.GetComponent<SpriteRenderer>().sortingOrder = _monsters.Count-i-1;
            //对怪物位置进行线性插值
            var t = _monsters.Count - 1>0?i / (float)(_monsters.Count - 1):0;
            var sp = monsterSpawnStart.position;
            var ep = monsterSpawnEnd.position;
            if (_monsters.Count < 4)
            {
                var halfPos = (1 / 2f) * (ep - sp);
                var offsetPos = ((4 -_monsters.Count)/4f)*halfPos;
                sp += offsetPos;
                ep -= offsetPos;
            }
            var newPos = Vector3.Lerp(sp, ep, t);
            //移动怪物到新位置
            //monster.transform.GetComponent<KGameObject>().MoveTo(newPos);
            monster.transform.position = newPos;
        }
    }

    private void RemoveDeadMonster()
    {
        for (var i = _monsters.Count - 1; i >=0; i--)
        {
            if (_monsters[i] == null)
            {
                _monsters.RemoveAt(i);
            }
        }
    }

    private void ResetBattle()
    {
        KillAllMonster();
        if (_currentAvatar != null)
        {
            _currentAvatar.Kill();
        }
        _inBattle = true;
        _battleOver = false;
        _currentStage = StageType.RoundStart;
        _roundCount = 0;
    }
    public static void StartBattle()
    {
        _instants.ResetBattle();
        _instants.SpawnMonster();
        CardManager.ShowCard();
    }
    public static void EndBattle()
    {
        _instants._inBattle = false;
        _instants.player.CleanTempBuff();
        KillAllMonster();
        if (_instants._currentAvatar != null)
        {
            _instants._currentAvatar.Kill();
        }
        CardManager.HideCard();
        buffChange?.Invoke();
    }
    private void Update()
    {
        RemoveDeadMonster();
        if(!_inBattle) return;
        if (player.IsDead)
        {
            leking.UIManager.ShowMessage("Dead!");
            _inBattle = false;
            _battleOver = true;
            return;
        }
        if (_monsters.Count <= 0)
        {
            EndBattle();
            _battleOver = true;
            RoomManager.OnBattleCompletion();
        }
        if(_battleOver) return;
        switch (_currentStage)
        {
            case StageType.RoundStart:
                RoundStart();
                _currentStage = StageType.PlayerPart;
                _roundCount++;
                leking.UIManager.SetRoundNumber(_roundCount);
                GameObject.Find("CardManager").GetComponent<CardManager>().actionable = true;
                break;
            case StageType.PlayerPart:
                PlayerPart();
                break;
            case StageType.PlayerPartEnd:
                PlayerPartEnd();
                _currentStage = StageType.MonsterPart;
                break;
            case StageType.MonsterPart:
                MonsterPart();
                break;
            case StageType.RoundEnd:
                RoundEnd();
                _currentStage = StageType.RoundStart;
                break;
        }
    }

    private void RoundStart()
    {
        while (_firstCommandQueue.Count>0)
        {
            _firstCommandQueue.Dequeue()();
        }
    }

    private void PlayerPart()
    {
        if (_instants.player.isSleep)
        {
            _currentStage = StageType.PlayerPartEnd;
            return;
        }
        while (_roundCommandQueue.Count>0)
        {
            _roundCommandQueue.Dequeue()();
        }

        while (_playerCastQueue.Count>0)
        {
            var action = _playerCastQueue.Dequeue();
            action();
            if (CheckPlayerBuff(BuffType.DoubleCast))
            {
                print("Double");
                action();
            }
        }
    }

    private void PlayerPartEnd()
    {
        _monsterReadyStack.Clear();
        for (int i = _monsters.Count -1; i >=0; i--)
        {
            _monsterReadyStack.Push(_monsters[i]);
        }
    }
    private void MonsterPart()
    {
        if (_monsterReadyStack.Count <= 0 && _monsterActionQueue.Count <= 0 && _monsterCastQueue.Count <= 0)
        {
            _currentStage = StageType.RoundEnd;
            return;
        }
        if(_monsterActionQueue.Count<=0 && _monsterCastQueue.Count<=0 && _monsterReadyStack.Count>0)
        {
            _currentActionMonster = _monsterReadyStack.Pop();
            _currentActionMonster.MonsterAction();
            _monsterActionQueue.Enqueue(_currentActionMonster);
        }
        else
        {
            if (_monsterActionQueue.Peek().actionCompleted)
            {
                _monsterActionQueue.Dequeue().actionCompleted = false;
            }
        }
        while (_monsterCastQueue.Count>0)
        {
            var action = _monsterCastQueue.Dequeue();
            action();
            if (CheckMonsterBuff(_currentActionMonster,BuffType.DoubleCast))
            {
                action();
            }
        }
    }

    private Stack<Monster> _monsterReadyStack = new();
    private Queue<Monster> _monsterActionQueue = new();
    private Monster _currentActionMonster;
    private void RoundEnd()
    {
        _instants.player.isSleep = false;
        for (int i = _instants._monsters.Count - 1; i >= 0; i--)
        {
            _instants._monsters[i].isSleep = false;
        }
        player.BuffNext();
        foreach (var monster in _monsters)
        {
            monster.BuffNext();
        }
        player.ExecuteBuffs();
        foreach (var monster in _monsters)
        {
            monster.ExecuteBuffs();
        }
        buffChange?.Invoke();
    }
    
    #region DEBUG
    public void NextStage()
    {
        _currentStage = StageType.PlayerPartEnd;
        GameObject.Find("CardManager").GetComponent<CardManager>().actionable = false;
    }
    public void OnStartButtonDown()
    {
        StartBattle();
    }
    public void KillForemost()
    {
        if(_monsters.Count<=0) return;
        var monster = _monsters[0];
        _monsters.RemoveAt(0);
        monster.Kill();
        //UpdateCardsPos();
    }

    public void AttackForemost()
    {
        if(_monsters.Count<=0) return;
        var monster = _monsters[0];
        monster.Attack(1);
    }

    public static void AttackMonster()
    {
        if(_instants._monsters.Count<=0) return;
        var monster = _instants._monsters[0];
        monster.Attack(1);
    }

    public void AddBurnToPlayer()
    {
        AddPlayerBuff(new Buff(BuffType.Burn,3,1));
    }
    public void AddFragileToPlayer()
    {
        AddPlayerBuff(new Buff(BuffType.Fragile,3,0,0.3f));
    }
    public void AddBurnToMonster()
    {
        AddSelectedMonsterBuff(new Buff(BuffType.Burn,3,1));
    }
    public void AddFragileToMonster()
    {
        AddSelectedMonsterBuff(new Buff(BuffType.Fragile,3,0,0.3f));
    }

    public void AddParalysisPlayer()
    {
        AddPlayerBuff(Buff.BuffParalysis(3));
    }
    public void AddFireAvatar()
    {
        CreateAvatar(ElementTypes.Fire, () =>
        {
            AttackAllMonster(40,ElementTypes.Fire);
            AddAllMonsterBuff(Buff.BuffBurn(3));
        });
    }
    public void AddWaterAvatar()
    {
        CreateAvatar(ElementTypes.Water, () =>
        {
            AddAllMonsterBuff(Buff.BuffFragile(2,0.3f));
        });
    }
    #endregion
    
}
