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
        DialogueManager.AddDialogue("???","Here is the... The castle?");
        DialogueManager.AddDialogue("???","Who am I?");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,$"I remember that my name is {GameObject.FindWithTag("Player").GetComponent<Player>().name}");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"I can't remember everything, holy .. ");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"But I think I came to this.");
        DialogueManager.AddDialogue(GameObject.FindWithTag("Player").GetComponent<Player>().name,"Go in and have a look.");
        DialogueManager.StartDialogue();
    }
}
