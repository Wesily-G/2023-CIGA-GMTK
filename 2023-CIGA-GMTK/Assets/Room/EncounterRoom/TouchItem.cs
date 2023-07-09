using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_TestRoom : MonoBehaviour
{
    private void OnMouseDown()
    {
        RoomManager.ReadyNextRoom();
        Destroy(gameObject);
    }
}
