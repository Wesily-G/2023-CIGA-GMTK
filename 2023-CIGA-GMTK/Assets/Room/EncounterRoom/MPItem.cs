using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPItem : MonoBehaviour
{
    private static bool _isNotFirstFalling;
    private static bool _isNotFirstPick;

    private void Start()
    {
        if (!_isNotFirstFalling && !RoomManager.isSwitchRoom)
        {
            _isNotFirstFalling = true;
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"发出着淡蓝色光的星星。");
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"触摸一下或许会想起什么。");
            DialogueManager.StartDialogue();
        }
    }

    private void OnMouseDown()
    {
        if (!RoomManager.isSwitchRoom)
        {
            if (!_isNotFirstPick)
            {
                _isNotFirstPick = true;
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"好强的魔力！");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"这是。。");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"这是，魔力星，构成世界的元素。");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"使用它就可以。。。");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"我想不起来。");
                DialogueManager.StartDialogue();
            }
            SpellsManager.AddMagicAmount(30);
            RoomManager.ReadyNextRoom();
            Destroy(gameObject);
        }
    }
}
