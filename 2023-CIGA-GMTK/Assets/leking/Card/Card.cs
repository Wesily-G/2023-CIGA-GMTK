using System;
using UnityEngine;

namespace GameplayTest.Scripts
{
    public class Card : MonoBehaviour
    {
        public bool isHighlight;
        public string describe = "This is Test Card";
        protected static int _testId;
        protected Color _initColor;
        private void Start()
        {
            describe = $"This is Test Card :{_testId}";
            _testId++;
            _initColor = GetComponent<SpriteRenderer>().color;
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

        //当卡牌被使用时调用
        public virtual void OnUseCard()
        {
            Destroy(gameObject);
        }
    }
}
