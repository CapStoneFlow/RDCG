using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Card1 : MonoBehaviour
{
    //카드 위치 정보 변수
    public  PRS originPRS;


    private void Start()
    {
        originPRS = new PRS(this.gameObject.transform.position, Quaternion.identity, new Vector3(13f, 13f, 13f));
    }

    //두투윈을 이용해서 카드를 이동
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

   private void OnMouseOver()
    {//카드를 위에 마우스를 둘때 함수
        CardManager.Inst.CardMouseOver(this);
    }

    private void OnMouseExit()
    {//빠져나올 때 함수
        CardManager.Inst.CardMouseExit(this);
    }

   /* private void OnMouseDown()
    {//마우스를 누를 때
        CardManager.Inst.CardMouseDown();
    }

    private void OnMouseUp()
    {// 마우스를 땔 때
       // CardManager.Inst.CardMouseUP();
    }*/
}
