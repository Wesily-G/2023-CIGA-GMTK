using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private static BattleManager _instants;
    private List<Monster> _monster = new();

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
    
    public static void KillAllMonster()
    {
        if(_instants == null) return;
        foreach (var monster in _instants._monster)
        {
            monster.Kill();
        }
    }
}
