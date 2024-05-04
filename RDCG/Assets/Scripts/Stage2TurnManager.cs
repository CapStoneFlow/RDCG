using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage2TurnManager : MonoBehaviour
{
    public Button turnBtn2; // 턴종료 버튼 누르면 적의 턴으로 넘어가는 UI
    public int enemy2Turn; // 적의 턴이 몇번 진행되었는지 알 수 있는 변수

    public bool isPlayer2Turn; // 현재 플레이어의 턴상태 확인
    public bool isEnemy2Turn; // 현재 적의 턴상태 확인

    public Player player2; // 플레이어 스크립트를 불러옴
    public Enemy enemy2; // 적 스크립트를 불러옴

    // Start is called before the first frame update
    void Start()
    {
        turnBtn2.GetComponent<Button>();
        enemy2Turn = 1;
        Player.playerGainGold = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy2.enemyHp <= 0) // 적의 체력이 0보다 작을 경우
        {
            enemy2.Stage2EnemyDeath(); // 적이 죽었다는 함수 실행
        }

        if (Player.playerHp <= 0) // 플레이어의 체력이 0보다 작을 경우
        {
            player2.PlayerDead(); // 플레이어가 죽었다는 함수 실행
        }
    }
    /// <summary>
    /// 플레이어 행동을 다 끝내고
    /// 턴 종료 버튼을 눌렀을 경우 적 행동을 실행 시킬 때 비활성화 시키고
    /// 턴 종료 버튼을 다시 활성화
    /// 그 과정에서 플레이어 턴 일 경우 플레이어의 코스트를 매턴 마다 얻는 함수도 실행
    /// </summary>
    public void TurnButtonClick2()
    {   
        isPlayer2Turn = false; // 현재 플레이어 턴을 종료
        isEnemy2Turn = true; // 적 턴이 실행

        turnBtn2.interactable = false; // 턴 종료 버튼을 비활성화
        enemy2Turn++; // 적의 턴 증가 (적의 체력 회복이나 강력한 공격 준비)

        // 적의 턴이 3턴이 진행되었을 경우
        if (enemy2Turn == 3)
        {
            enemy2.EnemyHeal(); // 적의 체력을 회복하는 함수 실행
        }
        // 적의 턴이 6턴이 진행되었을 경우
        else if (enemy2Turn == 6)
        {
            enemy2.EnemyHeal(); // 적의 체력을 회복하는 함수 실행
        }
        // 적의 턴이 9턴이 진행되었을 경우
        else if (enemy2Turn == 9)
        {
            enemy2.EnemyHeal(); // 적의 체력을 회복하는 함수 실행
        }
        // 적의 턴이 12턴이 진행되었을 경우
        else if (enemy2Turn == 12)
        {
            enemy2.EnemyHeal(); // 적의 체력을 회복하는 함수 실행
        }

        else
        {
            enemy2.EnemyAttack2(); // 적 스크립트에 있는 공격 함수 실행
        }

        isPlayer2Turn = true; // 플레이어 턴 시작
        isEnemy2Turn = false; // 적 턴이 종료

        if (isPlayer2Turn == true) // 만약 플레이어 턴이 시작 되면
        {
            player2.PlayerGetCost(); // 플레이어의 코스트를 1씩 증가 (최대 코스트까지)
            player2.PlayerTurnNewCardSpawn(); // 플레이어의 카드가 새롭게 생성
        }

        turnBtn2.interactable = true; // 턴 종료 버튼이 다시 활성화 나중에 코루틴으로 동적 지연을 줄 예정
    }
}
