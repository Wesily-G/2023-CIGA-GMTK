using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private class Dialogue
    {
        public string speakerName;
        public string dialogue;
        public Action completeAction;
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

    private readonly Queue<Dialogue> _dialogueQueue = new();
    private bool _onDialogueStart;
    private Dialogue _currentDialogue;

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
        dialogueUI.SetActive(false);
    }

    #region 静态函数
    public static void AddDialogue(string speakerName, string dialogue)
    {
        if(_instants._onDialogueStart) return;
        _instants._dialogueQueue.Enqueue(new Dialogue(speakerName,dialogue));
    }

    private Action _firstAction;
    public static void AddAction(Action action)
    {
        if (_instants._dialogueQueue.Count <= 0)
        {
            _instants._firstAction = action;
        }
        else
        {
            _instants._dialogueQueue.Last().completeAction = action;
        }
    }
    public static void StartDialogue()
    {
        _instants._onDialogueStart = true;
        _instants.Continue();
        _instants.ShowDialogue();
    }
    

    #endregion

    private void ShowDialogue()
    {
        _firstAction?.Invoke();
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
            dialogueText.text = _currentDialogue.dialogue;
            return;
        }
        if (_dialogueQueue.Count > 0)
        {
            _currentDialogue = _dialogueQueue.Dequeue();
            speakerNameText.text = _currentDialogue.speakerName;
            _currentDialogue.completeAction?.Invoke();
            _coroutine = StartCoroutine(nameof(TypewriterEffect));
        }
        else
        {
            HideDialogue();
            _onDialogueStart = false;
        }
    }
    private IEnumerator TypewriterEffect()
    {
        for (int i = 0; i < _currentDialogue.dialogue.Length; i++)
        {
            dialogueText.text = _currentDialogue.dialogue[..(i+1)];
            yield return new WaitForSeconds(0.01f);
        }
        _coroutine = null;
    }
}
