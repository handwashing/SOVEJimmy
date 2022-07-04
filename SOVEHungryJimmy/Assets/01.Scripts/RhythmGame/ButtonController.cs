using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{//(현재 방향키 기반)버튼키를 관리하는 스크립트
    private SpriteRenderer theSR; //스프라이트 렌데러에 엑세스 할
    public Sprite defaultImage; //버튼이 눌리지 않았을 때 기본 이미지 
    public Sprite pressedImage; //버튼이 눌렸을 때의 이미지

    public KeyCode keyToPress; //버튼을 활성화시킬 키

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>(); //theSR을버튼 컨트롤러와 동일한 객체에 있는 스프라이트 렌더러로 할당
        
         //(색깔)버튼을 활성활할 때 실제로 어떤 버튼을 눌러야 하는가
         //버튼을 활성활할 때 어떤 버튼이 실제로 눌렸는가
         //키를 눌렀을때 버튼이 활성화되도록 하기
         
    }

    void Update()
    {
        //키 입력 여부를 확인하기
        if(Input.GetKeyDown(keyToPress))
        {
            theSR.sprite = pressedImage; //버튼을 누르면 (버튼을)눌렸을 때의 이미지로 바꿔주기
        }

        if(Input.GetKeyUp(keyToPress))
        {
            theSR.sprite = defaultImage; //버튼에서 손을 떼면 기본 이미지로 다시 바꿔주기
        }
    }
}

