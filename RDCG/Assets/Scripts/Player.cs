using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static float playerHp = 100; // 플레이어의 현재 체력 (static으로 전역 변수 접근)
    public int playerMaxHp = 100; // 플레이어의 최대 체력
    public static int playerCurrentGold = 0; // 플레이어가 현재 가지고 있는 돈 (static으로 전역 변수 접근)
    public static int playerGainGold = 0; // 플레이어가 스테이지 끝나고 얻은 돈 (static으로 전역 변수 접근) 

    public int playerCost = 1; // 플레이어의 현재 사용 할 수 있는 코스트
    public int playerMaxCost = 10; //  플레이어의 최대 코스트
    public int playerTurnCount = 1; // 플레이어의 턴 수를 카운트 하는 변수
    private int rannum = 0;   // 카드의 운적인 요소를 정하기 위해 사용하는 변수
    private int mulnum = 0;  // 카드효과 배로 데미지들어가는것을 위해 사용하는 변수
    private int cardDamage = 0; // 카드 value값 받아오기위해 사용하는 변수

    public Enemy enemy;

    public Text playerHpText; // 플레이어의 체력을 나타내는 텍스트 UI
    public Text playerCostText; // 플레이어의 코스트를 나타내는 텍스트 UI
    public Slider playerHpBar; // 현재 플레이어의 체력 바 UI 

    public bool isPlayerDead = false; // 플레이어가 죽었는지 살아있는지 상태
    public static bool isPlayerStage1 = false; // 플레이어가 스테이지1을 깼는지 못꺴는지 알 수 있는 상태 (Static으로 버튼 활성화)
    public static bool isPlayerStage2 = false; // 플레이어가 스테이지2을 깼는지 못꺴는지 알 수 있는 상태 (Static으로 버튼 활성화)
    public static bool isPlayerBoss = false; // 플레이어가 보스를 꺴는지 못깼는지 알 수 있는 상태 (Static으로 버튼 활성화)

    // 카드의 고정 위치를 받아오기 위한 변수 지정
    public GameObject cardPosition1;
    public GameObject cardPosition2;
    public GameObject cardPosition3;
    public GameObject cardPosition4;
    public GameObject cardPosition5;

    // 현재 스테이지에서 사용할 카드을 담은 리스트 
    public List<GameObject> cards;

    // 카드를 사용한 몇 번째 인지 알 수 있는 번호 리스트
    private List<int> useNumbers;

    // 카드 복사본 배열
    private GameObject[] cardCopies;

    // 남은 카드가 없을때 사용할 기본카드
    public GameObject normalCard;

    public Animator animator; //애니메이션 컴포넌트

    public GameObject healParticle; //힐 파티클
    public GameObject attParticle; //공격 파티클
    public GameObject costParticle; //코스트 파티클

    public AudioClip hitclip; //맞는효과음
    public AudioClip costclip; //코스트효과음
    public AudioClip healclip; //힐 효과음

 
    // Start is called before the first frame update
    void Start()
    {
        // currentdeck 클래스의 인스턴스를 찾습니다.
        CurrentDeck cardDeck = FindObjectOfType<CurrentDeck>();

        //  CurrentDeck클래스의 cardDeck 리스트에 있는 카드를 Player의 cards 리스트로 복사합니다.
        cards.AddRange(cardDeck.cardDeck);

        // useNumbers 리스트를 <int> 초기화
        useNumbers = new List<int>();

        // 카드 위치를 배열로 선언
        GameObject[] cardPositions = new GameObject[] { cardPosition1, cardPosition2, cardPosition3, cardPosition4, cardPosition5 };

        // 카드 복사본 배열 초기화
        cardCopies = new GameObject[cardPositions.Length];

        for (int i = 0; i < cardPositions.Length; i++)
        {
            // 랜덤 카드 인덱스 선택 
            int randomIndex = Random.Range(0, cards.Count);
            // 선택한 랜덤 카드를 복사하여 생성
            GameObject cardCopy = Instantiate(cards[randomIndex], cardPositions[i].transform.position, Quaternion.identity);
            // 카드 리스트에서 복사된 카드 제거
            cards.RemoveAt(randomIndex);
            // 카드 복사본 배열에 추가
            cardCopies[i] = cardCopy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerHpText.text = "HP : " + playerHp.ToString(); // HP 텍스트를 실시간으로 변경
        playerCostText.text = playerCost.ToString() + " / " + playerTurnCount.ToString(); // Cost 텍스트를 실시간으로 변경
        playerHpBar.value = playerHp / playerMaxHp; // HP 슬라이더 바 현재 체력 비례하여 최대 체력으로 나눠 업데이트

        // 키보드 입력을 감지하여 해당 위치를 매겨변수로 사용
        if (Input.GetKeyDown(KeyCode.Alpha1)) //키보드숫자1번 누를시
        {
            UseCardAtPosition(cardPosition1.transform.position); //1번카드포지션위치를 매개변수로 넣어 함수실행
            Debug.Log("1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) //키보드2번 누를시
        {
            UseCardAtPosition(cardPosition2.transform.position); //2번카드포지션위치를 매개변수로 넣어 함수실행
            Debug.Log("2");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) //키보드3번 누를시
        {
            UseCardAtPosition(cardPosition3.transform.position); //3번카드포지션위치를 매개변수로 넣어 함수실행
            Debug.Log("3");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) //키보드4번 누를시
        {
            UseCardAtPosition(cardPosition4.transform.position); //4번카드포지션위치를 매개변수로 넣어 함수실행
            Debug.Log("4");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) //키보드5번 누를시
        {
            UseCardAtPosition(cardPosition5.transform.position); //5번카드포지션위치를 매개변수로 넣어 함수실행
            Debug.Log("5");
        }
    }

    // 사용한카드의 위치를 매개변수로 받아 그 위치에 카드를 제거하고 새로운 카드를 생성
    void UseCardAtPosition(Vector3 position)
    {
        // 일정 거리 내에 있는지 확인하기 위한 임계값, 내 맘대로설정 값 바꾸어도 상관없음
        float distanceThreshold = 1.5f;

        // 5개의 위치중 이게 어느위치인지 찾기위한 반복문 
        for (int i = 0; i < cardCopies.Length; i++)
        {
            GameObject cardCopy = cardCopies[i];
            // 만약 cardCopy변수가 비어있지않고 매개변수로받은 위치와 for문의 카드 위 위치가 일정 범위내에 있을시
            if (cardCopy != null && Vector3.Distance(cardCopy.transform.position, position) < distanceThreshold)
            {
                // 카드 정보를 가져옴
                CardInfo cardInfo = cardCopy.GetComponent<CardInfo>();

                // 카드의 코스트를 확인
                if (cardInfo != null && cardInfo.cardCost <= playerCost)
                {
                    // 카드 코스트만큼 차감
                    playerCost -= cardInfo.cardCost;

                    // 카드효과 불러와 적용하는 함수 호출
                    CardEffect(cardInfo);  

                    // useNumbers 리스트에 몇 번째 cardPosition이 사용이 되었는지 리스트에 추가
                    useNumbers.Add(i);

                    // 카드 파괴
                    Destroy(cardCopy);
                }
                else
                {
                    //코스트 부족시 띄울 로그
                    Debug.Log("코스트가 부족합니다.");
                }
                // 카드를 찾았으므로 반복 종료
                break;
            }
        }
    }
    // 카드위치를 매개변수로 받아 리스트에서 랜덤 카드를 생성하는 함수
    IEnumerator SpawnNewCard(Vector3 position , int index)
    {
        yield return null;

        //새롭개 생성할 카드를 받아올 변수
        GameObject newCardCopy;

        if (cards.Count > 0)
        {
            // 사용할 카드의 랜덤선택
            int randomIndex = Random.Range(0, cards.Count);

            // 리스트에서 랜덤 카드를 매개변수 위치에 복사 
            newCardCopy = Instantiate(cards[randomIndex], position, Quaternion.identity);

            // 생성된 카드는 리스트에서 제거
            cards.RemoveAt(randomIndex);
        }
        else
        {
            // 카드 리스트에 더 이상 카드가 없을 경우 기본 카드 생성
            newCardCopy = Instantiate(normalCard, position, Quaternion.identity);
        }
        // 카드 복사본 배열에 생성된 카드로 변경
        cardCopies[index] = newCardCopy;
    }
    
    public void PlayerTurnNewCardSpawn()
    {
        // useNumbers 리스트 값 순회
        foreach (int num in useNumbers)
        {
            // num이 1일 경우 (카드 1번째를 사용 하였을 경우)
            if (num == 0)
            {
                // CardPosition 1에 카드 생성
                StartCoroutine(SpawnNewCard(cardPosition1.transform.position, num));
            }
            else if (num == 1)
            {
                StartCoroutine(SpawnNewCard(cardPosition2.transform.position, num));
            }
            else if (num == 2)
            {
                StartCoroutine(SpawnNewCard(cardPosition3.transform.position, num));
            }
            else if (num == 3)
            {
                StartCoroutine(SpawnNewCard(cardPosition4.transform.position, num));
            }
            else if (num == 4)
            {
                StartCoroutine(SpawnNewCard(cardPosition5.transform.position, num));
            }
        }
        useNumbers.Clear();
    }

    /// <summary>
    /// 플레이어가 턴마다 코스트를 얻는 것을 구현
    /// </summary> 
    public void PlayerGetCost()
    {
        if (playerTurnCount < playerMaxCost) // 플레이어의 턴 수가 플레이어의 최대 코스트보다 작을 경우
        {
            // 현재 플레이어의 턴 수를 증가 시키고
            playerTurnCount++;
        }
        // 플레이어의 코스트를 플레이어의 턴과 같이 함
        playerCost = playerTurnCount;
    }
    /// <summary>
    /// 현재 플레이어가 죽는 상태를 나타내는 함수
    /// 플레이어의 체력이 0 이하 일 경우 isPlayerDead true
    /// 플레이어의 체력이 0 보다 많을 경우 isPlayerDead false
    /// </summary>
    public void PlayerDead()
    {
        isPlayerDead = true; // 플레이어가 죽은 상태
        playerHp = playerMaxHp; // 플레이어의 체력을 플레이어의 최대 체력으로 초기화
        playerCurrentGold = 0; // 플레이어의 현재 가지고 있는 돈을 0으로 초기화
        playerGainGold = 0; // 플레이어의 얻는 골드를 0으로 초기화
        isPlayerStage1 = false; // 플레이어의 스테이지1 상태를 클리어 못한 것으로 초기화
        isPlayerStage2 = false; // 플레이어의 스테이지2 상태를 클리어 못한 것으로 초기화
        isPlayerBoss = false; // 플레이어의 보스 스테이지 상태를 클리어 못한 것으로 초기화
        // CurrentDeck 인스턴스가 존재하는지 확인 후 덱 초기화 함수 호출
        if (CurrentDeck.instance != null)
        {
            CurrentDeck.instance.ClearDeck();
        }

        SceneManager.LoadScene("Death"); // "Death" 씬으로 이동
    }

    /// <summary>
    /// 현재 플레이어가 Stage1을 깬건지 알 수 있는 상태
    /// Stage는 1, 2, Boss로 나뉘어져 있으며 총 3번의 전투를 할 예정
    /// </summary>
    public void PlayerClearStage1()
    {
        if (Enemy.isEnemyDead1 == true) // 에너미가 죽었을 때
        {
            isPlayerStage1 = true; // 플레이어는 Stage1을 클리어 된 상태
        }
    }

    /// <summary>
    /// 현재 플레이어가 Stage2을 깬건지 알 수 있는 상태
    /// </summary>
    public void PlayerClearStage2()
    {
        if (Enemy.isEnemyDead2 == true) // 에너미가 죽었을 때
        {
            isPlayerStage2 = true; // 플레이어는 Stage2을 클리어 된 상태
        }
    }

    /// <summary>
    /// 현재 플레이어가 보스를 깬건지 알 수 있는 상태
    /// </summary>
    public void PlayerClearBoss()
    {
        if (Enemy.isBossDead == true) // 보스가 죽었을 때
        {
            isPlayerBoss = true; // 플레이어는 보스를 클리어 된 상태
            playerHp = playerMaxHp; // 플레이어의 체력을 플레이어의 최대 체력으로 초기화
            playerCurrentGold = 0; // 플레이어의 현재 가지고 있는 돈을 0으로 초기화
            playerGainGold = 0; // 플레이어의 얻는 골드를 0으로 초기화
            isPlayerStage1 = false; // 플레이어의 스테이지1 상태를 클리어 못한 것으로 초기화
            isPlayerStage2 = false; // 플레이어의 스테이지2 상태를 클리어 못한 것으로 초기화
            isPlayerBoss = false; // 플레이어의 보스 스테이지 상태를 클리어 못한 것으로 초기화
            // CurrentDeck 인스턴스가 존재하는지 확인 후 덱 초기화 함수 호출
            if (CurrentDeck.instance != null)
            {
                CurrentDeck.instance.ClearDeck();
            }
        }

    }

    // 카드의 효과를 확인하고 적응해주는 함수
    public void CardEffect(CardInfo cardInfo)
    {
        attParticle.SetActive(false);
        costParticle.SetActive(false);
        healParticle.SetActive(false);
        // 효과없이 공격만있는 카드일경우
        if (cardInfo.cardEffect == 0)
        {
            // cardDamage에 cardValue값 받아옴
            cardDamage = cardInfo.cardValue;
            // mulnum이 0이아닐시 cardDamage는 mulnum값만큼 배로 들어감
            if (mulnum != 0)
            {
                cardDamage *= mulnum;
                mulnum = 0;
            }
            // 적 스크립트의 적 체력을 불러와서
            // 데미지 정보 잘 들어갔는지 확인차 로그
            enemy.enemyHp -= cardDamage;
            attParticle.SetActive(true); //파티클실행
            Debug.Log("적의 현재 체력은 " + enemy.enemyHp);
            animator.Play("damage");
            GetComponent<AudioSource>().PlayOneShot(hitclip);
        }
        // 1일경우 절반의 확률로 적이 카드값의 두배로 데미지를 입거나 카드값만큼 회복함
        else if (cardInfo.cardEffect == 1)
        {
            // cardDamage에 cardValue값 받아옴
            cardDamage = cardInfo.cardValue;
            // mulnum이 0이아닐시 cardDamage는 mulnum값만큼 배로 들어감
            if (mulnum != 0)
            {
                cardDamage *= mulnum;
                mulnum = 0;
               
            }
            //rannum에 0이나 1을 넣어 0일시 데미지입고 1일시 회복하도록 설정
            rannum = Random.Range(0, 2);
            if (rannum == 0)
            {
                attParticle.SetActive(true); //파티클실행
                cardDamage *= 2;
                enemy.enemyHp -= cardDamage;
                animator.Play("damage");
                GetComponent<AudioSource>().PlayOneShot(hitclip);
            }
            else if (rannum == 1)
            {
                healParticle.SetActive(true); //파티클실행
                enemy.enemyHp += cardDamage;
                GetComponent<AudioSource>().PlayOneShot(healclip);
                //만약 적체력을 회복하면 최대체력보다 높아질시 체력을 최대체력으로 지정
                if (enemy.enemyHp > enemy.enemyMaxHp)
                {
                    enemy.enemyHp = enemy.enemyMaxHp;
                }
            }
        }
        // 2일경우 절반의 확률로 플레이어가 카드값의 데미지를 입거나 카드값의 두배만큼만큼 회복함
        else if (cardInfo.cardEffect == 2)
        {
            // cardDamage에 cardValue값 받아옴
            cardDamage = cardInfo.cardValue;
            // mulnum이 0이아닐시 cardDamage는 mulnum값만큼 배로 들어감
            if (mulnum != 0)
            {
                cardDamage *= mulnum;
                mulnum = 0;
            }
            //rannum에 0이나 1을 넣어 0일시 데미지입고 1일시 회복하도록 설정
            rannum = Random.Range(0, 2);
            if (rannum == 0)
            {
                GetComponent<AudioSource>().PlayOneShot(hitclip);
                playerHp -= cardDamage;
            }
            else if (rannum == 1)
            {
                GetComponent<AudioSource>().PlayOneShot(healclip);
                cardDamage *= 2;
                playerHp += cardDamage;
                //만약 플레이어체력을 회복하면 최대체력보다 높아질시 체력을 최대체력으로 지정
                if (playerHp > playerMaxHp)
                {
                    playerHp = playerMaxHp;
                }
            }
        }
        // 3일경우 카드값만큼 절반의 확률로 이번턴에 사용할코스트를 올려주거나 플레이어 최대코스트를 올려줍니다.
        else if(cardInfo.cardEffect == 3)
        {
            GetComponent<AudioSource>().PlayOneShot(costclip);
            // cardDamage에 cardValue값 받아옴
            cardDamage = cardInfo.cardValue;
            // mulnum이 0이아닐시 cardDamage는 mulnum값만큼 배로 들어감
            if (mulnum != 0)
            {
                cardDamage *= mulnum;
                mulnum = 0;
            }
            //rannum에 0이나 1을 넣어 0일시 현재코스트 1일시 최대코스트 올려주도록 설정  
            costParticle.SetActive(true);
            rannum = Random.Range(0, 2);
            if (rannum == 0)
            {
                playerCost += cardDamage;
                if (playerCost > playerTurnCount) // 플레이어의 턴 수가 플레이어의 턴수보다 클경우
                {
                    // 플레이어 코스트를 턴수로 지정
                    playerCost = playerTurnCount;
                }
            }
            else if (rannum == 1)
            {
                // 현재 플레이어의 턴 수를 증가 
                playerTurnCount+= cardDamage;
                if (playerTurnCount > playerMaxCost) // 플레이어의 턴 수가 플레이어의 최대코스트보다 클경우
                {
                    // 플레이어 턴수를 최대 코스트로 지정
                    playerTurnCount = playerMaxCost;
                }
            }
        }
        // 4일경우 다음 카드 사용시 효과 카드값만큼 배로 들어감
        else if( cardInfo.cardEffect == 4)
        {
            GetComponent<AudioSource>().PlayOneShot(costclip);
            mulnum = cardInfo.cardValue;
        }
        // 5일경우 만약 카드 사용시 남은코스트가 0이라면 데미지 두배로 들어감
        else if((cardInfo.cardEffect == 5))
        {
            // cardDamage에 cardValue값 받아옴
            cardDamage = cardInfo.cardValue;
            // mulnum이 0이아닐시 cardDamage는 mulnum값만큼 배로 들어감
            if (mulnum != 0)
            {
                cardDamage *= mulnum;
                mulnum = 0;
            }
            // 남은 코스트가 0일시 두배의 데미지 들어감
            if(playerCost == 0)
            {
                cardDamage *= 2;
            }
            GetComponent<AudioSource>().PlayOneShot(hitclip);
            attParticle.SetActive(true); //파티클실행
            enemy.enemyHp -= cardDamage;
            animator.Play("damage");
        }
    }

    // 다른 스크립트에서 쉽게 현재 플레이어 돈 얻어오기 위해 만든 함수
    public static int GetPlayerMoney()
    {
        return playerCurrentGold;
    }

    // 다른 스크립트에서 쉽게 현재 플레이어 돈 설정하기 위해 만든 함수
    public static void SetPlayerMoney(int money)
    {
        playerCurrentGold = money;
        Debug.Log("돈 :  " + money);
    }

    // 다른 스크립트에서 현재 플레이어 체력 얻어오기 위해 만든 함수
    public static float GetPlayerHp()
    {
        return playerHp;
    }

    // 다른 스크립트에서 현재 플레이어 체력 설정하기 위해 만든 함수
    public static void SetPlayerHp(float hp)
    {
        playerHp = hp;
        Debug.Log("체력 :  " + hp);
    }

}
