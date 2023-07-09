using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPItem : MonoBehaviour
{
    private void OnMouseDown()
    {
        SpellsManager.AddMagicAmount(30);
        RoomManager.ReadyNextRoom();
        Destroy(gameObject);
    }
}
