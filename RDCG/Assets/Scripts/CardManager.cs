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


    //마우스를 올렸을 때 카드 정보
    public CardInfo selectCard;
    //마우스 위치 정보
    public Vector3 MousePos;

    void Update()
    {
        //마우스 위치
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePos.z = -10;


    }


    //카드를 공격하지 않고 마우스를 땔 때 다시 원래 위치로 이동
    void EnlargeCard(bool isEnlarge, CardInfo card)
    {

        //마우스를 카드 위에 둘때 카드의 크기를 크게 만듬

        if (isEnlarge)
        {
            //해당 위치로 카드 이동
            Vector3 enlargePos = new Vector3(card.gameObject.transform.position.x, -5f, -18f);
            card.MoveTransform(new PRS(enlargePos, Quaternion.identity, new Vector3(20f, 20f, 20f)), false);
            card.transform.position = enlargePos;


        }
        else

        {

            {//아니면 원래 크기로 변함


                card.MoveTransform(card.originPRS, false);
            }

        }
    }
    //카드 위로 마우스를 둘때 함수
    public void CardMouseOver(CardInfo card)
    {



        selectCard = card;

        EnlargeCard(true, selectCard);

    }


    //마우스가 카드에서 벗어날 때 함수
    public void CardMouseExit(CardInfo card)
    {

        EnlargeCard(false, selectCard);
    }
}

    

