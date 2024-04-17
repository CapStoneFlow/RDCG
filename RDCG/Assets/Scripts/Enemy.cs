using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float enemyHp; // 적의 현재 체력
    public float enemyMaxHp; // 적의 최대 체력
    public static bool isEnemyDead; // 적이 죽었는지 살았는지 상태

    public Text enemyHpText; // 적의 체력을 알 수 있는 체력 텍스트
    public Text playerMoneyText; // 플레이어의 골드 텍스트
    public Slider enemyHpBar; // 현재 적의 체력 바 UI 
    public Animation enemyWarning; //적이 강력한 공격을 하는 애니메이션
    public Text enemyWarningText; // 적이 강력한 공격을 할 때 나오는 텍스트

    public int enemyAttackDamage; // 적이 플레이어에게 줄 수 있는 데미지
    public int enemyRandomDamage; // 적이 플레이어에게 줄 수 있는 랜덤 데미지 (적의 행동 패턴을 다양하게)

    public Player player; // 플레이어 스크립트

    // Start is called before the first frame update
    void Start()
    {
        isEnemyDead = false; // 죽은 상태가 아니니 false
        enemyMaxHp = 30; // 적의 최대 체력은 30
        enemyWarningText.enabled = false; // 적 강력한 공격 나오는 텍스트 안보이게
    }

    // Update is called once per frame
    void Update()
    {
        enemyHpText.text = "HP : " + enemyHp.ToString();
        enemyHpBar.value = enemyHp / enemyMaxHp; // HP 슬라이더 바 현재 체력 비례하여 최대 체력으로 나눠 업데이트
    }

    /// <summary>
    /// 적이 플레이어의 체력을 1부터 10으로 랜덤하게 데미지를 주는 함수
    /// </summary>
    public void EnemyAttack()
    {
        enemyRandomDamage = Random.Range(1, 11); // 적은 플레이어의 체력을 랜덤적으로 1~10까지 까이게 함
        Player.playerHp -= enemyRandomDamage; // 플레이어의 체력을 적의 랜덤 데미지만큼 까이게 함
        Debug.Log("플레이어의 현재 체력은 " + Player.playerHp);
    }

    /// <summary>
    /// 적이 5턴일 경우 현재 체력에서 최대 체력 절반만큼 차게 하는 함수
    /// </summary>
    public void EnemyHeal()
    {
        Debug.Log("적의 체력이 회복되었습니다.");
        enemyHp += enemyMaxHp; // 적의 체력이 MaxHp만큼 힐이 됨

        if (enemyHp >= enemyMaxHp) // 적의 체력이 힐이 적의 최대 체력보다 높을 경우 
        {
            enemyHp = enemyMaxHp; // 적의 체력은 적의 최대 체력으로 고정
        }
    }

    /// <summary>
    /// 적의 턴이 10턴이 되었을 경우 플레이어에게 강력한 공격을 한번 주는 함수
    /// </summary>
    public void EnemyLastAttack()
    {
        enemyAttackDamage = 30; // 강력한 공격은 30 데미지
        Player.playerHp -= enemyAttackDamage; // 플레이어 체력을 30만큼 까이게 함
        Debug.Log("플레이어의 현재 체력은 " + Player.playerHp);
    }

    /// <summary>
    /// 적이 강력한 공격을 할 때 나오는 경고창 UI 패널로 나옴
    /// </summary>
    public IEnumerator EnemyWarning()
    {
        enemyWarningText.enabled = true; // 경고 메세지 보이게 함
        enemyWarningText.text = "적이 강력한 공격을 \n준비중입니다...";
        enemyWarning.Play(); // 적 경고 애니메이션 실행

        yield return new WaitForSeconds(1.0f); // 1초 대기 후

        enemyWarningText.enabled = false; // 경고 메세지 사라지게 실행
    }
    /// <summary>
    /// 적이 죽었을 때 플레이어에게 다음 스테이지로 가는 씬 이동 구현
    /// </summary>
    public void EnemyDeath()
    {
        isEnemyDead = true; // 적이 죽은 상태를 true
        player.PlayerClearStage1(); // player가 Stage1을 클리어 했다는 함수 실행
        Player.playerGainGold += Random.Range(100, 601); // 플레이어의 획득 골드를 100 ~ 600까지 랜덤으로 획득
        Player.playerCurrentGold += Player.playerCurrentGold; // 플레이어의 현재 골드를 플레이어의 획득 골드만큼 더함
        SceneManager.LoadScene("Stage Clear1"); // Stage Clear1 씬으로 이동
    }
}
