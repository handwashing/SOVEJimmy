using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour
{
    [SerializeField]
    private float time_start; //타이머가 시작했을 때 플레이타임
    [SerializeField]
    private float time_current; //타이머 현재 시간 (time.Time - time_start)
    private float time_Max = 10f; //타이머 최종 시간
    private bool isEnded; //타이머 종료 확인
    private PlayerController thePlayerController;


    private void Check_Timer() //타이머 시간 검사
    {
        time_current = Time.time - time_start;
        if (time_current < time_Max);
        else if (!isEnded)
        {
            End_Timer(); //타이머 종료 시 실행
        } 
    }
    private void End_Timer()
    {
        Debug.Log("End");
        time_current = time_Max;
        isEnded = true;
        gameObject.SetActive(false);
    }
    private void Reset_Timer()
    {
        time_start = Time.time;
        time_current = 0;
        isEnded = false;
        gameObject.SetActive(true);
        Debug.Log("Start");
    }    
    
    void Start()
    {
        Reset_Timer(); //리셋
    }

    void Update()
    {
        if (isEnded)return;
        Check_Timer();
    }
}
