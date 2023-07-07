using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillTreeUI : MonoBehaviour
{
    public GameObject test;
    private void Start()
    {
        Game.uiManager.ShowUI<DisplaySkillUI>("DisplaySkillUI");
    }
}
