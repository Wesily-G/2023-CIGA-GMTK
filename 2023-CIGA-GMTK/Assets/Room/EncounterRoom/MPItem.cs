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
        if (!_isNotFirstFalling && !RoomManager.IsSwitchRoom)
        {
            _isNotFirstFalling = true;
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"It sends the light blue color star.");
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"Touch it maybe thinking of what may.");
            DialogueManager.StartDialogue();
        }
    }

    private void OnMouseDown()
    {
        if (!RoomManager.IsSwitchRoom)
        {
            if (!_isNotFirstPick)
            {
                _isNotFirstPick = true;
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"The magic of spirit!");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"That is ...");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"This is the magic star, the constituent parts of the world.");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"Using it can...");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"But I can't remember.");
                DialogueManager.StartDialogue();
            }
            SpellsManager.AddMagicAmount(30);
            RoomManager.ReadyNextRoom();
            Destroy(gameObject);
        }
    }
}
