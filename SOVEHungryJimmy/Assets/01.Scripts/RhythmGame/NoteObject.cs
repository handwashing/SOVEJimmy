using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{//Arrow가 떨어질 때 옳은 위치의 버튼을 누르면 Arrow가 사라짐
    public bool canBePressed;

    public KeyCode keyToPress;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {//만약 KeyDown 상태이면 canBePressed상태가 true일지 체크하겠다...
        if(Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {
                gameObject.SetActive(false); //true상태면 오브젝트가 옳은 위치에 있다는 것
            }
        }
    }

    private void OnTriggerEnter(Collider other) //영상에서는 Collider2D 사용
    {//만약 다른 콜라이더의 태그가 "Activator"라면...
    //canBePressed 할 수 있는 상태인지 체크
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit(Collider other) //영상에서는 Collider2D 사용
    {//만약 다른 콜라이더의 태그가 "Activator"라면...
    //canBePressed 할 수 있는 상태인지 체크
        if(other.tag == "Activator")
        {
            canBePressed = false;
        }
    }
}
