using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardPosition : MonoBehaviour
{
    // 카드의 고정 위치를 받아오기 위한 변수 지정
    public GameObject cardPosition1;
    public GameObject cardPosition2;
    public GameObject cardPosition3;
    public GameObject cardPosition4;
    public GameObject cardPosition5;

    // 현재스테이지에서 사용할 카드을 담은 리스트 
    public List<GameObject> cards; 
    // 카드 복사본 배열
    private GameObject[] cardCopies;

    // 남은 카드가 없을때 사용할 기본카드
    public GameObject normalCard;

    // 코루틴위해 사용할 인덱스번호
    private int index = 0;

    //코스트 부족 테스트를 위한 임시 변수
    private int testCost = 1000;

    void Start()
    {
        // 카드 위치를 배열로 선언
        GameObject[] cardPositions = new GameObject[] { cardPosition1, cardPosition2, cardPosition3, cardPosition4, cardPosition5 };

        // 카드 복사본 배열 초기화
        cardCopies = new GameObject[cardPositions.Length];

        // CurrentDeck스크립트 불러옴
        CurrentDeck currentDeck = FindObjectOfType<CurrentDeck>();

        // null 체크를 수행하여 currentDeck가 null이 아닌지 확인합니다.
        if (currentDeck != null && currentDeck.cardDeck != null)
        {
            // foreach문 사용하여 카드덱에 있는 카드들 cards리스트에 추가
            foreach (GameObject card in currentDeck.cardDeck)
            {
                // 잘 추가되는지 확인하기위한 로그
                Debug.Log("카드가 추가되었습니다");
                cards.Add(card);
            }
        }
        else
        {
            Debug.Log("추가할 카드가 없습니다");
        }


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

    void Update()
    {
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
            Debug.Log("4");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) //키보드4번 누를시
        {   
            UseCardAtPosition(cardPosition4.transform.position); //4번카드포지션위치를 매개변수로 넣어 함수실행
            Debug.Log("3");
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
            // 만약 cardCopy변수가 비어있지않고 매개변수로받은 위치와 포문의 카드위 위치가 일정 범위내에 있을시
            if (cardCopy != null && Vector3.Distance(cardCopy.transform.position, position) < distanceThreshold)
            {
                // 카드 정보를 가져옴
                CardInfo cardInfo = cardCopy.GetComponent<CardInfo>();
                // 카드의 코스트를 확인
                if (cardInfo != null && cardInfo.cardCost <= testCost)
                {
                    // 카드 코스트만큼 차감
                    testCost -= cardInfo.cardCost;
                    // 데미지 정보 잘 들어갔는지 확인차 로그
                    Debug.Log("사용한 카드의 데미지는 : " + cardInfo.cardValue);
                    // 카드 파괴
                    Destroy(cardCopy);
                    // 인덱스값 지정
                    index = i;
                    // 일정 시간이 지난 후에 새로운 카드 생성
                    StartCoroutine(SpawnNewCard(position));
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
    IEnumerator SpawnNewCard(Vector3 position)
    {
        // 일정 시간 동안 대기 시간바꾸어도 상관없음
        yield return new WaitForSeconds(2.0f);

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

}
