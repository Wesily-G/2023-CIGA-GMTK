using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomCutScene : MonoBehaviour
{
    private bool isStartCutScene;

    private void Update()
    {
        if (!RoomManager.isSwitchRoom && !isStartCutScene)
        {
            isStartCutScene = true;
            StartCutScene();
        }
    }

    private void StartCutScene()
    {
        print(name.Split("_")[2]);
        switch (name.Split("_")[2].Replace("(Clone)",""))
        {
            case "1":
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"你是谁？");
                DialogueManager.AddDialogue("？？？","你不能再往前进了？");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"城堡里面究竟有什么？");
                DialogueManager.AddDialogue("？？？","你不需要知道？");
                DialogueManager.StartDialogue();
                break;
            case "2":
                DialogueManager.AddDialogue("？？？","你来到这里了吗。");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"为什么不让我进去？");
                DialogueManager.AddDialogue("？？？","哎，你会知道的。");
                DialogueManager.StartDialogue();
                break;
            case "3":
                DialogueManager.AddDialogue("？？？","战胜我吧。");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"。。。。。");
                DialogueManager.StartDialogue();
                break;
            case "4":
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"你是。。。");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name+"???",$"我是{GameObject.FindWithTag("Player").GetComponent<Player>().name}");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"你是。。我？？");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name+"???","战胜我吧，不然别想再进去一步。");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"为什么！");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name+"???","想成为我的话就战胜我！");
                DialogueManager.StartDialogue();
                break;
        }
    }
}
