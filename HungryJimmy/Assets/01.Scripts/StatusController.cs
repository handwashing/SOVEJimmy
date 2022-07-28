using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    // 체력
    [SerializeField]
    private int stamina; //체력의 총량
    public float currentStamina; //나의 현재 체력

    // 체력이 줄어드는 속도
    [SerializeField]
    public int staminaDecreaseTime; //staminaDecreaseTime만큼 채워져야 스태미너가 1씩 깎임
    private int currentStaminaDecreaseTime; // 실시간으로 체력이 줄어드는 정도를 측정

    // 배고픔
    [SerializeField]
    private int hungry;     // 배고픔
    public int currentHungry;       // 현재 배고픔

    // 배고픔이 줄어드는 속도
    [SerializeField]
    private int hungryDecreaseTime;     // 지정해둔 시간
    private int currentHungryDecreaseTime;      // 실시간으로 줄어들 시간

    // 목마름
    [SerializeField]
    private float thirsty;      // 수분
    public float currentThirsty;        // 현재 수분

    // 목마름이 줄어드는 속도
    [SerializeField]
    private int thirstyDecreaseTime; //지정해둔 속도
    private int currentThirstyDecreaseTime; //계속 변하는 양(시간)

    // 필요한 이미지
    [SerializeField]
    private Image[] images_Gauge;

    private bool isDead = false;
    private HealthManager theHealth;

    public GameObject rainPrefab; // 비 내리는 파티클 이펙트 오브젝트 
    public GameObject bonFire; //장작 오브젝트

    public bool isRain = false; // 비가 오는지 확인
    
    public GameObject sun; //낮 상태인지 알려주는 이미지
    public GameObject moon; //밤 상태인지 알려주는 이미지

    private const int HUNGRY = 0, THIRSTY = 1, STAMINA = 2;

    void Start()
    {
        currentStamina = stamina;
        currentHungry = hungry;
        currentThirsty = thirsty;
    }


    void Update()
    {
        if (!isDead)
        {
            Hungry();
            Thirsty();
            Stamina();
            GaugeUpdate();
        }
    }

    private void Hungry()       // 배고픔 구현
    {
        if (currentHungry > 0)      // 현재 배고픔이 0보다 클 경우에만 깎음
        {
            if (currentHungryDecreaseTime <= hungryDecreaseTime)    // currentHungryDecreaseTime가 hungryDecreaseTime도달하지못하면 
                currentHungryDecreaseTime++;        // currentHungryDecreaseTime 증가
            else        // currentHungryDecreaseTime가 hungryDecreaseTime도달하면 
            {
                currentHungry--;        // currentHungry -1
                currentHungryDecreaseTime = 0;      // currentHungryDecreaseTime리셋
            }
        }
        else        // 0보다 작아졌을때
            theHealth.Dead();       // HealthManager의 Dead함수 실행
    }

    private void Thirsty()      // 목마름 구현
    {
        if (currentThirsty > 0)
        {
            if (currentThirstyDecreaseTime <= thirstyDecreaseTime)
                currentThirstyDecreaseTime++; //1씩 계속 증가
            else
            {
                currentThirsty--;
                currentThirstyDecreaseTime = 0;
            }
            if(!isRain) //비오는 상태가 아니라면
            {
                Raining(); //Raining 실행
                isRain = false; 
            }
        }
    }

    private void Stamina()       // 체력 구현
    {
        if (currentStamina > 0)      // 현재 체력이 0보다 클 경우에만 깎음
        {
            if (currentStaminaDecreaseTime <= staminaDecreaseTime)
                currentStaminaDecreaseTime++;
            else
            {
                currentStamina--;
                currentStaminaDecreaseTime = 0; //기본값
            }

        }
    }

    private void Raining() //비 내리기
    {
        if (rainPrefab.activeInHierarchy) //하이어라키 창에 비 프리팹이 활성화 되었다면
        {
            isRain = true; //isRain 활성화
            currentThirsty += 3f * Time.deltaTime; //총 30의 수분 주기(비가 10초동안 옴)
        }
    }

    public void ColdNight() //night상태에서
    {
        if(!bonFire.activeInHierarchy && moon.activeInHierarchy) //밤인데 모닥불이 활성화되어 있지 않다면
        {
           staminaDecreaseTime = 500; //밤일때 체력이 더 빨리 사라지게 (밤일때 체력이 떨어지는 속도)
        }

        
    }

    public void BonDay() //day 상태에서
    {
        staminaDecreaseTime = 1000; //낮일때 체력이 떨어지는 속도
    }

    private void GaugeUpdate()      // 상태 수치 변화 시각화
    {
        images_Gauge[STAMINA].fillAmount = (float)currentStamina / stamina;
        images_Gauge[HUNGRY].fillAmount = (float)currentHungry / hungry;
        images_Gauge[THIRSTY].fillAmount = (float)currentThirsty / thirsty;
    }

    // stamina 회복 (아이템 사용시)
    public void IncreaseStamina(int _count)
    {
        if (currentStamina + _count < stamina)        // currentStamina와 회복될 수치를 더했을때 stamina가 넘는가?
        {
            currentStamina += _count;
        }
        else
            currentStamina = stamina;
    }

    // Stamina 감소
    public void DecreaseStamina(float _count)       // 감소시킬 값
    {
        currentStamina -= _count;       

        if (currentStamina <= 0)     // currentStamina 0 이하가 되면 움직임이 매우 느려짐
            Debug.Log("캐릭터의 stamina가 0이 되었습니다!!");
    }

    public void IncreaseThirsty(int _count)     // 수분 감소 처리를 하고 싶을때
    {
        if (currentThirsty + _count < thirsty)      // currentThirsty에 입력값을 더했을때 thirsty보다 작으면
        {
            currentThirsty += _count;       // currentThirsty에 입력값 더해줌
        }
        else        // currentThirsty에 입력값을 더했을때 thirsty보다 크면 
            currentThirsty = thirsty;       // 현재값을 기존thirsty값으로 바꿔줌
    }

    // Hungry 증가
    public void IncreaseHungry(int _count)      // 배고픔 감소 처리를 하고 싶을때
    {
        if (currentHungry + _count < hungry)        // currentHungry에 입력값을 더했을때 hungry보다 작으면
        {
            currentHungry += _count;        // currentHungry에 입력값 더해줌
        }
        else        // currentHungry에 입력값을 더했을때 hungry보다 크면 
            currentHungry = hungry;     // 현재값을 기존hungry값으로 바꿔줌
    }

    // Hungry 감소
    public void DecreaseHungry(int _count)      // 감소시킬 값
    {
        currentHungry -= _count;        // 현재 배고픔에서 받아온 값만큼 감소
    }


}
