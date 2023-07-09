using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellNode : MonoBehaviour
{
    public string spellName;
    private Spells _spells;
    void Start()
    {
        _spells = SpellsManager.GetSpell(spellName);
        name = _spells.name;
        transform.Find("OkButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            SpellsManager.GetInstance().LearnSpell(_spells.name);
        });
    }
}
