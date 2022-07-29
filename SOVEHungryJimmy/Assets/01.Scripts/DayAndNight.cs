using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    [SerializeField] private float secondPerRealTimeSecond; //게임 세계의 100초 =  현실 세계의 1초

    [SerializeField] private bool isNight = false; //밤인지..아닌지...

    [SerializeField] private float fogDensityCalc; //증감량 비율

    [SerializeField] private float nightFogDensity; //밤 상태의 Fog 밀도
    private float dayFogDensity; //낮 상태의 Fog 밀도
    private float currentFogDensity; //계산
    public StatusController theStatus; // StatusController 가져오기
    public GameObject bonFire; //모닥불 오브젝트
    public GameObject sun; //낮 상태인지 알려주는 이미지
    public GameObject moon; //밤 상태인지 알려주는 이미지


    void Start()
    {
        dayFogDensity = RenderSettings.fogDensity; //dayFogDensity에 현재값 주기
    }

    void Update()
    {//태양의 엑스축을 증가시켜 낮,밤 바꾸기 / 태양이 특정 각도로 기울어지면 낮,밤이 되도록 조정
        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime);
        
        if (transform.eulerAngles.x >= 170 && !isNight) 
        // 태양이 특정 각도로 기울어지면 낮이 된다
        //밤이 되면 장작불을 지펴서 체온과 스태미너를 유지할 수 있습니다.
            {
                isNight = true; //밤
                sun.SetActive(false); //낮 상태 이미지 비활성화
                moon.SetActive(true); //밤 상태 이미지 활성화
                theStatus.ColdNight(); //스태미너가 더 빨리 닳도록...
            }
        else if (transform.eulerAngles.x >= 10 && transform.eulerAngles.x < 170 && isNight) // 태양이 특정 각도로 기울어지면 밤이 된다
        {
            isNight = false; //낮
            GameManager.instance.AddDate(1); //화면상의 Day(생존일 ) +1
            moon.SetActive(false); //밤 상태 이미지 비활성화
            sun.SetActive(true); //낮 상태 이미지 활성화
            theStatus.BonDay();//스태미너가 느리게 닳도록
        }

        if (isNight) //밤일 경우
        {
            if (currentFogDensity <= nightFogDensity) //밤이여도 적당히 보이도록 nightFogDensity 이하일때만 실행
            {
                currentFogDensity += 0.1f * fogDensityCalc * Time.deltaTime; //특정시간만큼 계속 증가시키기
                RenderSettings.fogDensity = currentFogDensity; //위에 계산한 값을 실제 반영    

            }
        }
        else //낮일 경우
        {

            if (currentFogDensity >= dayFogDensity)
            {
                currentFogDensity -= 0.1f * fogDensityCalc * Time.deltaTime; //특정시간만큼 계속 감소시키기
                RenderSettings.fogDensity = currentFogDensity; //위에 계산한 값을 실제 반영  
            }


        }

    }

}