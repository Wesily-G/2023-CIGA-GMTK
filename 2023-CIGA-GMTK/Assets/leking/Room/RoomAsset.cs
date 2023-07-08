using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAsset : ScriptableObject
{
    public RoomType roomType;
    public string roomName;
    public string describe;
    public GameObject roomPrefab;
    public int floor;
}
