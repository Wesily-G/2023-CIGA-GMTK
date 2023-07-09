using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingRoom : MonoBehaviour
{
    private bool isStartCutScene;
    // Update is called once per frame
    void Update()
    {
        if (!RoomManager.isSwitchRoom && !isStartCutScene)
        {
            isStartCutScene = true;
            StartCutScene();
        }
    }

    private void StartCutScene()
    {
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"Here is the core?");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"In fact, I know, I'm not your right.");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"I am an outsider.");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"This is the memory of you.");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"Here is your guide me?");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"So, you want me to take over the this body");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"I will, I have been fortunate bitter.");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,$"Thank you, {GameObject.FindWithTag("Player").GetComponent<Player>().name}");

        DialogueManager.AddDialogue("Developers",$"Thanks for your playing, {GameObject.FindWithTag("Player").GetComponent<Player>().name}");
        DialogueManager.AddDialogue("Developers","Programmming: Leking, soraku7, Blueberry, Resonance");
        DialogueManager.AddDialogue("Developers","Design: Resonance, Bluebarry, Leking");
        DialogueManager.AddDialogue("Developers","Art: MiaoLikeIceCoke");
        DialogueManager.AddDialogue("Developers","Special thanks: Humble Pixel(https://humblepixel.itch.io/) for using UI assets.");
        DialogueManager.StartDialogue();
    }
}
