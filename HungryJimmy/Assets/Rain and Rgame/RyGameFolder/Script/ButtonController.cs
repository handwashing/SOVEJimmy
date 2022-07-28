using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{//(현재 방향키 기반)버튼키를 관리하는 스크립트
    private SpriteRenderer theSR; //스프라이트 렌데러에 엑세스하도록 관리

    public KeyCode keyToPress; //버튼을 활성화시킬 키

    void Start()
    {
        //theSR을 버튼 컨트롤러와 동일한 객체에 있는 스프라이트 렌더러로 할당
        theSR = GetComponent<SpriteRenderer>(); 

    }
}

