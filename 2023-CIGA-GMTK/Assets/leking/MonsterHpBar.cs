using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterHpBar : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    private Vector3 _offset;
    private Monster _target;
    private Camera _camera;
    private RectTransform _rectTransform;

    private void Start()
    {
        _camera = Camera.main;
        _rectTransform = GetComponent<RectTransform>();
        
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }
        _rectTransform.position = _camera.WorldToScreenPoint(_target.transform.position + _offset);
        hpText.text = _target.GetHp().ToString("00.00");
    }

    public void TargetTo(Monster target,Vector3 offset)
    {
        _target = target;
        _offset = offset;
    }
}
