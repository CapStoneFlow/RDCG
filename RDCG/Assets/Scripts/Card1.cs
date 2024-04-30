using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Card1 : MonoBehaviour
{
    //ī�� ��ġ ���� ����
    public  PRS originPRS;


    private void Start()
    {
        originPRS = new PRS(this.gameObject.transform.position, Quaternion.identity, new Vector3(13f, 13f, 13f));
    }

    //�������� �̿��ؼ� ī�带 �̵�
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
    {//ī�带 ���� ���콺�� �Ѷ� �Լ�
        CardManager.Inst.CardMouseOver(this);
    }

    private void OnMouseExit()
    {//�������� �� �Լ�
        CardManager.Inst.CardMouseExit(this);
    }

   /* private void OnMouseDown()
    {//���콺�� ���� ��
        CardManager.Inst.CardMouseDown();
    }

    private void OnMouseUp()
    {// ���콺�� �� ��
       // CardManager.Inst.CardMouseUP();
    }*/
}
