using System;
using UnityEngine;

namespace GameplayTest.Scripts
{
    public class Card : MonoBehaviour
    {
        public bool isHighlight;
        public string describe = "This is Test Card";
        private static int _testId;
        private void Start()
        {
            describe = $"This is Test Card :{_testId}";
            _testId++;
        }

        private void Highlight()
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }

        private void LateUpdate()
        {
            if (isHighlight) Highlight();
            else GetComponent<SpriteRenderer>().color = Color.white;
            isHighlight = false;
        }

        //当卡牌被使用时调用
        public void OnUseCard()
        {
            Destroy(gameObject);
        }
    }
}
