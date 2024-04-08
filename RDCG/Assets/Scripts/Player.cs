using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float playerHp = 100; // 플레이어의 현재 체력
    public float playerMaxHp = 100; // 플레이어의 최대 체력
    public int playerCost = 1; // 플레이어의 현재 사용 할 수 있는 코스트
    public int playerMaxCost = 10; //  플레이어의 최대 코스트
    public int playerTurnCount = 1; // 플레이어의 턴 수를 카운트 하는 변수

    public Enemy enemy; // 적 스크립트 호출

    public Text playerHpText; // 플레이어의 체력을 나타내는 텍스트 UI
    public Text playerCostText; // 플레이어의 코스트를 나타내는 텍스트 UI
    public Slider playerHpBar; // 현재 플레이어의 체력 바 UI 

    public bool isPlayerDead = false; // 플레이어가 죽었는지 살아있는지 상태

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

    // Start is called before the first frame update
    void Start()
    {
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
        playerCostText.text = "Cost : " + playerCost.ToString() + " / " + playerMaxCost.ToString(); // Cost 텍스트를 실시간으로 변경
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

                    // 적 스크립트의 적 체력을 불러와서
                    // 데미지 정보 잘 들어갔는지 확인차 로그
                    enemy.enemyHp -= cardInfo.cardValue;
                    Debug.Log("적의 현재 체력은 " + enemy.enemyHp);

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
        if (playerHp <= 0)
        {
            isPlayerDead = true;
        }
        else
        {
            isPlayerDead = false;
        }
    }
}
