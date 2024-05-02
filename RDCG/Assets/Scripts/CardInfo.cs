using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardInfo : MonoBehaviour
{
    // 카드의 속성을 설정할 Card 컴포넌트
    private Card cardComponent;
    // 카드의 이름
    public string cardName;
    // 카드의 코스트
    public int cardCost;
    // 카드의 값
    public int cardValue;
    // 카드의 내용
    public string cardContent;
    // 카드의 특수효과
    public int cardEffect;
    //카드의 위치정보
    public PRS originPRS;
    //카드 이름및 공격력과 코스트 정보
    public TMP_Text nameTMP;
    public TMP_Text attackTMP;
    public TMP_Text costTMP;
    public TMP_Text contentTMP;

    // Start is called before the first frame update
    void Start()
    {
        // Card 클래스의 생성자를 사용하여 카드 생성 및 속성 설정
        Card cardComponent = new Card(cardName, cardCost, cardValue, cardContent);
        //카드가 생성 될떄 처음 위치 정보를 저장
        originPRS = new PRS(this.gameObject.transform.position, Quaternion.identity, new Vector3(2f, 2f, 2f));
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //카드 이름및 코스트 공격력 세팅
    public void Setup()
    {
        nameTMP.text = cardName;
        attackTMP.text = cardValue.ToString();
        costTMP.text = cardCost.ToString();
        contentTMP.text = cardContent;
    }
    //매개변수 위치로 이동하는 함수
    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0f)
    {
        if (useDotween)
        {

            transform.DOMove(prs.Pos, dotweenTime);
            transform.DORotateQuaternion(prs.Rot, dotweenTime);
            transform.DOScale(prs.Scale, dotweenTime);
        }
        else
        {
            transform.position = prs.Pos;
            transform.rotation = prs.Rot;
            transform.localScale = prs.Scale;
        }

    }
    //카드 위로 마우스가 올라올 때 발동 되는 함수
    private void OnMouseOver()
    {
        CardManager.Inst.CardMouseOver(this);
    }
    //카드에서 마우스가 나올 때 발동 되는 함수
    private void OnMouseExit()
    {
        CardManager.Inst.CardMouseExit(this);
    }
}
