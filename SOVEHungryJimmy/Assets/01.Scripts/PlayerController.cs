using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //스피드 조정 변수
    [SerializeField]
    private float walkSpeed; //걷기 속도
    [SerializeField]
    private float runSpeed; //달리기 속도
    private float applySpeed; //현재 적용중인 속도 / 이것 하나만 있어도 대입만 하면 됨...(여러개의 함수 쓸 필요없다)


    //상태 변수
    private bool isRun = false; //걷기인지 달리기인지 (false가 기본값)
    private bool isGround = true; //땅인지 아닌지
    private bool isActivated = true;
    private bool isJoy = false;

    //땅 착지 여부 /바닥에 닿았는지 여부를  확인할 콜라이더
    private CapsuleCollider capsuleCollider; //캡슐 콜라이더가 Mesh콜라이더와 맞닿아 있을 경우가 true임(지상)...

    //필요한 컴퍼넌트
    [SerializeField]
    private Camera theCamera; //camera component
    //플레이어의 실제 육체(몸) / 콜라이더로 충돌 영역 설정, 리지드바디로 콜라이더에 물리적 기능 추가
    private Rigidbody myRigid;
    private StatusController theStatusController;
    public Inventory inventory;


    // 조이스틱 가져오기
    [SerializeField] private Joystick moveJoystick;
    [SerializeField] private Joystick cameraJoystick;

    //필요한 사운드 이름
    [SerializeField]
    private string walk_Sound;


    public Animator animator; // 애니메이션
    public GameObject runningImage; //달리기 상태인지 알려주는 이미지



    void Start()
    {
        //하이어라키의 객체를 뒤져서 카메라 컴퍼넌트가 있다면 theCamera에 
        //찾아서 넣어주기 -> theCamera = FindObjectOfType<Camera>(); 
        //카메라가 여래개일 수 있으니 프로젝트창에 직접 드래그했음...

        capsuleCollider = GetComponent<CapsuleCollider>();
        //플레이어가 캡슐 콜라이더를 통제할 수 있도록 가져오기...
        //리지드바디 컴퍼넌트를 마이리지드 변수에 넣겠다
        myRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;  //달리기 전까지 기본속도는 걷기
        animator = gameObject.GetComponent<Animator>();

        theStatusController = FindObjectOfType<StatusController>();

    }

    void Update()
    {
        if (isActivated && GameManager.canPlayerMove)
        {
            IsGround();// isGround가 true인지 false인지 확인하는 함수 
            TryRun(); //뛰거나 걷는것을 구분하는 함수(판단 후 움직임 제어 / 순서주의)
            Move(); //키입력에 따라 움직임이 실시간으로 이루어지게하는 처리
            // CameraRotation(); // 상하 카메라 회전
        }

    }

    private void IsGround() //지면 체크
    {//고정된 좌표를 향해 y 반절의 거리만큼 (아래방향으로) 레이저 쏘기
     //-> 지면과 닿게 됨...isGround는 true를 반환해 점프할 수 있는 상태가 됨...
     //지면의 경사에 따라 오차가 생기는 것을 방지하기 위해 여유주기 /+0.1f/
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }

    public void TryRun() //달리기 시도
    {//달리기 버튼을 누르면 달릴 수 있도록...
        if (runningImage.activeInHierarchy) //달리기 버튼을 누르게 되면
        {
            Running();
        }
        if (!runningImage.activeInHierarchy) //달리기 버튼에서 손을 떼면
        {
            RunningCancel();
        }
    }


    private void Running() //달리기 실행
    {
        theStatusController.DecreaseStamina(1f * Time.deltaTime);    // 달리는 중일때 (1초에 1씩) 스태미너 값 깎음

        Vector2 moveInput = new Vector2(moveJoystick.horizontal, moveJoystick.vertical);
        bool isRun = moveInput.magnitude != 0;
        applySpeed = runSpeed; //스피드가 RunSpeed로 바뀜

        animator.SetBool("isRun", isRun);
    }

    private void RunningCancel() //달리기 취소
    {
        isRun = false;
        applySpeed = walkSpeed; //걷는 속도
        animator.SetBool("isRun", isRun);
    }

    private void Move() //움직임 실행
    {//상하좌우...move...
     
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        //벡터값을 이용해 실제 움직이도록...
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;
        //normalized를 써서 x와 z값의 합이 1이 나오도록 한다
        //계산을 편하게 하기 위해 합이 1이 나오도록 정규화 하는 것이 나음...

        //나온 값을 myRigid에 무브포지션 메서드를 이용해 움직이도록 구현
        //현위치에서 velocity만큼 움직임 / 순간이동하듯 움직이는 것을 방지하기 위해
        //타임.델타타임으로 쪼개준다
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    // private void Move() //움직임 실행
    // {
    //     Vector2 moveInput = new Vector2(moveJoystick.horizontal, moveJoystick.vertical);
    //     bool isMove = moveInput.magnitude != 0;
    //     animator.SetBool("isMove", isMove);

    //     if (isMove)
    //     {
    //         Vector3 lookForward = new Vector3(theCamera.transform.forward.x, 0f, theCamera.transform.forward.z).normalized;
    //         Vector3 lookRight = new Vector3(theCamera.transform.right.x, 0f, theCamera.transform.right.z).normalized;
    //         Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

    //         gameObject.transform.forward = moveDir; // 캐릭터가 바라보는 정면은 입력된 방향에 맞춰 바라본다. 
    //         transform.position += moveDir * Time.deltaTime * applySpeed;    // 입력된 방향에 속도를 곱해줌

    //         //SoundManager.instance.PlaySE(walk_Sound); //walk sound
    //     }
    // }

    private void OnTriggerEnter(Collider other)     // 콜라이더와 충돌하면
    {
        if (other.tag == "Box")     // 충돌체의 태그가 Box일때
        {
            StartCoroutine(Joy());      // Joy코루틴 실행
        }
        if (other.tag == "EndingSpot")      // 충돌체의 태그가 EndingSpot일때
        {
            CheckBoat();        // CheckBoat 실행
        }
    }
    public void CheckBoat()     // 인벤토리에 보트확인
    {
        for (int i = 0; i < inventory.slots.Length; i++)        // 인벤토리의 슬롯의 길이만큼 for문 실행
            {
                if (inventory.slots[i].item != null)        // 슬롯에 아이템이 있고
                {
                    if (inventory.slots[i].item.itemName == "Boat")      // 그 아이템의 이름이 Boat일때
                    {
                        new WaitForSeconds(3f);     // 3초후
                        SceneManager.LoadScene("endding");      // endding씬 이동
                    }
                }
            }
    }

    IEnumerator Joy()       // 기뻐하는 애니메이션 
    {
        isJoy = true;       // isJoy true상태로 변경
        animator.SetBool("isJoy", isJoy);       // isJoy애니메이션 실행
        yield return new WaitForSeconds(2f);        // 2초간 지연
        animator.SetBool("isJoy", false);       // isJoy애니메이션 끄기
        isJoy = false;      // isJoy false로 변경
    }
}
