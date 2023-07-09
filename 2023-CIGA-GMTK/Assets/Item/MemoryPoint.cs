using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPoint : MonoBehaviour
{
    public int memoryValue;
    private void OnMouseDown()
    {
        SpellsManager.GetInstance().currentMemory = memoryValue;
        Destroy(gameObject);
    }
}
