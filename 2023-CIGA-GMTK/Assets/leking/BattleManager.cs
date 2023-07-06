using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameplayTest.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class BattleManager : MonoBehaviour
{
    private static BattleManager _instants;
    
    public Player player;
    public Transform monsterSpawnStart;
    public Transform monsterSpawnEnd;
    private readonly List<Monster> _monsters = new();
    private int _stageNumber;
    private int _roundCount;

    private int _selectMonsterIndex;
    public static event Action buffChange;
    //命令队列
    private readonly Queue<Action> _firstCommandQueue = new();
    private readonly Queue<Action> _roundCommandQueue = new();

    public int StageNumber
    {
        get => _stageNumber;
        set => _stageNumber = value % 3;
    }

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

    public static void StartBattle()
    {
        _instants.SpawnMonster();
        _instants._inBattle = true;
    }
    public static void KillAllMonster()
    {
        if(_instants == null) return;
        foreach (var monster in _instants._monsters)
        {
            monster.Kill();
        }
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

    //添加Buff
    public static void AddPlayerBuff(Buff buff)
    {
        _instants.player.AddBuff(buff);
        buffChange?.Invoke();
    }
    public static void AddSelectedMonsterBuff(Buff buff)
    {
        _instants._monsters[_instants._selectMonsterIndex].AddBuff(buff);
        buffChange?.Invoke();
    }
    public static void AddAllMonsterBuff(Buff buff)
    {
        
    }
    
    //攻击
    public static void AttackPlayer(Monster monster,float damage,ElementTypes type)
    {
        
    }
    public static void AttackSelectedMonster(float damage,ElementTypes type)
    {
        
    }
    public static void AttackAllMonster(float damage,ElementTypes type)
    {
        
    }
    
    //延迟施法
    public static void PlayerDelaySpell(int time,Action spell)
    {
        AddPlayerBuff(Buff.BuffDelaySpell(time,spell));
    }

    public static void InterruptPlayer()
    {
        
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
            _inBattle = false;
            _battleOver = true;
            RoomManager.NextRoom();
        }
        if(_battleOver) return;
        switch (StageNumber)
        {
            case 0:
                ProcessFirst();
                StageNumber++;
                _roundCount++;
                leking.UIManager.SetRoundNumber(_roundCount);
                GameObject.Find("CardManager").GetComponent<CardManager>().actionable = true;
                break;
            case 1:
                InRound();
                break;
            case 2:
                CloseoutPhase();
                StageNumber++;
                break;
        }
    }

    private void ProcessFirst()
    {
        print("ProcessFirst");
        while (_firstCommandQueue.Count>0)
        {
            _firstCommandQueue.Dequeue()();
        }
        player.ExecuteBuffs();
        foreach (var monster in _monsters)
        {
            monster.ExecuteBuffs();
        }
        player.BuffNext();
        foreach (var monster in _monsters)
        {
            monster.BuffNext();
        }
        buffChange?.Invoke();
    }

    private void InRound()
    {
        while (_roundCommandQueue.Count>0)
        {
            _roundCommandQueue.Dequeue()();
        }
    }

    private void CloseoutPhase()
    {
        print("CloseoutPhase");
    }
    
    

    #region DEBUG
    public void NextStage()
    {
        StageNumber++;
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
    #endregion
    
}
