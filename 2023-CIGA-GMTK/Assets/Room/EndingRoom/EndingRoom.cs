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
        DialogueManager.AddDialogue("Developers",$"感谢你的游玩{GameObject.FindWithTag("Player").GetComponent<Player>().name}");
        DialogueManager.AddDialogue("Developers","Programmming: Leking, soraku7, Blueberry, Resonance");
        DialogueManager.AddDialogue("Developers","Design: Resonance, Bluebarry, Leking");
        DialogueManager.AddDialogue("Developers","Art: MiaoLikeIceCoke");
        DialogueManager.AddDialogue("Developers","Special thanks: Humble Pixel(https://humblepixel.itch.io/) for using UI assets.");
        DialogueManager.StartDialogue();
    }
}
