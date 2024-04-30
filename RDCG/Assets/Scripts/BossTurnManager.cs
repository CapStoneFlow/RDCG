using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTurnManager : MonoBehaviour
{
    public Button turnBtn3; // 턴종료 버튼 누르면 적의 턴으로 넘어가는 UI
    public int bossTurn; // 적의 턴이 몇번 진행되었는지 알 수 있는 변수

    public bool isPlayer3Turn; // 현재 플레이어의 턴상태 확인
    public bool isbossTurn; // 현재 적의 턴상태 확인

    public Player player3; // 플레이어 스크립트를 불러옴
    public Enemy boss; // 적 스크립트를 불러옴

    // Start is called before the first frame update
    void Start()
    {
        turnBtn3.GetComponent<Button>();
        bossTurn = 1;
        Player.playerGainGold = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.enemyHp <= 0) // 적의 체력이 0보다 작을 경우
        {
            boss.BossEnemyDeath(); // 적이 죽었다는 함수 실행
        }

        if (Player.playerHp <= 0) // 플레이어의 체력이 0보다 작을 경우
        {
            player3.PlayerDead(); // 플레이어가 죽었다는 함수 실행
        }
    }
    /// <summary>
    /// 플레이어 행동을 다 끝내고
    /// 턴 종료 버튼을 눌렀을 경우 적 행동을 실행 시킬 때 비활성화 시키고
    /// 턴 종료 버튼을 다시 활성화
    /// 그 과정에서 플레이어 턴 일 경우 플레이어의 코스트를 매턴 마다 얻는 함수도 실행
    /// </summary>
    public void TurnButtonClick3()
    {
        isPlayer3Turn = false; // 현재 플레이어 턴을 종료
        isbossTurn = true; // 적 턴이 실행

        turnBtn3.interactable = false; // 턴 종료 버튼을 비활성화
        bossTurn++; // 적의 턴 증가 (적의 체력 회복이나 강력한 공격 준비)

        // 적의 턴이 3턴이 진행되었을 경우
        if (bossTurn == 3)
        {
            boss.EnemyHeal(); // 적의 체력을 회복하는 함수 실행
        }
        // 적의 턴이 6턴이 진행되었을 경우
        else if (bossTurn == 6)
        {
            boss.EnemyHeal(); // 적의 체력을 회복하는 함수 실행
        }
        // 적의 턴이 9턴이 진행 되었을 경우
        else if (bossTurn == 9)
        {
            StartCoroutine(boss.EnemyWarning()); // 적이 강력한 공격을 할 것이라는 경고 애니메이션 (추후 UI 할 때 수정)
        }
        // 적의 턴이 10턴이 진행 되었을 경우
        else if (bossTurn == 10)
        {
            boss.EnemyLastAttack(); // 적이 강력한 공격인 플레이어 체력 30을 깎는 함수 실행
        }

        else
        {
            boss.EnemyAttack2(); // 적 스크립트에 있는 공격 함수 실행
        }

        isPlayer3Turn = true; // 플레이어 턴 시작
        isbossTurn = false; // 적 턴이 종료

        if (isPlayer3Turn == true) // 만약 플레이어 턴이 시작 되면
        {
            player3.PlayerGetCost(); // 플레이어의 코스트를 1씩 증가 (최대 코스트까지)
            player3.PlayerTurnNewCardSpawn(); // 플레이어의 카드가 새롭게 생성
        }

        turnBtn3.interactable = true; // 턴 종료 버튼이 다시 활성화 나중에 코루틴으로 동적 지연을 줄 예정
    }
}
