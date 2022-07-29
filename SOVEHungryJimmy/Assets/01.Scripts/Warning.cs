using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warning : MonoBehaviour
{
    //경고 패널
    [SerializeField] private GameObject hungry_yellow_Panel;        // 배고픔 경고패널
    [SerializeField] private GameObject hungry_red_Panel;       //  배고픔 위험패널
    [SerializeField] private GameObject thirsty_yellow_Panel;        // 목마름 경고패널
    [SerializeField] private GameObject thirsty_red_Panel;      // 목마름 위험패널


    //필요한 사운드 이름
    [SerializeField]
    private string weakWarning_Sound; //1번째 경고음
    [SerializeField]
    private string halfEmergencyWarning_Sound; //2번째 경고음

    //지미의 현재 상태를 알려주는 이미지 오브젝트
    public GameObject healthyJim; //건강한 지미 상태 이미지
    public GameObject sickJim; //아픈 지미 상태 이미지
    public GameObject soSickJim; //매우 아픈 지미 상태 이미지
    public GameObject deadJim; //죽은 지미 이미지



    bool h_yellowUI = false;      //아이템으로 회복할때마다 이거 처리 해줘야 계속 경고창 나타남
    bool h_redUI = false;
    bool t_yellowUI = false;
    bool t_redUI = false;


    public StatusController theStatus;
    public GameObject rgstButton; //리듬게임(기우제) 버튼

    void Update()
    {
        if (theStatus.currentHungry < 50)       // theStatus의 currentHungry가 50 미만이며
        {
            if (theStatus.currentHungry > 30 && !h_yellowUI)        // theStatus의 currentHungry가 30 초과이며 h_yellowUI가 false일때
            {
                healthyJim.SetActive(false); //건강한 지미 이미지 비활성화
                sickJim.SetActive(true); //지미의 상태 이미지를 아픈 상태로 변경

                h_yellowUI = true;      // 경고창 상태 true
                StartCoroutine(ShowHYellowPanel());     // ShowHYellowPanel 코루틴 실행
            }
            if (theStatus.currentHungry < 15)       // theStatus의 currentHungry가 15 미만이고
            {
                if (!h_redUI)       // h_redUI가 false일때
                {
                    sickJim.SetActive(false);
                    soSickJim.SetActive(true);//지미의 상태 이미지를 매우 아픈 상태로 변경

                    h_redUI = true;     // 위험창 상태 true

                    StartCoroutine(ShowHRedPanel());        // ShowHRedPanel 코루틴 실행

                }

            }
            else        // theStatus의 currentHungry가 15 초과
            {
                h_redUI = false;
            }
        }
        else
        {
            h_yellowUI = false;
        }

        if (theStatus.currentThirsty < 50)      // theStatus의 currentThirsty 50 미만이며
        {
            if (theStatus.currentThirsty > 30 && !t_yellowUI)       // theStatus의 currentThirsty 30 초과이며 t_yellowUI가 false일때
            {
                if (!rgstButton.activeInHierarchy)      // rgstButton이 Hierarchy창에서 비활성화 됐다면
                {
                    rgstButton.SetActive(true);         // 리듬게임 버튼 활성화
                    t_yellowUI = true;          // 경고창 상태 true
                    StartCoroutine(ShowTYellowPanel());     // ShowTYellowPanel코루틴 실행
                }
            }

            if (theStatus.currentThirsty < 15) //목마름 지수가 15보다 낮을 때
            {
                if (!t_redUI)
                {
                    t_redUI = true;
                    StartCoroutine(ShowTRedPanel());
                }
            }
            else
            {
                t_redUI = false;

            }
        }
        else
        {
            t_yellowUI = false;
        }
    }

    IEnumerator ShowHYellowPanel()      // ShowHYellowPanel 코루틴
    {
        hungry_yellow_Panel.SetActive(true);        // hungry_yellow_Panel활성화
        yield return new WaitForSeconds(1.5f);      // 지연시간
        hungry_yellow_Panel.SetActive(false);       // hungry_yellow_Panel비활성화

        SoundManager.instance.PlaySE(weakWarning_Sound); //warning sound play
    }

    IEnumerator ShowHRedPanel()
    {
        hungry_red_Panel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        hungry_red_Panel.SetActive(false);

        SoundManager.instance.PlaySE(halfEmergencyWarning_Sound); //warning sound play
    }

    IEnumerator ShowTYellowPanel()
    {
        thirsty_yellow_Panel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        thirsty_yellow_Panel.SetActive(false);

        SoundManager.instance.PlaySE(weakWarning_Sound); //warning sound play
    }

    IEnumerator ShowTRedPanel()
    {
        thirsty_red_Panel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        thirsty_red_Panel.SetActive(false);

        SoundManager.instance.PlaySE(halfEmergencyWarning_Sound); //warning sound play
    }


}
