using System;
using System.Collections;
using System.Collections.Generic;
using leking;
using UnityEngine;

public class SkillTreeMap : MonoBehaviour
{
    private static bool _isNotFirstFalling;
    private static bool _isNotFirstPick;

    private void Start()
    {
        if (!_isNotFirstFalling && !RoomManager.isSwitchRoom)
        {
            _isNotFirstFalling = true;
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"好美的画。");
            DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"能摸一下吗。");
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
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"看着这幅星空我好像想起了什么。");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"这是我战斗的回忆吗？");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"我为什么要战斗？");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"我想不起来了。");
                DialogueManager.StartDialogue();
            }
            UIManager.ShowSkillTree();
        }
    }
}
