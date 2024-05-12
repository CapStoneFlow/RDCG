using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Popup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.one * 0.1f;//크기를 0.1로 초기화
        gameObject.SetActive(false);//처음에는 팝업창이 안보이게 하기 위함
    }

    public void Show()//팝업창이 나오는 함수
    {
        gameObject.SetActive(true);

        var seq = DOTween.Sequence();//아래 함수를 저장할 공간

        seq.Append(transform.DOScale(1.1f, 0.2f));//크기를 1.1로 0.2초안에 수행한다
        seq.Append(transform.DOScale(1f, 0.1f));//크기를 1오 0.1초안에 수핸한다

        seq.Play();//저장된 함수를 순서대로 수행
    }
    // Update is called once per frame
    public void Hide()//팝업창이 없애는 함수
    {
        var seq = DOTween.Sequence();//아래 함수를 저장할 공간

        transform.localScale = Vector3.one * 0.2f;//크기 초기화

        seq.Append(transform.DOScale(1.1f, 0.1f));//크기를 1.1로 0.1초안에 수행한다
        seq.Append(transform.DOScale(0.2f, 0.2f));//크기를 0.2로 0.2초안에 수행한다


        seq.Play().OnComplete(() =>
        {
            gameObject.SetActive(false); //저장된 함수 수행 후 팝업창을 안보이게 하는 함수
        });


    }
}
