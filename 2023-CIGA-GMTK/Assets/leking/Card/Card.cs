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
            //硬编码的技能，实际应该动态获取
            var newCardObject = KGameObject.Instantiate(gameObject);
            newCardObject.transform.localScale = Vector3.one * 2.5f;
            newCardObject.transform.position = Vector3.zero;
            var newCard = newCardObject.GetComponent<Card>();
            newCard.gameObject.SetActive(false);
            Action skill = ()=>
            {
                BattleManager.AddFirstCommand(() =>
                {
                    //在这里添加技能添加的卡
                    newCard.gameObject.SetActive(true);
                    print( $"Get : newCard.name");
                    CardManager.AddCard(newCard);
                });
            };
            BattleManager.AddRoundCommand(skill);
            Destroy(gameObject);
        }
    }
}
