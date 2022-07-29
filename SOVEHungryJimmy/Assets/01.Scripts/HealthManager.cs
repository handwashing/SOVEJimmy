using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    [SerializeField] private StatusController theStatus;
    [SerializeField] private PlayerController thePlayer;

    //애니메이션
    public Animator animator;
    private bool isDead = false;        // isDead 상태확인
    private bool isDancing = false;         // isDancing 상태확인
    private bool isEnd = false;     // isEnd 상태확인
    private bool isRainPose = false;        // isRainPose 상태확인

    public GameObject rainPrefab; //비 효과
    public GameObject deadPanel;

    void Start()
    {
        animator = thePlayer.GetComponent<Animator>();      // 플레이어의 Animator가져옴
    }

    void Update()
    {
        if (!isDead)
        {
            Dead();
            if(rainPrefab.activeInHierarchy) //살아있는 상태에서 비가 온다면...
            {
                Dancing();
            }
        }

    }


IEnumerator DeadAction()        // 죽었을때 코루틴
{
    GameManager.isPause = true;
    animator.SetTrigger("isDead");      // isDead애니메이션 실행
    isDead = true;
    deadPanel.SetActive(true);      // deadPanel 활성화
    yield return new WaitForSeconds(6f);        // 지연시간
    Time.timeScale = 0f;        // 시간흐름 0
    SceneManager.LoadScene("Select");       // Select씬 로드

}

    public void Dead()      // 사망조건
    {
        if (theStatus.currentHungry <= 0 || theStatus.currentThirsty <= 0)      // currentHungry나 currentThirsty 0이하일때
        {
            StartCoroutine(DeadAction());
        }
    }

    public void Dancing() //댄싱 애니메이션
    {       
        StartCoroutine(Dance()); //댄싱 코루틴 실행       
    }

    IEnumerator Dance()
    {
        
        isDancing = true; //파라미터 값이 isDancing이면....
        animator.SetBool("isDancing", isDancing); //비맞는 애니메이션 실행
        yield return new WaitForSeconds (1f); //1초 정도 대기 후
        isRainPose = true; //파라미터 값 isRainPose
        animator.SetBool("isDancing", false); //댄싱 애니메이션 중지
        animator.SetBool("isRainPose",isRainPose); //비맞는 포즈 애니메이션 실행
        yield return new WaitForSeconds (9f); //9초 정도 대기 후
        isEnd = true; //비맞는 애니메이션도 중지
        animator.SetBool("isEnd",isEnd);

    }

}
