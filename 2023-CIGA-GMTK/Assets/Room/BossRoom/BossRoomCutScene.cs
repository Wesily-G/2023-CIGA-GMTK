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
                print("dialogue");
                DialogueManager.AddDialogue("你","你是谁？");
                DialogueManager.AddDialogue("？？？","你不能再往前进了？");
                DialogueManager.AddDialogue("你","城堡里面究竟有什么？");
                DialogueManager.AddDialogue("？？？","你不需要知道？");
                DialogueManager.StartDialogue();
                break;
            case "2":
                break;
            case "3":
                break;
            case "4":
                break;
        }
    }
}
