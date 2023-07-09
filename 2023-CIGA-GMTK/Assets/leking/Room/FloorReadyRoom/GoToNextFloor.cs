using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextFloor : MonoBehaviour
{
    private void OnMouseDown()
    {
        RoomManager.NextFloor();
    }
}
