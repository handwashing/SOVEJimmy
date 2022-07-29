using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RhythmGameManager : MonoBehaviour
{
    public AudioSource theMusic; //오디오 소스

    public bool startPlaying; //게임이 시작되었는지...

    public BeatScroller theBS; // BeatScroller 받아오기

    public static RhythmGameManager instance; //이 자체로 인스턴스화

    public int currentScore; //현재 점수
    public int scorePerNote = 100; //(한 노트 당) 추가 될 점수

    public TextMeshProUGUI scoreText; //화면에 표시 될 점수 텍스트

    public float totalNotes; //적중된 총 노트들
    public float normalHits; //적중
    public float missedHits; //놓침

    public GameObject rgstButton; //리듬게임(기우제) 버튼
    public GameObject rainButton; //비를 내리게 하는 버튼(일정 점수 이상 획득 시)
    public GameObject backButton; //메인 화면으로 돌아가는 버튼 (일정 점수 이상 획득 x)
    public GameObject gamePanel; //게임 패널 오브젝트
    public GameObject resultsScreen; //점수 요소 가져다쓰려고 참조 쓰기 /점수 팝업으로 점수 보여주기
    public TextMeshProUGUI normalsText, missesText, rankText, finalScoreText; //점수판에 나타날 각 점수들


    void Start()
    {
        //음악 재생과 바 떨어뜨리기를 시작할 때 게임매니저를 컨트롤 
        instance = this; 

        scoreText.text = "0"; //처음 시작할 때 점수 = 0으로 

        totalNotes = FindObjectsOfType<NoteObject>().Length;
        //Arrow컴퍼넌트에 이미 NoteObject 스크립트가 있음 -> 여기서 Length 가져오기
        //Length가 노트가 얼마나 찍혔는지 알려줄 것
    }


    void Update()
    {
        //startPlaying = false일때 시작
        if(!startPlaying) 
        {
            if(gamePanel.activeInHierarchy) //gamePanel이 활성화 되면
            {
                startPlaying = true; //게임이 시작이 되고
                theBS.hasStarted = true; //BeatScroller또한 시작

                //버튼을 눌렀을 때 음악이 플레잉 됨
                theMusic .Play();
            }
        }
        else
            {
                if(!theMusic.isPlaying && !resultsScreen.activeInHierarchy && gamePanel.activeInHierarchy) //음악이 플레이 되지 않고, 점수판이 활성화 되지 않았다면...
                {
                    resultsScreen.SetActive(true); //점수판 활성화           
                    normalsText.text = normalHits.ToString(); //적중 개수 나타내기
                    missesText.text = missedHits.ToString(); //놓침 개수 나타내기

                    float totalHit = normalHits; //총 점수 받아오기

                    string rankVal = "Fail_!"; //default rank value

                    if(normalHits > 4f) //4개 초과 적중 시켰을 경우
                    {
                        rankVal = "Success_!"; //성공
                        rainButton.SetActive(true); //비를 내릴 수 있는 버튼 활성화
                    }

                    else if(normalHits < 5f) //5개 미만 적중
                    {
                        rankVal = "Fail_!"; //실패
                        backButton.SetActive(true); //메인 화면으로 돌아가는 버튼 활성화           
                    }

                    rankText.text = rankVal; //점수판에 나타날 문구(성공 여부)

                    finalScoreText.text = currentScore.ToString(); //최종 점수
                }
            }
    }

    public void NoteHit() //적중 여부
    {
        Debug.Log("Hit On Time");

        scoreText.text = "" + currentScore; //(점수가 추가된) 현재 점수 표시
    }

    public void NormalHit() //적중
    {
        currentScore += scorePerNote;
        NoteHit();

        normalHits++; //총 스코어인 NormalHits에 1 합산
    }

    public void NoteMissed() //놓침
    {
        Debug.Log("Missed Note");

        missedHits++; //총 스코어인 missedHits에 1 합산
    }
}
