using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace leking
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instants;
        public Canvas canvas;
        public MonsterHpBar hpBarPrefab;
        public PlayerStatesUI playerStatesUI;
        public TextMeshProUGUI roundNumber;
        public TextMeshProUGUI messageText;
        public MonsterBuffBar monsterBuffBarPrefab;
        public Transform buffsTransform;

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

        public static void UpdatePlayerBuffUI(Player player)
        {
            var childCount = _instants.buffsTransform.childCount;
            if (childCount > 0)
            {
                for (int i = 0; i < childCount; i++)
                {
                    Destroy(_instants.buffsTransform.GetChild(i).gameObject);
                }
            }
            foreach (var buff in player.GetBuffs())
            {
                switch (buff.type)
                {
                    case BuffType.Burn:
                        var o = Instantiate(Resources.Load<GameObject>("Burn"),_instants.buffsTransform);
                        o.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = buff.time.ToString();
                        break;
                    case BuffType.Fragile:
                        var fragile = Instantiate(Resources.Load<GameObject>("Fragile"),_instants.buffsTransform);
                        fragile.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = buff.time.ToString();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        public static void AddMonsterBuffBar(Monster target,Vector3 offset)
        {
            var hpBar = Instantiate(_instants.monsterBuffBarPrefab, _instants.canvas.transform);
            hpBar.TargetTo(target,offset);
        }
        public static void ShowMessage(string message)
        {
            _instants.messageText.text = message;
        }
        public static void SetRoundNumber(int number)
        {
            _instants.roundNumber.text = number.ToString();
        }
        public static void AddMonsterHpBar(Monster target,Vector3 offset)
        {
            var hpBar = Instantiate(_instants.hpBarPrefab, _instants.canvas.transform);
            hpBar.TargetTo(target,offset);
        }

        public static void ShowPlayerStates()
        {
            _instants.playerStatesUI.ShowUI();
        }
        public static void HidePlayerStates()
        {
            _instants.playerStatesUI.HideUI();
        }
    }
}
