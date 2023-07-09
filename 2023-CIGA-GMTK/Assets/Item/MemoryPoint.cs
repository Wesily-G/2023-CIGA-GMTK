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
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"从怪物身上掉出来了一个亮点");
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"触碰一下会发生什么吗");
            DialogueManager.StartDialogue();
        }
    }

    private void OnMouseDown()
    {
        if (!_isNotFirstPick)
        {
            _isNotFirstPick = true;
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"这是。。。");
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"我想起来了，我来过这里。。。");
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"不行。");
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"其他的我想不起来了。");
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"如果继续触摸这些光的话，我或许就能想起什么。");
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"继续前进吧。");
            DialogueManager.StartDialogue();
        }
        SpellsManager.GetInstance().currentMemory = memoryValue;
        Destroy(gameObject);
    }
}
