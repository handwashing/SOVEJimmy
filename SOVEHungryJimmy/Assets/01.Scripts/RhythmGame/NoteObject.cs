using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{//Arrow가 떨어질 때 옳은 위치의 버튼을 누르면 Arrow가 사라짐 / Note 의 Hit과 Miss를 체크해 저장
    public bool canBePressed; //버튼이 눌렸는지?

    public KeyCode keyToPress; //키 정보

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {//만약 keyToPress상태이면 canBePressed상태가 true일지 체크하겠다...
        if(Input.GetKeyDown(keyToPress)) 
        {
            if(canBePressed)
            {
                gameObject.SetActive(false); //true상태면 오브젝트가 옳은 위치에 있다는 것 /디폴트로 돌리는 것

                RhythmGameManager.instance.NoteHit(); //RhythmGameManager에 있는 NoteHit함수를 가져다 쓴다
            }
        }
    }

    private void OnTriggerEnter(Collider other) //영상에서는 Collider2D 사용 / 다른 콜라이더를 가진(태그 된)물체와 닿았을 때 / 딱 한 번만 감지
    {   //만약 다른 콜라이더의 태그가 "Activator"라면...
        //canBePressed 할 수 있는 상태인지 체크
        if(other.tag == "Activator")
        {
            canBePressed = true; //위의 상태면 버튼을 누를 수 있는 
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Activator")
        {
            canBePressed = false;

            RhythmGameManager.instance.NoteMissed();
        }
    }
}
