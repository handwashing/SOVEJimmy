using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{//떨어지는 arrow들을 관리하는 스크립트
    public float beatTempo;  //arrow가 얼마나 빨리 떨어질지를 관리
    public bool hasStarted; //버튼을 누르면 arrow가 화면 아래로 떨어지도록...(버튼이 눌렸나 체크하는 것)

    void Start()
    {//설정한 비트 템포를 2로 나누기
        beatTempo = beatTempo / 2f;
    }

    void Update()
    {
        //hasStarted가 되어있지 않다면...        
        if(!hasStarted)
        {
            //버튼이 눌렸는지 체크하고 없다면
            if(Input.anyKeyDown) //입력을 할 때 아무 키나 입력하면
            {   
                hasStarted = true; //버튼이 눌려있다면 hasStarted상태인 것...
            }
        }
        else 
        //화살키가 얼마나 빨리 움직일지를 나타낼 것
        {
            //비트 템포에 시간을 곱해 모든 프레임에 나타나지 않도록
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
        
    }
 }
