using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloseButton : MonoBehaviour
{
    public Popup popupWindow;// 팝업창 오브젝트
    // Start is called before the first frame update
    public void OnButtonClick()
    {
        var seq = DOTween.Sequence();//아래 세개의 스케일 변경을 순서대로 실행시키기 위한 함수

        seq.Append(transform.DOScale(0.95f, 0.1f));//0.1초안에 스케일값 변경
        seq.Append(transform.DOScale(1.05f, 0.1f));//0.1초안에 스케일값 변경
        seq.Append(transform.DOScale(1f, 0.1f));//0.1초안에 스케일값 변경

        seq.Play().OnComplete(() => {//OnComplete 는 seq 에 설정한 애니메이션의 플레이가 완료되면 { } 안에 있는 코드가 수행된다는 의미
            popupWindow.Hide();
        });
    }
}
