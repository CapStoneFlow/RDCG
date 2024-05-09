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
    public static bool isEnemyDead1; // 스테이지 1 적이 죽었는지 살았는지 상태
    public static bool isEnemyDead2; // 스테이지 2 적이 죽었는지 살았는지 상태
    public static bool isBossDead; // 보스가 죽었는지 살았는지 상태

    public Text enemyHpText; // 적의 체력을 알 수 있는 체력 텍스트
    public Text playerMoneyText; // 플레이어의 골드 텍스트
    public Slider enemyHpBar; // 현재 적의 체력 바 UI 
    public Animation enemyWarning; //적이 강력한 공격을 하는 애니메이션
    public Text enemyWarningText; // 적이 강력한 공격을 할 때 나오는 텍스트

    public int enemyAttackDamage; // 적이 플레이어에게 줄 수 있는 데미지
    public int enemyRandomDamage; // 적이 플레이어에게 줄 수 있는 랜덤 데미지 (적의 행동 패턴을 다양하게)

    public Player player; // 플레이어 스크립트

    public Animator animator; //애니메이션 컴포넌트

    public GameObject healParticle; //힐 파티클

    public AudioClip hitclip; //맞는효과음
    public AudioClip healclip; //힐 효과음

    public Transform cam; // 카메라 받아오기

    // Start is called before the first frame update
    void Start()
    {
        isEnemyDead1 = false; // 죽은 상태가 아니니 false
        isEnemyDead2 = false; // 죽은 상태가 아니니 false
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
        StartCoroutine(Shake());
        GetComponent<AudioSource>().PlayOneShot(hitclip);
        animator.Play("attack");
        enemyRandomDamage = Random.Range(1, 11); // 적은 플레이어의 체력을 랜덤적으로 1~10까지 까이게 함
        Player.playerHp -= enemyRandomDamage; // 플레이어의 체력을 적의 랜덤 데미지만큼 까이게 함
        Debug.Log("플레이어의 현재 체력은 " + Player.playerHp);
    }

    /// <summary>
    /// 적이 플레이어의 체력을 7부터 15으로 랜덤하게 데미지를 주는 함수
    /// </summary>
    public void EnemyAttack2()
    {
        StartCoroutine(Shake());
        GetComponent<AudioSource>().PlayOneShot(hitclip);
        animator.Play("attack");
        enemyRandomDamage = Random.Range(7, 16); // 적은 플레이어의 체력을 랜덤적으로 7~15까지 까이게 함
        Player.playerHp -= enemyRandomDamage; // 플레이어의 체력을 적의 랜덤 데미지만큼 까이게 함
        Debug.Log("플레이어의 현재 체력은 " + Player.playerHp);
    }

    /// <summary>
    /// 적이 5턴일 경우 현재 체력에서 최대 체력 절반만큼 차게 하는 함수
    /// </summary>
    public void EnemyHeal()
    {
        GetComponent<AudioSource>().PlayOneShot(healclip);
        healParticle.SetActive(false); //파티클실행
        healParticle.SetActive(true); //파티클실행
        Debug.Log("적의 체력이 회복되었습니다.");
        enemyHp += 15; // 적의 체력이 15씩 힐이 됨

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
        StartCoroutine(Shake());
        GetComponent<AudioSource>().PlayOneShot(hitclip);
        animator.Play("attack");
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
    /// 스테이지 1 적이 죽었을 때 플레이어에게 다음 스테이지로 가는 씬 이동 구현
    /// </summary>
    public void Stage1EnemyDeath()
    {
        StartCoroutine(DelayedAnimation1("Stage Clear1")); //코루틴실행
    }

    /// <summary>
    /// 스테이지 2 적이 죽었을 때 플레이어에게 다음 스테이지로 가는 씬 이동 구현
    /// </summary>
    public void Stage2EnemyDeath()
    {
        StartCoroutine(DelayedAnimation2("Stage Clear2")); //코루틴실행
    }

    /// <summary>
    /// 보스가 죽었을 때 플레이어에게 다음 스테이지로 가는 씬 이동 구현
    /// </summary>
    public void BossEnemyDeath()
    {
        StartCoroutine(DelayedAnimation3("Ending")); //코루틴실행
    }

    // 죽는 애니메이션 보기위한 코루틴
    IEnumerator DelayedAnimation1(string sceneName)
    {
        SetPlayerAndTurnBtnActive(false);  // 실행 전에 player와 turnbtn 비활성화
        animator.Play("dead");
        yield return new WaitForSeconds(2f); // 2초 대기
        SetPlayerAndTurnBtnActive(true); // 실행 후에 player와 turnbtn 다시 활성화
        isEnemyDead1 = true; // 스테이지 1 적이 죽은 상태를 true
        player.PlayerClearStage1(); // player가 Stage1을 클리어 했다는 함수 실행
        Player.playerGainGold += Random.Range(500, 1001); // 플레이어의 획득 골드를 500 ~ 1000까지 랜덤으로 획득
        Player.playerCurrentGold += Player.playerGainGold; // 플레이어의 현재 골드를 플레이어의 획득 골드만큼 더함
        SceneManager.LoadScene(sceneName);
    }
    IEnumerator DelayedAnimation2(string sceneName)
    {
        SetPlayerAndTurnBtnActive(false);  // 실행 전에 player와 turnbtn 비활성화
        animator.Play("dead");
        yield return new WaitForSeconds(2f); // 2초 대기
        SetPlayerAndTurnBtnActive(true); // 실행 후에 player와 turnbtn 다시 활성화
        isEnemyDead2 = true; // 스테이지 2 적이 죽은 상태를 true
        player.PlayerClearStage2(); // player가 Stage2를 클리어 했다는 함수 실행
        Player.playerGainGold += Random.Range(1000, 2001); // 플레이어의 획득 골드를 1000 ~ 2000까지 랜덤으로 획득
        Player.playerCurrentGold += Player.playerGainGold; // 플레이어의 현재 골드를 플레이어의 획득 골드만큼 더함
        SceneManager.LoadScene(sceneName);
    }
    IEnumerator DelayedAnimation3(string sceneName)
    {
        SetPlayerAndTurnBtnActive(false);  // 실행 전에 player와 turnbtn 비활성화
        animator.Play("dead");
        yield return new WaitForSeconds(2f); // 2초 대기
        SetPlayerAndTurnBtnActive(true); // 실행 후에 player와 turnbtn 다시 활성화
        isBossDead = true; // 보스가 죽은 상태를 true
        player.PlayerClearBoss(); // player가 Stage1을 클리어 했다는 함수 실행
        SceneManager.LoadScene(sceneName);
    }

    // 죽는애니메이션 실행중 아무것도 할수없도록 설정해주는 함수
    private void SetPlayerAndTurnBtnActive(bool isActive)
    {
        // player 비활성화
        player.gameObject.SetActive(isActive);

        // TurnBtn 태그를 가진 모든 게임 오브젝트를 찾아서 비활성화
        GameObject[] turnBtnObjects = GameObject.FindGameObjectsWithTag("TurnBtn");
        foreach (GameObject turnBtnObject in turnBtnObjects)
        {
            turnBtnObject.SetActive(isActive);
        }
    }

    // 플레이어 맞을시 카메라 흔들리는 함수 구현
    IEnumerator Shake()
    {
        //원래 위치 저장
        Vector3 oriposition = cam.position;
        float elapsed = 0.0f;
        // 1초가 지나면 반복문 종료
        while (elapsed < 0.5f)
        {
            cam.position = oriposition + Random.insideUnitSphere * 0.5f;
            yield return null;
            elapsed += Time.deltaTime;
        }
        cam.localPosition = oriposition;    
    }

}
