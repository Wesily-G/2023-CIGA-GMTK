using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillTreeUI : MonoBehaviour
{
    public Button closeBtn;
    public GameObject displaySkillUI;
    private void Start()
    {
        if(closeBtn == null) Debug.LogError("缺少CloseBtn");
        closeBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            if(displaySkillUI.activeSelf) displaySkillUI.SetActive(false);
        });
    }
}
