using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace leking
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instants;
        public Canvas canvas;
        public MonsterHpBar hpBarPrefab;
        public PlayerStatesUI playerStatesUI;
        public TextMeshProUGUI roundNumber;
        public GameObject roundIcon;
        public TextMeshProUGUI messageText;
        public MonsterBuffBar monsterBuffBarPrefab;
        public GameObject toReadyRoomButton;
        public Transform buffsTransform;
        public GameObject titleUI;
        public TextMeshProUGUI stepNumber;
        public TextMeshProUGUI floorNumber;
        public TextMeshProUGUI magicAmount;
        public TextMeshProUGUI cost;
        public GameObject skillTree;
        public Button nextRoundButton;
        [SerializeField] private List<GameObject> uiList;
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
            if (toReadyRoomButton.TryGetComponent(out Button button))
            {
                button.onClick.AddListener(RoomManager.ToReadyRoom);
            }
            skillTree.SetActive(false);
        }

        private void Update()
        {
            stepNumber.text = RoomManager.GetStepNumber().ToString();
            floorNumber.text = RoomManager.GetFloorNumber().ToString();
            magicAmount.text = SpellsManager.GetMagicAmount().ToString();
            cost.text = BattleManager.GetCost().ToString();
        }

        public static void HideCanvas()
        {
            _instants.canvas.gameObject.SetActive(false);
        }
        public static void ShowCanvas()
        {
            _instants.canvas.gameObject.SetActive(true);
        }
        public static void ShowTitleUI()
        {
            _instants.titleUI.SetActive(true);
        }
        public static void HideTitleUI()
        {
            _instants.titleUI.SetActive(false);
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
                var lo = Resources.Load<GameObject>(buff.type.ToString());
                if(lo == null) continue;
                var o = Instantiate(lo,_instants.buffsTransform);
                o.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = buff.time.ToString();
                // switch (buff.type)
                // {
                //     case BuffType.Burn:
                //         
                //         break;
                //     case BuffType.Fragile:
                //         var fragile = Instantiate(Resources.Load<GameObject>("Fragile"),_instants.buffsTransform);
                //         fragile.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = buff.time.ToString();
                //         break;
                //     case BuffType.Paralysis:
                //         var paralysis = Instantiate(Resources.Load<GameObject>("Paralysis"),_instants.buffsTransform);
                //         paralysis.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = buff.time.ToString();
                //         break;
                //     case BuffType.Invincible:
                //         var paralysis = Instantiate(Resources.Load<GameObject>("Paralysis"),_instants.buffsTransform);
                //         paralysis.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = buff.time.ToString();
                //         break;
                //     case BuffType.CriticalStrike:
                //         var paralysis = Instantiate(Resources.Load<GameObject>("Paralysis"),_instants.buffsTransform);
                //         paralysis.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = buff.time.ToString();
                //         break;
                //     case BuffType.DoubleCast:
                //         var paralysis = Instantiate(Resources.Load<GameObject>("Paralysis"),_instants.buffsTransform);
                //         paralysis.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = buff.time.ToString();
                //         break;
                //     case BuffType.ExplosiveInjury:
                //         var paralysis = Instantiate(Resources.Load<GameObject>("Paralysis"),_instants.buffsTransform);
                //         paralysis.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = buff.time.ToString();
                //         break;
                //     case BuffType.FireResistance:
                //         break;
                //     case BuffType.WaterResistance:
                //         break;
                //     case BuffType.LightingResistance:
                //         break;
                //     case BuffType.IncreasedInjury:
                //         break;
                // }
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
        public static void ShowNextRoomButton()
        {
            _instants.toReadyRoomButton.SetActive(true);
        }
        public static void HideNextRoomButton()
        {
            _instants.toReadyRoomButton.SetActive(false);
        }
        public static void ShowSkillTree()
        {
            _instants.skillTree.SetActive(true);
        }
        public static void HideSkillTree()
        {
            _instants.skillTree.SetActive(false);
        }

        public static void ShowRoundNumber()
        {
            _instants.roundIcon.gameObject.SetActive(true);
        }
        public static void HideRoundNumber()
        {
            _instants.roundIcon.gameObject.SetActive(false);
        }
        public static void ShowNextRoundButton()
        {
            _instants.nextRoundButton.gameObject.SetActive(true);
        }
        public static void HideNextRoundButton()
        {
            _instants.nextRoundButton.gameObject.SetActive(false);
        }
        public static void NextRoundButtonInteractable(bool interactable)
        {
            _instants.nextRoundButton.interactable = interactable;
        }
    }
}
