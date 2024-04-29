using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.Assertions.Must;

public class CardManager : MonoBehaviour
{
    public static CardManager Inst { get; private set; }

    void Awake() => Inst = this;

    //카드를 생성하기 위한 프리팹
    public GameObject cardPrefab;
    //카드를 생성할 스폰 포인트
   // public Transform cardspawnPoint;
    //두 트랜스폼은 아직 하는중이라 지금은 무시
    //public Transform myCardLeft;
   // public Transform myCardRight;
    //생성된 카드를 덱에 넣을 리스트
    public List<Card1> myCards;
    //player의 mp값을 가져오기 위한 변수
    public GameObject player;
    //카드를 생성할 프리팹
    GameObject cardObject;
    //내가 선택한 카드 정보 변수
    public Card1 selectCard;
    //카드를 마우로 클릭했을 떄 
    bool isCardDrag;
    //카드 영역
    bool onCardArea;
    //마우스의 위치
    public Vector3 MousePos;

    
    //플레이어의 마나
    //float myMP;




    /* void Start()
     { //액션 이벤트로 onaddcard에 addcard함수를 등록
         TurnManager1.OnAddCard += AddCard;

     OriginPos = new Vector3(card.gameObject.transform.position.x, card.gameObject.transform.position.y,
             card.gameObject.transform.position.z);

     }*/

    /*void OnDestroy()
    {//반대로 삭제할 떄 사용 일단은 사용 안함
        TurnManager1.OnAddCard -= AddCard;
    }*/

    void Update()
    {//플레이어 마나
       // myMP = player.GetComponent<Player1>().playerMP;
        //마우스 위치
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePos.z = -10;
        //카드영역에 있는지 확인
       // DetectCardArea();
        //카드를 마우스로 클릭 했을 때 카드 이동
      /*  if (isCardDrag)
        {
            cardDrag();
        }*/
        
    }

    //카드 영역에 없을 때 카드 이동
    /*private void cardDrag()
    {//카드영역에 없으면 선택 카드를 마우스 위치로 이동
        if (!onCardArea)
        {
            selectCard.MoveTransform(new PRS(MousePos, Quaternion.identity, selectCard.originPRS.Scale), false);

        }
    }*/
    //레이케스트를 쏴서 카드영역인지 아닌지 확인
    /*void DetectCardArea()
    {//레이케스트를 발사
        RaycastHit2D[] hits = Physics2D.RaycastAll(MousePos, Vector3.forward);
        int layer = LayerMask.NameToLayer("cardArea");
        //레이를 쏴서 맞은 오브젝트들 중 카드에리어이면 트루 아니면 폴스
        onCardArea = Array.Exists(hits, x => x.collider.gameObject.layer == layer);

    }*/

    //카드생성 함수
  /*  void AddCard()
    {//카드를 스폰 위치에 생성
        cardObject = Instantiate(cardPrefab, cardspawnPoint.position, Quaternion.identity);
        //Card 스크립트를 가져옴
        
        var card = cardObject.GetComponent<Card1>();
        //리스트에 카드 정보 넣기
        myCards.Add(card);



        cardAlignment();
    }//카드를 해당 위치로 이동*/

    //카드를 냈을 때 함수
   /* void AttackCard()
    {//마우스 클릭을 때고 카드영역 밖일 때 선택 카드를 삭제하고 마나를 줄이고 다시 카드 정렬
        if (!isCardDrag && !onCardArea)
        {
            //카드 리스트에서 선택 카드 제거
            myCards.Remove(selectCard);
            //두트윈 종료
            selectCard.transform.DOKill();
            //선택 카드 이름 디버그
            Debug.Log(selectCard.gameObject.GetComponent<CardInfo>().cardName);
            //내 마나 해당 코스트 만큼 빼줌
            myMP -= selectCard.gameObject.GetComponent<CardInfo>().cardCost;
            //플레이어로 정보 전달
            player.GetComponent<Player1>().playerMP = myMP;
            //정보 업데이트
            player.GetComponent<Player1>().UpdateState();

            //곧바로 삭제하기 위해
            DestroyImmediate(selectCard.gameObject);
            //삭제하면 미씽으로 남아서 오류가 나기 땜에
            selectCard = null;
            //카드 정렬
            cardAlignment();

        }
        
    }*/

  /*  void cardAlignment()
    {//카드들의 위치정보
        List<PRS> originCardPRSs = new List<PRS>();

        originCardPRSs = RoundAlignment(myCardLeft, myCardRight, myCards.Count, -0.5f, new Vector3(5f, 10f, 5f));


        //카드 리스트 수 만큼 반복
        for (int i = 0; i < myCards.Count; i++)
        {//해당 카드를 지정 위치로 이동
            var card = myCards[i];

            card.originPRS = originCardPRSs[i];
            card.MoveTransform(card.originPRS, true, 0.7f);
        }
    }*/
    //카드의 위치 변경하는 함수
   /* List<PRS> RoundAlignment(Transform left, Transform right, int count, float height, Vector3 scale)
    {//카드가 몇개인지를 위한 변수
        float[] objectLerps = new float[count];
        //카드 갯수 만큼 리스트 공간 확보
        List<PRS> results = new List<PRS>(count);
        //카드가 3개 까지는 지정한 위치로 이동 4개 이상으로는 갯수에 따라 간격 변경
        switch (count)
        {
            case 1: objectLerps = new float[] { 0.5f }; break;
            case 2: objectLerps = new float[] { 0.27f, 0.73f }; break;
            case 3: objectLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
            default:
                //카드 위치를 위해 위치 나누기
                float interval = 1f / (count - 1);
                //카드 위치를 배열에 저장
                for (int i = 0; i < count; i++)
                {
                    objectLerps[i] = interval * i;

                }

                break;

        }
        
        for (int i = 0; i < count; i++)
        {//정한 위치로 위치 저장
            var myPos = Vector3.Lerp(left.position, right.position, objectLerps[i]);
            var myRot = Quaternion.identity;

            //해당 위치로 이동
            results.Add(new PRS(myPos, myRot, scale));
        }

        return results;
    }*/
    //카드를 공격하지 않고 마우스를 땔 때 다시 원래 위치로 이동
    void EnlargeCard(bool isEnlarge,  Card1 card)
    {
        
        //마우스를 카드 위에 둘때 카드의 크기를 크게 만듬
        if (isEnlarge)
        {
            
            Vector3 enlargePos = new Vector3(card.gameObject.transform.position.x, -5f, -18f);
            card.MoveTransform(new PRS(enlargePos, Quaternion.identity, new Vector3(20f, 20f, 20f)), false);
            card.transform.position = enlargePos;
             

        }
        else
        {//아니면 원래 크기로 변함
            
            card.MoveTransform(card.originPRS, false);
        }

    }
    //카드 위로 마우스를 둘때
    public void CardMouseOver(Card1 card)
    {
        
        selectCard = card;

        EnlargeCard(true, selectCard);

        }
    



    //카드에서 마우스를 뺼 때
    public void CardMouseExit(Card1 card)
    {
        Debug.Log("Exit");
        //카드 크기를 원래대로
        EnlargeCard(false, selectCard);
    }
    //마우스를 누를 때
    public void CardMouseDown()
    {//드래그 할수 있게 하는 불 함수 트루로
        isCardDrag = true;
        
    }
    //아닐 때
   /* public void CardMouseUP()
    {//드래그 할수 있게 하는 불 함수 폴스로
       // isCardDrag= false;
        //현재 마나가 카드 코스트 보다 많을 때만 카드가 사라짐
        if (myMP >= selectCard.gameObject.GetComponent<CardInfo>().cardCost) 
        { 
            AttackCard();

        }
        


    }*/

}


