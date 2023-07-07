using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum RoomType
{
    BattleRoom,
    NormalRoom,
    EncounterRoom,
    TitleRoom
}

public class Room
{
    public RoomType type;
}
public class RoomManager : MonoBehaviour
{
    private static RoomManager _instants;
    public TitleRoom titleRoom;
    public List<BattleRoom> battleRoomsPool;
    public List<NormalRoom> normalRoomsPool;
    public List<EncounterRoom> encounterRoomsPool;
    private Room _currentRoom;
    private List<Door> _doors = new();
    private bool _readyNextRoom;
    private int _currentFloor;
    public GameObject roomRoot;
    public GameObject readRoomPrefab;
    public Vector3 rollRoomOffset;
    public Vector3 fadeRoomOffset;

    private RoomType _currentRoomType;
    
    private void Awake()
    {
        if (_instants == null)
        {
            _instants = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ToTitleRoom();
    }

    public void ToTitleRoom()
    {
        _currentRoomObject = Instantiate(titleRoom.roomPrefab, roomRoot.transform);
        _currentRoomType = RoomType.TitleRoom;
        _instants.RoomSwitchFade(_currentRoomObject);
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
    
    public static void OnBattleCompletion()
    {
        leking.UIManager.ShowNextRoomButton();
    }

    public static void ToReadyRoom()
    {
        leking.UIManager.HideNextRoomButton();
        var rr = Instantiate(_instants.readRoomPrefab,_instants.roomRoot.transform);
        _instants.RoomSwitchRoll(rr);
    }

    private GameObject _currentRoomObject;
    private GameObject _nextRoomObject;

    private void RoomSwitchRoll(GameObject targetRoom)
    {
        if(_currentRoomObject == null) return;
        _nextRoomObject = targetRoom;
        StartCoroutine(nameof(RoomSwitchRollCoroutine));
    }
    private void RoomSwitchFade(GameObject targetRoom)
    {
        if(_currentRoomObject == null||targetRoom == null) return;
        _nextRoomObject = targetRoom;
        StartCoroutine(nameof(RoomSwitchFadeCoroutine));
    }
    private IEnumerator RoomSwitchFadeCoroutine()
    {
        var nextRoomR = _nextRoomObject.GetComponent<SpriteRenderer>();
        var currRoomR = _currentRoomObject.GetComponent<SpriteRenderer>();
        Ulit.SetSpriteAlpha(ref nextRoomR,0);
        Ulit.SetSpriteAlpha(ref currRoomR,1);
        _nextRoomObject.transform.position -= fadeRoomOffset;
        var position = roomRoot.transform.position;
        var targetPosNext = position;
        var targetPosCurr = position + fadeRoomOffset;
        for (float i = 0; i <= 1; i += 0.001f)
        {
            Ulit.SetSpriteAlpha(ref nextRoomR,i);
            Ulit.SetSpriteAlpha(ref currRoomR,1-i);
            _currentRoomObject.transform.position =
                Vector3.Lerp(_currentRoomObject.transform.position, targetPosCurr, i);
            _nextRoomObject.transform.position =
                Vector3.Lerp(_nextRoomObject.transform.position, targetPosNext, i);
            yield return new WaitForSeconds(1 / 100f);
        }
        Destroy(_currentRoomObject);
    }
    private IEnumerator RoomSwitchRollCoroutine()
    {
        _nextRoomObject.transform.position -= rollRoomOffset;
        var position = roomRoot.transform.position;
        var targetPosNext = position;
        var targetPosCurr = position + rollRoomOffset;
        for (float i = 0; i <= 1; i += 0.001f)
        {
            _currentRoomObject.transform.position =
                Vector3.Lerp(_currentRoomObject.transform.position, targetPosCurr, i);
            _nextRoomObject.transform.position =
                Vector3.Lerp(_nextRoomObject.transform.position, targetPosNext, i);
            yield return new WaitForSeconds(1 / 100f);
        }
        Destroy(_currentRoomObject);
    }
}
