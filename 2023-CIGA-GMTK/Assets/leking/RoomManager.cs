using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    BattleRoom,
    NormalRoom,
    EncounterRoom
}

public class Room
{
    public RoomType type;
}
public class RoomManager : MonoBehaviour
{
    public List<BattleRoom> battleRoomsPool;
    public List<NormalRoom> normalRoomsPool;
    public List<EncounterRoom> encounterRoomsPool;
    private Room _currentRoom;
    private List<Door> _doors = new();

    public void InitRoom()
    {
        BattleManager.KillAllMonster();
    }

    public void ToRoom(RoomType roomType)
    {
        switch (roomType)
        {
            case RoomType.BattleRoom:
                break;
            case RoomType.NormalRoom:
                break;
            case RoomType.EncounterRoom:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(roomType), roomType, null);
        }
    }

    private void Start()
    {
        
    }
}
