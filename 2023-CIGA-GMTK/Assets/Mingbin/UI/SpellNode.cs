using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellNode : MonoBehaviour
{
    public string spellName;
    [SerializeField]private Spells _spell;
    void Start()
    {
        _spell = SpellsManager.GetSpell(spellName);
        GetComponentInChildren<Text>().text = _spell.spellName;
        name = _spell.name;

        transform.GetComponent<Button>().onClick.RemoveAllListeners();
        transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            SkillTreeManager.GetInstance().DisplayInfoUI(_spell);
        });
    }
}
