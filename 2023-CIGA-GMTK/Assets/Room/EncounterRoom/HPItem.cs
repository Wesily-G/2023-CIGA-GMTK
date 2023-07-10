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
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"What's this?");
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"Touch your can possibly know.");
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
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"Feel the body easily up!");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"This is my favorite thing, the hearts of cake!");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"It's the afternoon tea which will be on the table for dessert!");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"But, why I forget it?");
                DialogueManager.StartDialogue();
            }
            GameObject.FindWithTag("Player").GetComponent<Player>().Hp += GameObject.FindWithTag("Player").GetComponent<Player>().maxHp * 0.3f;
            RoomManager.ReadyNextRoom();
            Destroy(gameObject);
        }
    }
}
