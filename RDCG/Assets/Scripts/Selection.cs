using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Selection : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.0f; // 페이드 효과 지속 시간

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator FadeIn()
    {
        float t = fadeDuration;
        while (t > 0)
        {
            t -= Time.deltaTime;
            float alpha = t / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator FadeOut()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = t / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0.001f);
        }
        yield return new WaitForSeconds(1.0f);
        // 페이드 아웃 후 씬 변경
        SceneManager.LoadScene("CharacterSelection1");
    }

    // CharacterSelection2에서 스테이지 이동 클릭 시 Stage으로 이동 
    public void StageClickBtn()
    {
        SceneManager.LoadScene("Stage");
    }
    //MainTitle에서 게임시작 버튼 클릭 시 CharacterSelection1으로 이동
    public void MainTitleClickStartBtn(){
        StartCoroutine(FadeOut());
    }

    // CharacterSelection1화면에서 캐릭터 버튼 클릭시 CharacterSelection2로 이동
    public void CharacterSelection1ClickChar1Btn(){
        SceneManager.LoadScene("CharacterSelection2");
    }
    // CharacterSelection1화면에서 캐릭터 버튼 클릭시 CharacterSelection3로 이동
    public void CharacterSelection1ClickChar2Btn()
    {
        SceneManager.LoadScene("CharacterSelection3");
    }

    // CharacterSelection1화면에서 뒤로가기 버튼 클릭시 MainTitle로 이동
    public void CharacterSelection1ClickBackBtn(){
        SceneManager.LoadScene("MainTitle");
    }

    //CharacterSelection2에서 뒤로가기버튼 클릭 시 CharacterSelection1으로 이동
    public void CharacterSelection2ClickBackBtn(){
        SceneManager.LoadScene("CharacterSelection1");
    }

    //게임 클리어 or 게임 오버 화면에서 메인화면 버튼 클릭시 MainTitle로 이동
    public void Ending(){
        SceneManager.LoadScene("MainTitle");
    }

}
