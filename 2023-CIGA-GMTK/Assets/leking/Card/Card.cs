using System;
using TMPro;
using UnityEngine;

namespace GameplayTest.Scripts
{
    public class Card : MonoBehaviour
    {
        public bool isHighlight;
        public string describe = "This is Test Card";
        private Spells _currentSpell;
        protected SpriteRenderer _spriteRenderer;
        public TextMeshPro costText;
        public TextMeshPro nameText;
        public TextMeshPro describeText;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            costText = transform.Find("Cost").GetComponent<TextMeshPro>();
            nameText = transform.Find("Name").GetComponent<TextMeshPro>();
            describeText = transform.Find("Describe").GetComponent<TextMeshPro>();
        }

        public void SetCardSpell(Spells spells)
        {
            _currentSpell = spells;
            _spriteRenderer ??= GetComponent<SpriteRenderer>();
            costText ??= transform.Find("Cost").GetComponent<TextMeshPro>();
            nameText ??= transform.Find("Name").GetComponent<TextMeshPro>();
            describeText ??= transform.Find("Describe").GetComponent<TextMeshPro>();
            var sortingOrder = _spriteRenderer.sortingOrder+1;
            costText.sortingOrder = sortingOrder;
            nameText.sortingOrder = sortingOrder;
            describeText.sortingOrder = sortingOrder;
            costText.text = _currentSpell.cost.ToString();
            nameText.text = _currentSpell.Name.ToString();
            describeText.text = _currentSpell.spellDescription.ToString();
        }

        private void Update()
        {
            var sortingOrder = _spriteRenderer.sortingOrder+1;
            costText.sortingOrder = sortingOrder;
            nameText.sortingOrder = sortingOrder;
            describeText.sortingOrder = sortingOrder;
        }

        protected void Highlight()
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }

        private void LateUpdate()
        {
            if (isHighlight) Highlight();
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
            _currentSpell.OnCastByPlayer();
            Destroy(gameObject);
        }
        //当卡牌被使用时调用
        public bool UseCard()
        {
            var tempMagic = Mathf.Min(BattleManager.GetCost(),SpellsManager.GetMagicAmount());
            if (_currentSpell.cost <= tempMagic)
            {
                GetComponent<Animator>().enabled = true;
                return true;
            }
            return false;
        }
    }
}
