using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPoint : MonoBehaviour
{
    public int memoryValue;
    private static bool _isNotFirstFalling;
    private static bool _isNotFirstPick;

    private void Start()
    {
        if (!_isNotFirstFalling)
        {
            _isNotFirstFalling = true;
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"A window from monsters fall out");
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"If touch it will happen something?");
            DialogueManager.StartDialogue();
        }
    }

    private void OnMouseDown()
    {
        if (!_isNotFirstPick)
        {
            _isNotFirstPick = true;
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"It is ... ?");
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"I remember, I have been here...");
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"Emm ... I think I can not something else.");
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"If continue to touch the light, I might be able to think of anything.");
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"Move on.");
            DialogueManager.StartDialogue();
        }
        SpellsManager.GetInstance().currentMemory = memoryValue;
        Destroy(gameObject);
    }
}
