using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPItem : MonoBehaviour
{
    private static bool _isNotFirstFalling;
    private static bool _isNotFirstPick;

    private void Start()
    {
        if (!_isNotFirstFalling && !RoomManager.isSwitchRoom)
        {
            _isNotFirstFalling = true;
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"这是什么？");
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"触摸一下或许就可以知道。");
            DialogueManager.StartDialogue();
        }
    }

    private void OnMouseDown()
    {
        if (!_isNotFirstPick)
        {
            _isNotFirstPick = true;
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"感觉身体轻松了起来！");
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"这是我最爱吃的东西，红心蛋糕！");
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"我下午茶的时候一定会摆在桌子上的甜点！");
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"但是，我为什么会忘记这一切呢？");
            DialogueManager.StartDialogue();
        }
        GameObject.FindWithTag("Player").GetComponent<Player>().Hp += GameObject.FindWithTag("Player").GetComponent<Player>().maxHp * 0.3f;
        RoomManager.ReadyNextRoom();
        Destroy(gameObject);
    }
}
