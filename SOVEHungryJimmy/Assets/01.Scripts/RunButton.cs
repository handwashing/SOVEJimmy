using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunButton : MonoBehaviour
{
    bool runBtn; //run 버튼
    public GameObject runningSt;  //달리기 상태인지 알려주는 이미지

    public void TryRun() //달리기 시도
    {
        if (runBtn == false) //기본값인지 확인
            {
                this.runBtn = true; //버튼이 눌린 상태면
                runningSt.SetActive(true); //상태 표시 이미지 활성화
            }
            else
            {
                this.runBtn = false; //그 반대면
                runningSt.SetActive(false); //상태 표시 이미지 비활성화
            }
    }
}
