using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatesUI : MonoBehaviour
{
    public TextMeshProUGUI hpText;

    private void Update()
    {
        hpText.text = GameObject.FindWithTag("Player").GetComponent<Player>().GetHp().ToString("00.00");
    }

    public void ShowUI()
    {
        gameObject.SetActive(true);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }
}
