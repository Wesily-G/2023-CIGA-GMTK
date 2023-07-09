using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class StartUserName : MonoBehaviour
{
    public TMP_InputField playerName;
    public PlayableDirector timeLine;

    public void StartGame()
    {
        if (playerName.text.Trim() == "")
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().name = "Alice";
        }
        else
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().name = playerName.text;
        }
        timeLine.stopped += TimeLineOnPlayed;
        timeLine.Play();
    }

    private void TimeLineOnPlayed(PlayableDirector obj)
    {
        Destroy(gameObject);
        DialogueManager.AddDialogue("???","这里是。。。城堡？");
        DialogueManager.AddDialogue("???","我是谁？");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,$"我记得我叫{GameObject.FindWithTag("Player").GetComponent<Player>().name}");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"我什么都想不起来了。");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"但我记得我好像来过这。");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"进去看看吧。");
        DialogueManager.StartDialogue();
    }
}
