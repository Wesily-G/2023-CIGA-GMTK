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
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"这里就是核心吗。");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"其实我知道了，我不是你是吗。");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"我才是外来者。");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"这是你的记忆。");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"是你引导我到这里来的吗。");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"原来如此，你希望我接管这具身体。");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"我会的，一直以来幸苦了。");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,$"感谢你{GameObject.FindWithTag("Player").GetComponent<Player>().name}");

        DialogueManager.AddDialogue("Developers",$"感谢你的游玩{GameObject.FindWithTag("Player").GetComponent<Player>().name}");
        DialogueManager.AddDialogue("Developers","Programmming: Leking, soraku7, Blueberry, Resonance");
        DialogueManager.AddDialogue("Developers","Design: Resonance, Bluebarry, Leking");
        DialogueManager.AddDialogue("Developers","Art: MiaoLikeIceCoke");
        DialogueManager.AddDialogue("Developers","Special thanks: Humble Pixel(https://humblepixel.itch.io/) for using UI assets.");
        DialogueManager.StartDialogue();
    }
}
