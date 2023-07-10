using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int roomIndex;
    public Sprite battleRoomSprite;
    public Sprite encounterRoomSprite;
    public Sprite bossRoomSprite;

    public RoomType type;

    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case RoomType.BattleRoom:
                _spriteRenderer.sprite = battleRoomSprite;
                break;
            case RoomType.NormalRoom:
                break;
            case RoomType.EncounterRoom:
                _spriteRenderer.sprite = encounterRoomSprite;
                break;
            case RoomType.BossRoom:
                _spriteRenderer.color = Color.black;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnMouseDown()
    {
        RoomManager.NextRoom(roomIndex);
    }
}
