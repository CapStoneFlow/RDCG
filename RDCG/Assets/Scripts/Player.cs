using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    //ĳ���� ���ÿ� ���� ����
    private bool Male = false;
    private bool Female = false;
    //�÷��̾��� ü��
    private float HP = 100;
    //�� ���� ��� ����?
    public float AP = 1;
    //ī�尡 ������ �⺻ ���ݷ� ��ŭ�� ����ī�带 �ٶ��� ���� ����
    private float AD;
    //HP�� ���� UI �ؽ�Ʈ
    public Text stateText;



    // Start is called before the first frame update
    void Start()
    {//UI ������Ʈ
        UpdateState();

        //ĳ���Ϳ� ���� �⺻ ����
        if (Male)
        {
            AD = 20;
            AP = 0;
        }
        else if (Female)
        {
            AD = 10;
            AP = 1;
        }

    }
    // ��ī�带 ���� �� ü�¿� ���� ���
    void PlayerHeal()
    {
        if (HP < 100)
            {
                //���Ƿ� 10ü��ȸ��
                HP += 10;
            }
            else
            {
                //ü���� Ǯ�� ��� ���� �ȵ�
                Debug.Log("�÷��̾��� ü���� �ִ��Դϴ�.");
            }
    }


    //�÷��̾ ���� ������ ��
    public void PlayerDamage()
    {//���Ƿ� �� ������ 10���� ����
        HP -= 10;
        //��� - ���ӸŴ����� �Űܵ� �ɼ���...?
        if(HP < 0 || HP == 0)
        {
            Debug.Log("!!!!!GameOver!!!!!");
            SceneManager.LoadScene("SampleScene");
            //���� ȭ�� �̳� �������� ���� ȭ������ �̵�
        }
    }
    //���� �� �϶� ������ �ϳ� ��´�
    public void MyTurn()
    {
        
        AP += 1;
    }
    // �ǿ� ������ �ؽ�Ʈ�� ���� �Լ�
    public void UpdateState()
    {
        stateText.text = "HP\n" + HP + "\nmana\n" + AP;
    }
    //ī�� ���� ���� �Ҹ�
    public void ManaConsumption()
    {
        
            AP -= 1;
            UpdateState();

        
        
    }




}
