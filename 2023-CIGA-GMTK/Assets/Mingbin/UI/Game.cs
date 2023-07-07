using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static UIManager uiManager;
    public static bool isLoad = false;
    private void Awake()
    {
        if (isLoad == true)
        {
            Destroy(gameObject);
        }
        else
        {
            isLoad = true;
            DontDestroyOnLoad(gameObject);
            uiManager = new UIManager();
            Game.uiManager.Init();
        }

        
    }
}
