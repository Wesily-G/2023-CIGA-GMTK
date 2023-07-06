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

    private void Start()
    {
        foreach (var monster in battleRoomsPool[0].monsters)
        {
            print(monster);
            BattleManager.AddMonster(monster);
        }
    }
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

    public static void NextRoom()
    {
        print("NextRoom!");
    }
}
