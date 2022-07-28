using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitBtn : MonoBehaviour
{
    public AudioSource theMusic; //오디오 소스
    public BeatScroller theBS; //Beat Scroller 받아오기

    public void Pause() //정지
    {
            theMusic.Pause(); //음악 중지하기
            theBS.beatTempo = 0f; //arrow 떨어지는 것 관리
    }
}
