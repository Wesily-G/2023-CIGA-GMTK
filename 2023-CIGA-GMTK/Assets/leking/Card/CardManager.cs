using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace GameplayTest.Scripts
{
    public class CardManager : MonoBehaviour
    {
        private static CardManager _instants;
        public List<GameObject> cards;
        public Transform cardStart;
        public Transform cardEnd;
        public Transform center;
        public LayerMask cardLayerMask;
        public Transform hidePos;
        public Transform showPos;
        public TextMeshProUGUI cardDescribe;
        public Transform divider;
        public GameObject cardPrefab;
        private readonly List<Card> _currentCardList = new ();
        private bool _activeDelayActions;
        private readonly Queue<Action> _delayActions = new();
        public bool actionable;
        private Card _currentShowCard;
        private Card _currentDragCard;
        private bool _onDrag;
        private Camera _camera;

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
            _camera = Camera.main;
            //初始化卡牌
            foreach (var c in cards.Select(KGameObject.Instantiate))
            {
                _currentCardList.Add(c.AddComponent<Card>());
                c.SetActive(false);
            }

            HideCardHard();
            AddCardFromSpell(SpellsManager.GetSpell("Fireball"));
            AddCardFromSpell(SpellsManager.GetSpell("IceLance"));
            AddCardFromSpell(SpellsManager.GetSpell("ThunderboltJudgement"));
            //获取主摄像头
            _camera = Camera.main;
            
            //显示并刷新卡牌坐标
            UpdateCardsPosHard();
        }

        private Vector3 _offset;
        private Vector3 _initScale = new Vector3(2.5f, 2.5f, 2.5f);
        private void Update()
        {
            if (_currentShowCard is not null)
            {
                cardDescribe.text = _currentShowCard.describe;
            }
            else
            {
                cardDescribe.text = "";
            }
            //执行延迟函数
            if (_activeDelayActions)
            {
                _delayActions.Dequeue()();
            }
            _activeDelayActions = _delayActions.Count > 0;
            
            if(Input.GetMouseButtonUp(0))
            {
                _onDrag = false;
                if (_currentDragCard != null)
                {
                    _currentDragCard.GetComponent<KGameObject>().ScaleTo(_initScale);
                    if (divider.position.y < _camera.ScreenToWorldPoint(Input.mousePosition).y)
                    {
                        if (_currentDragCard.UseCard())
                        {
                            _currentCardList.Remove(_currentDragCard);
                        }
                    }
                    UpdateCardsPos();
                    _currentDragCard = null;
                }
                
            }

            if (_onDrag)
            {
                if (_currentDragCard is not null)
                {
                    var movePos = _camera.ScreenToWorldPoint(Input.mousePosition);
                    movePos.z = 0;
                    _currentDragCard.transform.position = movePos;
                }
            }
            else
            {
                GetTopCollider2D(TopColliderAction);
            }
        }

        private void TopColliderAction(Collider2D topCollider)
        {
            var card = topCollider.GetComponent<Card>();
            if (actionable && card != _currentDragCard)
            {
                //高亮
                card.isHighlight = true;
                _currentShowCard = card;
                if (Input.GetMouseButtonDown(0))
                {
                    _onDrag = true;
                    _offset = topCollider.transform.position - _camera.ScreenToWorldPoint(Input.mousePosition);
                    _currentDragCard = card;
                    _currentDragCard.GetComponent<KGameObject>().ScaleTo(_initScale + 0.5f * Vector3.one);
                    _currentShowCard = null;
                    //_currentCardList.Remove(card);
                    card.GetComponent<SpriteRenderer>().sortingOrder = _currentCardList.Count;
                }
            }
        }
        
        private bool GetTopCollider2D(Action<Collider2D> action = null)
        {
            //获取碰撞信息
            var results = new RaycastHit2D[10];
            var size = Physics2D.RaycastNonAlloc(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,results,cardLayerMask);
            var nResults = new RaycastHit2D[size];
            
            //清除空的RaycastHit2D
            for (int i = 0; i < size; i++)
            {
                nResults[i] = results[i];
            }
            if (size > 0)
            {
                //获取最上层的collider2D
                var collider2Ds = 
                    from result in nResults
                    orderby result.collider.GetComponent<SpriteRenderer>().sortingOrder descending 
                    select result.collider;
                var topCollider = collider2Ds.ToArray()[0];
                action?.Invoke(topCollider);
                return true;
            }

            return false;
        }
        public void UpdateCardsPos()
        {
            //获取中心坐标
            var centerPos = center.position;
            for (var i = 0; i<_currentCardList.Count; i++)
            {
                if (_currentCardList[i] == null)
                {
                    _currentCardList.RemoveAt(i);
                    i--;
                    continue;
                }
                var c = _currentCardList[i].GetComponent<KGameObject>();
                //显示卡牌
                c.SetActive(true);
                //设置卡牌的排序层
                c.GetComponent<SpriteRenderer>().sortingOrder = i;
                //对卡牌位置进行球面插值
                var t = _currentCardList.Count - 1>0?i / (float)(_currentCardList.Count - 1):0;
                var sp = cardStart.position;
                var ep = cardEnd.position;
                if (_currentCardList.Count < 15)
                {
                    var halfPos = (1 / 2f) * (ep - sp);
                    var offsetPos = ((15 -_currentCardList.Count)/15f)*halfPos;
                    sp += offsetPos;
                    ep -= offsetPos;
                }
                var newPos = Vector3.Slerp(sp-centerPos, ep-centerPos, t)+centerPos;
                //移动卡牌到新位置
                c.MoveTo(newPos);
                //旋转卡牌到新角度
                var newRot = new Vector3(0, 0, Vector3.Angle(newPos - centerPos, Vector3.right) - 90);
                c.RotateTo(newRot);
            }
            
        }
        //硬移动
        private void UpdateCardsPosHard()
        {
            //获取中心坐标
            var centerPos = center.position;
            for (var i = 0; i<_currentCardList.Count; i++)
            {
                if (_currentCardList[i] == null)
                {
                    _currentCardList.RemoveAt(i);
                    i--;
                    continue;
                }
                var c = _currentCardList[i].GetComponent<KGameObject>();
                //显示卡牌
                c.SetActive(true);
                //设置卡牌的排序层
                c.GetComponent<SpriteRenderer>().sortingOrder = i;
                //对卡牌位置进行球面插值
                var t = _currentCardList.Count - 1>0?i / (float)(_currentCardList.Count - 1):0;
                var sp = cardStart.position;
                var ep = cardEnd.position;
                if (_currentCardList.Count < 15)
                {
                    var halfPos = (1 / 2f) * (ep - sp);
                    var offsetPos = ((15 -_currentCardList.Count)/15f)*halfPos;
                    sp += offsetPos;
                    ep -= offsetPos;
                }
                var newPos = Vector3.Slerp(sp-centerPos, ep-centerPos, t)+centerPos;
                //移动卡牌到新位置
                c.transform.position = newPos;
                //旋转卡牌到新角度
                c.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Vector3.Angle(newPos - centerPos, Vector3.right) - 90));
            }
        }

        public static void ShowCard()
        {
            _instants.SetCardToShowPos();
        }
        public static void HideCard()
        {
            _instants.SetCardToHidePos();
        }
        public void SetCardToShowPos()
        {
            var sp = cardStart.position;
            var ep = cardEnd.position;
            var position = showPos.position;
            sp.y = position.y;
            ep.y = position.y;
            cardStart.position = sp;
            cardEnd.position = ep;
            UpdateCardsPos();
        }
        
        public void SetCardToHidePos()
        {
            var sp = cardStart.position;
            var ep = cardEnd.position;
            var position = hidePos.position;
            sp.y = position.y;
            ep.y = position.y;
            cardStart.position = sp;
            cardEnd.position = ep;
            UpdateCardsPos();
        }
        public void HideCardHard()
        {
            var sp = cardStart.position;
            var ep = cardEnd.position;
            var position = hidePos.position;
            sp.y = position.y;
            ep.y = position.y;
            cardStart.position = sp;
            cardEnd.position = ep;
            UpdateCardsPosHard();
        }
        
        //测试用函数
        #region DEBUG

        public void SwitchState()
        {
            actionable = !actionable;
        }

        public static void AddCard(Card card)
        {
            _instants._currentCardList.Add(card);
            _instants.UpdateCardsPos();
        }

        public static void AddCardFromSpell(Spells spell)
        {
            var card = KGameObject.Instantiate(_instants.cardPrefab).AddComponent<Card>();
            card.SetCardSpell(spell);
            card.describe = spell.spellDescription;
            _instants._currentCardList.Add(card);
            _instants.UpdateCardsPos();
        }
        public static void RemoveAllCard()
        {
            
        }
        public void AddCard()
        {
            var c =  KGameObject.Instantiate(cards[0]);
            //c.SetActive(false);
            _currentCardList.Add(c.AddComponent<Card>());
            UpdateCardsPos();
        }
        
        public void RemoveCard()
        {
            if(_currentCardList.Count <= 0) return;
            Destroy(_currentCardList[^1].gameObject);
            _currentCardList.Remove(_currentCardList[^1]);
            UpdateCardsPos();
        }
        #endregion

    }
}
