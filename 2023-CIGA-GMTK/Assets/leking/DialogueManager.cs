using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private struct Dialogue
    {
        public string speakerName;
        public string dialogue;
        public Dialogue(string speakerName, string dialogue)
        {
            this.speakerName = speakerName;
            this.dialogue = dialogue;
        }
    }

    public GameObject dialogueUI;
    private static DialogueManager _instants;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI speakerNameText;
    public Button continueButton;

    private Queue<Dialogue> _dialogueQueue = new();
    private bool _onDialogueStart;

    public static bool InDialogue => _instants._onDialogueStart;

    private void Awake()
    {
        if (_instants == null)
        {
            _instants = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        speakerNameText.text = "";
        dialogueText.text = "";
        continueButton.onClick.AddListener(Continue);
        //dialogueUI.SetActive(false);
    }

    public static void AddDialogue(string speakerName, string dialogue)
    {
        if(_instants._onDialogueStart) return;
        _instants._dialogueQueue.Enqueue(new Dialogue(speakerName,dialogue));
    }

    public static void StartDialogue()
    {
        _instants._onDialogueStart = true;
        _instants.Continue();
        _instants.ShowDialogue();
    }

    private void ShowDialogue()
    {
        print(dialogueUI);
        dialogueUI.SetActive(true);
    }
    private void HideDialogue()
    {
        dialogueUI.SetActive(false);
    }

    private Coroutine _coroutine;
    private void Continue()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
            dialogueText.text = _currentDialogue;
            return;
        }
        if (_dialogueQueue.Count > 0)
        {
            var dialogue = _dialogueQueue.Dequeue();
            speakerNameText.text = dialogue.speakerName;
            _currentDialogue = dialogue.dialogue;
            _coroutine = StartCoroutine(nameof(TypewriterEffect));
        }
        else
        {
            HideDialogue();
            _onDialogueStart = false;
        }
    }

    private string _currentDialogue;
    private IEnumerator TypewriterEffect()
    {
        for (int i = 0; i < _currentDialogue.Length; i++)
        {
            dialogueText.text = _currentDialogue.Substring(0,i+1);
            yield return new WaitForSeconds(0.01f);
        }
        _coroutine = null;
    }
}
