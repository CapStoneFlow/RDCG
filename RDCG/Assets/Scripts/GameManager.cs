using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    
    //������ ī�带 ���� ����
    private GameObject Card;
    
    //ī�� ���ý� �Լ�
    public void ClickCard()
    {//Ŭ���� ������Ʈ�� �̸��� string������ ����
        string cardName = EventSystem.current.currentSelectedGameObject.name;
        //������ ������ ī�� ����� �ȵǰ� ���� ���ٰ� �ȳ�â ��
        if (this.GetComponent<Player>().AP == 0)
        {
            Debug.Log("������ �����ϴ�!");
        }
        else
        {//������ �Ծ��ٴ� �����
        Debug.Log("������ 10�� ī�带 ����Ͽ����ϴ�.");
            this.GetComponent<Player>().ManaConsumption();
        //Ŭ���� ī�� �̸��� ������ ����
        Card = GameObject.Find(cardName);
        //���� ī�� ����
        Destroy(Card);

        }
    }
    //�� ���� �Լ�
    public void ClickEnd()
    {//�������� �Ծ��ٴ� �����
        Debug.Log("������ 10�� �Ծ����ϴ�");
        //���� �����Ͽ� �������� ���� �Լ�
        this.GetComponent<Player>().PlayerDamage();
        //���� ���� �� ���� ���� �Ǹ鼭 �� ���� �ͼ� ������ ��� �Լ�
        this.GetComponent<Player>().MyTurn();
        //������ �� UI������Ʈ
        this.GetComponent<Player>().UpdateState();

        

    }
    


}
