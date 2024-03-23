using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPosition : MonoBehaviour
{
    // 카드의 고정 위치를 받아오기 위한 변수 지정
    public GameObject cardPosition1;
    public GameObject cardPosition2;
    public GameObject cardPosition3;
    public GameObject cardPosition4;
    public GameObject cardPosition5;

    // 카드의 배열
    public List<GameObject> cards;
    // 카드 복사본 배열
    private GameObject[] cardCopies; 

    // 코루틴위해 사용할 인덱스번호
    private int index = 0;

    void Start()
    {
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

    void Update()
    {
        // 키보드 입력을 감지하여 해당 위치를 매겨변수로 사용
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseCardAtPosition(cardPosition1.transform.position);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseCardAtPosition(cardPosition2.transform.position);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseCardAtPosition(cardPosition3.transform.position);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseCardAtPosition(cardPosition4.transform.position);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UseCardAtPosition(cardPosition5.transform.position);
        }
    }

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
                // 그 위치의 카드 파괴
                Destroy(cardCopy);
                //인덱스값 지정
                index = i;
                // 일정 시간이 지난 후에 새로운 카드 생성
                StartCoroutine(SpawnNewCard(position));

                // 카드를 찾았으므로 반복 종료
                break; 
            }
        }
    }

    IEnumerator SpawnNewCard(Vector3 position)
    {
        // 일정 시간 동안 대기 시간바꾸어도 상관없음
        yield return new WaitForSeconds(2.0f);

        // 사용할 카드의 랜덤선택
        int randomIndex = Random.Range(0, cards.Count);

        // 리스트에서 랜덤 카드를 매개변수 위치에 복사 
        GameObject newCardCopy = Instantiate(cards[randomIndex], position, Quaternion.identity);

        /// 생성된 카드는 리스트에서 제거
        cards.RemoveAt(randomIndex);
        // 카드 복사본 배열에 생성된 카드로 변경
        cardCopies[index] = newCardCopy;

    }

}
