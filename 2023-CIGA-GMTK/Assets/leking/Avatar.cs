using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour,IHealth
{
    private float _hp;
    public ElementTypes type;
    public Action onDead;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        switch (type)
        {
            case ElementTypes.Fire:
                _spriteRenderer.color = Color.red;
                break;
            case ElementTypes.Water:
                _spriteRenderer.color = Color.blue;
                break;
            case ElementTypes.Lighting:
                break;
        }
    }

    public void SetHp(float hp)
    {
        _hp = hp;
    }
    public float GetHp()
    {
        return _hp;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public float Attack(float damage)
    {
        if (_hp - damage <= 0)
        {
            onDead();
            Destroy(gameObject);
        }

        _hp -= damage;
        return damage;
    }

    public float Attack(float damage, ElementTypes type)
    {
        if (_hp - damage <= 0)
        {
            onDead();
            Destroy(gameObject);
        }

        _hp -= damage;
        return damage;
    }
}
