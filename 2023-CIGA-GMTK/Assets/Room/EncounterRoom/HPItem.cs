using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPItem : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameObject.FindWithTag("Player").GetComponent<Player>().Hp += GameObject.FindWithTag("Player").GetComponent<Player>().maxHp * 0.3f;
        RoomManager.ReadyNextRoom();
        Destroy(gameObject);
    }
}
