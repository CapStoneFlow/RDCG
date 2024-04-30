using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1TurnManager : MonoBehaviour
{
    public Button turnBtn; // 턴종료 버튼 누르면 적의 턴으로 넘어가는 UI
    public int enemy1Turn; // 적의 턴이 몇번 진행되었는지 알 수 있는 변수

    public bool isPlayerTurn; // 현재 플레이어의 턴상태 확인
    public bool isEnemyTurn; // 현재 적의 턴상태 확인

    public Player player; // 플레이어 스크립트를 불러옴
    public Enemy enemy1; // 적 스크립트를 불러옴

    // Start is called before the first frame update
    void Start()
    {
        turnBtn.GetComponent<Button>();
        enemy1Turn = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy1.enemyHp <= 0) // 적의 체력이 0보다 작을 경우
        {
            enemy1.Stage1EnemyDeath(); // 적이 죽었다는 함수 실행
        }

        if (Player.playerHp <= 0) // 플레이어의 체력이 0보다 작을 경우
        {
            player.PlayerDead(); // 플레이어가 죽었다는 함수 실행
        }
    }
    /// <summary>
    /// 플레이어 행동을 다 끝내고
    /// 턴 종료 버튼을 눌렀을 경우 적 행동을 실행 시킬 때 비활성화 시키고
    /// 턴 종료 버튼을 다시 활성화
    /// 그 과정에서 플레이어 턴 일 경우 플레이어의 코스트를 매턴 마다 얻는 함수도 실행
    /// </summary>
    public void TurnButtonClick()
    {
        isPlayerTurn = false; // 현재 플레이어 턴을 종료
        isEnemyTurn = true; // 적 턴이 실행

        turnBtn.interactable = false; // 턴 종료 버튼을 비활성화
        enemy1Turn++; // 적의 턴 증가 (적의 체력 회복이나 강력한 공격 준비)
        Debug.Log(enemy1Turn);
        
        // 1스테이지는 단순히 적이 1 ~ 10까지 랜덤 데미지로 공격을 함
        enemy1.EnemyAttack(); // 적 스크립트에 있는 공격 함수 실행

        isPlayerTurn = true; // 플레이어 턴 시작
        isEnemyTurn = false; // 적 턴이 종료

        if (isPlayerTurn == true) // 만약 플레이어 턴이 시작 되면
        {
            player.PlayerGetCost(); // 플레이어의 코스트를 1씩 증가 (최대 코스트까지)
            player.PlayerTurnNewCardSpawn(); // 플레이어의 카드가 새롭게 생성
        }

        turnBtn.interactable = true; // 턴 종료 버튼이 다시 활성화 나중에 코루틴으로 동적 지연을 줄 예정
    }
}
