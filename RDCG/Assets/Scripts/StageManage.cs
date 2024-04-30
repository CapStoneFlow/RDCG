using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManage : MonoBehaviour
{
    Player player; // 플레이어 스크립트 객체
    Enemy enemy; // 적 스크립트 객체

    public Button stage1PlayButton; // 스테이지 1 전투 씬으로 갈 수 있는 버튼
    public Button stage2PlayButton; // 스테이지 2 버튼
    public Button BossPlayButton;  // 보스 버튼
    public Button StoreButton; // 상점 버튼

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Stage2ButtonActive();
        BossButtonActive();
    }
    // Stage1 버튼을 누를시 Play으로 이동
    public void Stage1BtnClick()
    {
        SceneManager.LoadScene("Play");
    }
    /// <summary>
    /// 스테이지 1을 깼을 경우 스테이지2 버튼이 활성화가 됨
    /// </summary>
    public void Stage2ButtonActive()
    {
        if (Player.isPlayerStage1 == true)
        {
            stage2PlayButton.GetComponent<Button>().interactable = true;
        }
    }

    /// <summary>
    /// 스테이지 2을 깼을 경우 Boss 버튼이 활성화가 됨
    /// </summary>
    public void BossButtonActive()
    {
        if (Player.isPlayerStage2 == true)
        {
            BossPlayButton.GetComponent<Button>().interactable = true;
        }
    }

    public void StoreBtnClick()
    {
        SceneManager.LoadScene("Store");
    }

    public void Stage2ClickBtn()
    {
        SceneManager.LoadScene("Play2");
    }

    public void BossClickBtn()
    {
        SceneManager.LoadScene("Boss");
    }
}
