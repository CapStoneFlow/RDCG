using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*이후 enemycomponent에 추가할 스크립트*/
public enum State{
    awake, playerTurn, enemyTurn, dead
}

public class Enemy : MonoBehaviour
{
    public Status status;
    public State state;
    public unitCode unitcode;
    public int random;//공격/방어(6:4) 랜덤으로 선택시 사용

    public int playerSpeed;//행동력:상대의 1.8배 이상일경우 한번 더 행동(아직 구현x),상대보다 클 경우 시작시 선제공격
    private bool hadTurn=false;//턴 중복체크용 변수
    public int playerAtk;//각 턴마다 받아오는 player 공격량
    
    public int playerHP;
    /*player변수들-이후 player 구현 클래스에서 변수 가져올 예정*/
    

    public Status setEnemy(unitCode unitcode){
        
        status = new Status();
        status = status.setStatus(unitcode);
        this.state=State.awake;
        return status;
    }
    public void enemyAtk(){
        
        playerHP-=status.atkDmg;
        state=State.playerTurn;
    }
    public void enemyGetAtk(){
        
        
        
        status.nowHP-=playerAtk; 
        if(status.nowHP<=0){Dead();}
        

    }
    public void enemyGuard(){
        if(playerAtk<status.atkDmg){
                Debug.Log("Enemy방어 성공");
                state=State.enemyTurn;
            }else{status.nowHP-=playerAtk;state=State.enemyTurn;
            Debug.Log("Enemy방어 실패");
            if(status.nowHP<=0){Dead();}}

    }
    public void Dead(){
        if(this.state==State.awake){
            return;
        }
        Debug.Log("Enemy 패배,전투종료");

        state=State.dead;

    }
    public void enemyTurn(){
        if(this.state==State.enemyTurn){
           return;
            
        }else{
            this.state=State.enemyTurn;
            random=Random.Range(0,9);
            if(random<=5){
                enemyGetAtk();
                Debug.Log("Enemy공격");
                enemyAtk();
                }
        else{Debug.Log("Enemy방어");
            enemyGuard();}  
        }
            }

    
    public void playerTurn(){
        if(this.state==State.playerTurn){
            return;
        }else{
            this.state=State.playerTurn;
            enemyGetAtk();

        }

    }
    void Awake(){
        status=setEnemy(unitcode);
        
    }
    void Start()
    {
        if(playerSpeed>=status.Speed) state=State.playerTurn;
        else state=State.enemyTurn;
        
    }

    // Update is called once per frame
    void Update()
    {
            if(state!=State.dead){
                Invoke("playerTurn",1f);
            Invoke("enemyTurn",1f);
            }
            
        

    }
}
