using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterBuffBar : MonoBehaviour
{
    private Vector3 _offset;
    private Monster _target;
    private Camera _camera;
    private RectTransform _rectTransform;
    private void Start()
    {
        _camera = Camera.main;
        _rectTransform = GetComponent<RectTransform>();
        BattleManager.buffChange += BattleManagerOnBuffChange;
    }

    private void BattleManagerOnBuffChange()
    {
        var childCount = transform.childCount;
        if (childCount > 0)
        {
            for (int i = 0; i < childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        foreach (var buff in _target.GetBuffs())
        {
            var o = Instantiate(Resources.Load<GameObject>(buff.type.ToString()),transform);
            o.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = buff.time.ToString();
            // switch (buff.type)
            // {
            //     case BuffType.Burn:
            //         var burn = Instantiate(Resources.Load<GameObject>("Burn"),transform);
            //         burn.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = buff.time.ToString();
            //         break;
            //     case BuffType.Fragile:
            //         var fragile = Instantiate(Resources.Load<GameObject>("Fragile"),transform);
            //         fragile.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = buff.time.ToString();
            //         break;
            //     case BuffType.Paralysis:
            //         var paralysis = Instantiate(Resources.Load<GameObject>("Paralysis"),transform);
            //         paralysis.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = buff.time.ToString();
            //         break;
            // }
        }
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }
        _rectTransform.position = _camera.WorldToScreenPoint(_target.transform.position + _offset);
    }
    public void TargetTo(Monster target,Vector3 offset)
    {
        _target = target;
        _offset = offset;
    }

    private void OnDestroy()
    {
        BattleManager.buffChange -= BattleManagerOnBuffChange;
    }
}
