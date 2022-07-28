using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;     // 싱글턴화
    
    public static bool canPlayerMove = true;        // 플레이어의 움직임 제어

    public static bool isOpenInventory = false;     // 인벤토리 활성화

    public static bool isPause = false;     // 메뉴가 호출되면 True

    public TextMeshProUGUI dateText; //날짜를 출력할 UI텍스트

    private int date = 1; //day

    // Update is called once per frame
    void Update()
    {
        if (isOpenInventory || isPause)     
        {
            canPlayerMove = false;      // 플레이어 움직임 제어
        }
        else
        {
            canPlayerMove = true;       
        }
    }
    
    private void Awake() {
    if( instance == null)       // instance null 이면
    {
        instance = this;        // instance 자기자신
    }
    else{
        Destroy(gameObject);
    }
}

    void Start()
    {

    }

    //생존 일수를 증가시키는 메서드
    public void AddDate(int newDate)
    {
        //sun의 transform x가 35면 아침으로 간주 / day + 1
        date += newDate;
        dateText.text = "Day " + date;
    }



}
