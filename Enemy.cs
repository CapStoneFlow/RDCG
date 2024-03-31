using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*각 enemy마다 추가될 스크립트임.
1. 적은 unitcode로 enemyStats에서 설정, public 변수로 외부에서 지정 가능
unitcode 수정 시 enemy변경이 가능함.
2. enemy는 턴 시작시 랜덤으로 공격 혹은 방어(현재 공격:방어 7:3 확률)
3. 행동력(스피드)개념: 스피드가 더 높으면 배틀 시작시 선제공격
4. 현재 player: enemy 1:1전투로 구현됨. 이후 추가 가능
5. 턴제-GM에서 턴 관리하는 대신 player의 턴종료button으로 enemy턴 시작*/
/*수정해야할것
1. enemy 방어량도 공격량과 동일하게 했는데(랜덤도 동일) 이후 수정해야할지
2. player 턴 종료 후 카드를 받아오는 방식(class? struct?)->script맡으신 분과 조율
3. player 스크립트 맡으신 분도 턴종료 시 호출되는 함수가 있을텐데 통일시켜야함
------------------개인------------------
4. 애니메이션(+FSM)추가
5. endbattle시 씬전환*/
public enum State{
    awake, playerTurn, enemyTurn, dead
}//player와 enemy 턴 구별, enemy 죽음 상태를 판별할 enum

public class Enemy : MonoBehaviour
{
    public enemyStats status;//적 스테이터스 클래스 객체변수
    public State state;//현재 턴 상태
    private int random;//공격:방어=7:3 매턴마다 난수
    public unitCode unitcode;//enemy 구별 코드-> 유니티에서 변경가능하도록 public.
    
    /*player변수들-매턴마다 선택하는 카드의 요소와 player의 상태를 대체할 변수
    매턴마다 복수의 카드가 선택됨->enemy에게 들어갈 데미지량의 총합*/
    public int playerSpeed=2;//행동력:상대보다 클 경우 시작시 선제공격
    
    public int playerAtk=3;//각 턴마다 받아오는 player 공격량
    
    public int playerHP=10;//player의 현재 hp
    public int playerDefendHP=0;//player의 별도 defendHP;
    private int enemyDefendHP=0;//enemy가 방어action시 누적되는 별도 hp로 player공격시 가장 먼저 깎임

    
    
    
    void Awake(){
        
        setEnemy(unitcode);//먼저 enemy 설정,state는 awake
        
        
    }
    void Start()
    {   
        BattleStart();//배틀시작, 선제공격 정하고 state를 awake에서 변경
        if(state==State.dead){
            EndBattle();}//enemy가 dead일경우 배틀을 끝냄
        
    }

    // Update is called once per frame
    void Update()
    {
        
      }
    void setEnemy(unitCode unitcode){
        
        this.status.setStatus(unitcode);
        this.state=State.awake;
        return;
    }//unitcode를 받아와서 enemy설정, public변수로 유니티내에서 설정가능
    void BattleStart(){
        
        if(playerSpeed>=status.Speed){
            state=State.playerTurn;
        }else{
            enemyAction();
        }
    }//player, enemy중 더 speed가 높은 쪽에 먼저 턴이 돌아감(state변경)
    

    
    void playerEndTurn(){
        if(state!=State.playerTurn){//playerTurn이 아니면 버튼이 눌리지 않음
            if(state==State.enemyTurn){
                Debug.Log("EnemyTurn입니다.");}//enemyTurn이면 안내창
        
        return;
        }
        
        enemyGetAtk();//player에게 받은 공격 합산
        Invoke("enemyAction",1);//enemy 공격/방어

    }//UI button에 연결할 함수. player의 턴종료, enemy턴 불러옴
    void enemyAction(){
       
        if(state==State.dead){
            EndBattle()};//enemy가 dead일경우 배틀을 끝냄
        state=State.enemyTurn;

        
        int Dmg=random.Range(status.minAtkDmg,status.maxAtkDmg);//데미지 랜덤
        if(random>2){
               
                Debug.Log("Enemy공격");
                
                
                playerDefendHP-=Dmg;
                playerHP+=playerDefendHP;
                }//적공격: player에게 hp피해
        else{Debug.Log("Enemy방어");
            }

        state=State.playerTurn;
    }
    void enemyGetAtk(){
        //n개의 카드에서 받아온 playerAtk을 여기서 합산-리스트나 배열로 받아와서 for문으로 합산
        int Dfnd=random.Range(status.minAtkDmg,status.maxAtkDmg);//방어량 랜덤
        random=Random.Range(0,9);
        if(random<=2){
            Debug.Log("Enemy방어");enemyDefendHP+=Dmg;}//30%확률로 방어,방어hp 추가


        if(enemyDefendHP>0){
            enemyDefendHP-=playerAtk;
            if(enemyDefendHP<0)status.nowHP+=enemyDefendHP;}
        else{status.nowHP-=playerAtk;}
            

        
        
        if(status.nowHP<=0){Dead();} 
        
        
        

    }//적 공격받음: player로부터 hp손실, hp=0이하일 경우 죽음
    
    void Dead(){
        if(this.state==State.dead){
            return;
        }

        state=State.dead;
        EndBattle();

    }//enemy 패배, 죽음
    
    
    void EndBattle(){
        Debug.Log("Enemy 패배, player승리");
    }//enemy죽고 전투 승리(이후 씬 전환 코드 추가)

    
   
    
}
