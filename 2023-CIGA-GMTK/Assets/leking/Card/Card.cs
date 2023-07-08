using System;
using UnityEngine;

namespace GameplayTest.Scripts
{
    public class Card : MonoBehaviour
    {
        public bool isHighlight;
        public string describe = "This is Test Card";
        private Spells _currentSpell;
        protected Color _initColor;
        private void Start()
        {
            _initColor = GetComponent<SpriteRenderer>().color;
        }

        public void SetCardSpell(Spells spells)
        {
            _currentSpell = spells;
        }

        protected void Highlight()
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }

        private void LateUpdate()
        {
            if (isHighlight) Highlight();
            else GetComponent<SpriteRenderer>().color = _initColor;
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
                OnCardUsed();
                return true;
            }
            return false;
        }
    }
}
