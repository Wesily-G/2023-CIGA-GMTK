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
        DialogueManager.AddDialogue("？？？",$"感谢你的游玩{GameObject.FindWithTag("Player").GetComponent<Player>().name}");
        DialogueManager.StartDialogue();
    }
}
