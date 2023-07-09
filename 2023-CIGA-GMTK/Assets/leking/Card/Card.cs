using System;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameplayTest.Scripts
{
    public class Card : MonoBehaviour
    {
        public bool isHighlight;
        public string describe = "This is Test Card";
        private Spells _currentSpell;
        public SpriteRenderer spriteRenderer;
        public TextMeshPro costText;
        public TextMeshPro nameText;
        public TextMeshPro describeText;
        public Animator animator;
        public Material fire;
        public Material water;
        public Material lighting;

        private void Start()
        {
            
        }

        public void SetCardSpell(Spells spells)
        {
            _currentSpell = spells;
            spriteRenderer ??= GetComponent<SpriteRenderer>();
            costText ??= transform.Find("Cost").GetComponent<TextMeshPro>();
            nameText ??= transform.Find("Name").GetComponent<TextMeshPro>();
            describeText ??= transform.Find("Describe").GetComponent<TextMeshPro>();
            if (_currentSpell.cardSprite != null)
            {
                spriteRenderer.sprite = _currentSpell.cardSprite;
            }
            var sortingOrder = spriteRenderer.sortingOrder+1;
            costText.sortingOrder = sortingOrder;
            nameText.sortingOrder = sortingOrder;
            describeText.sortingOrder = sortingOrder;
            costText.text = _currentSpell.cost.ToString();
            nameText.text = _currentSpell.spellName.ToString();
            describeText.text = _currentSpell.spellDescription.ToString();
        }

        private void Update()
        {
            var sortingOrder = spriteRenderer.sortingOrder+1;
            costText.sortingOrder = sortingOrder;
            nameText.sortingOrder = sortingOrder;
            describeText.sortingOrder = sortingOrder;
        }
        protected void Highlight()
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }

        private bool isMouseOver;
        private void OnMouseEnter()
        {
            isMouseOver = true;
        }

        private void OnMouseExit()
        {
            isMouseOver = false;
        }

        private void LateUpdate()
        {
            if (isHighlight && isMouseOver) Highlight();
            if (!isMouseOver && !isHighlight)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            isHighlight = false;
        }

        public void OnCardUsed()
        {
            if (_currentSpell.isFixed)
            {
                BattleManager.AddFirstCommand(() =>
                {
                    CardManager.AddCardFromSpell(_currentSpell);
                });
            }
            else
            {
                SpellsManager.GetInstance().RemoveSpell(_currentSpell.name);
            }

            switch (_currentSpell.elementType)
            {
                case ElementTypes.Fire:
                    AudioManager.PlayClip("Fire");
                    break;
                case ElementTypes.Water:
                    AudioManager.PlayClip("Water");
                    break;
                case ElementTypes.Lighting:
                    AudioManager.PlayClip("Thunder");
                    break;
            }
            _currentSpell.OnCastByPlayer();
            Destroy(gameObject);
        }
        //当卡牌被使用时调用
        public bool UseCard()
        {
            var tempMagic = Mathf.Min(BattleManager.GetCost(),SpellsManager.GetMagicAmount());
            if (_currentSpell.cost <= tempMagic)
            {
                print(123);
                switch (_currentSpell.elementType)
                {
                    case ElementTypes.Fire:
                        spriteRenderer.material = fire;
                        animator.SetBool("isFire",true);
                        break;
                    case ElementTypes.Water:
                        spriteRenderer.material = water;
                        animator.SetBool("isWater",true);
                        break;
                    case ElementTypes.Lighting:
                        spriteRenderer.material = lighting;
                        animator.SetBool("isRaiden",true);
                        break;
                }
                AudioManager.PlayClip("Atk");
                return true;
            }
            return false;
        }
    }
}
