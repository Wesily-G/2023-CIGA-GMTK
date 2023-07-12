using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomCutScene : MonoBehaviour
{
    private bool isStartCutScene;

    private void Update()
    {
        if (!RoomManager.IsSwitchRoom && !isStartCutScene)
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
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"Who are you?");
                DialogueManager.AddDialogue("？？？","You cant go into!");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"What is inside the castle?");
                DialogueManager.AddDialogue("？？？","You not need to know this.");
                DialogueManager.StartDialogue();
                break;
            case "2":
                DialogueManager.AddDialogue("？？？","You, coming?");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"Why don't you let me in?");
                DialogueManager.AddDialogue("？？？","You will know that.");
                DialogueManager.StartDialogue();
                break;
            case "3":
                DialogueManager.AddDialogue("？？？","Beat me!");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"... ...");
                DialogueManager.StartDialogue();
                break;
            case "4":
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"You ... are ?");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name+"???",$"I'm {GameObject.FindWithTag("Player").GetComponent<Player>().name}");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"You mean ... you are me???");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name+"???","Beat me, or you don't want to go in one step.");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"Why!?");
                DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name+"???","Want to be my word, he beat me!");
                DialogueManager.StartDialogue();
                break;
        }
    }
}
