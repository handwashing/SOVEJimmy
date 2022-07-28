using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private GameObject go_BaseUI;      // 메뉴 UI
    [SerializeField] private SaveNLoad theSaveNLoad;

    // Start is called before the first frame update
    void Update()
    {

    }

    public void CallMenu()     // 메뉴 호출
    {
        GameManager.isPause = true;     // GameManager의 isPause가 true면
        go_BaseUI.SetActive(true);      // 메뉴 UI활성화
        Time.timeScale = 0f;        // 시간흐름이 0이 되게 = 정지시킴
    }

    public void CloseMenu()        // 메뉴 닫기
    {
        GameManager.isPause = false;
        go_BaseUI.SetActive(false);
        Time.timeScale = 1f;        // 다시 정상속도로 시간흐르게
    }

    public void ClickSave()
    {
        Debug.Log("세이브 버튼 클릭");
        theSaveNLoad.SaveData();
    }

    public void ClickExit()
    {
        Debug.Log("게임종료");
        Application.Quit();     // 어플리케이션 종료
    }
}
