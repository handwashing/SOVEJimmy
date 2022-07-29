using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingAxe : MonoBehaviour
{
    public Animator playerAnim; //player animator
    bool swingPossible; //swing controller 스윙(어택) 제어
    protected RaycastHit hitInfo;  //Raycast에 닿은 정보를 hitInfo에 저장
    public GameObject pickAxe; //바위용 도끼
    public GameObject treeAxe; //나무용 도끼
    
    public void Attack()
    {
        playerAnim.Play("SwingAxe"); //(첫번째) 도끼 휘두르는 애니메이션
        TryAttack(); //TryAttack 실행
    }

    //Raycast에 충돌한 것이 있는지 체크
    protected bool CheckObject() 
    {   //충돌한것이 있다면...
        if(Physics.Raycast(transform.position, transform.forward, out hitInfo))
        {
            return true; //충돌한 게 있음
        }
        return false; //충돌한 게 없음
    }

    public void SwingPossible() //Swing 가능
    {
        swingPossible = true;
    }

    //Swing Axe (스윙)공격
     public void Swing() 
     {   

     }

    //공격(스윙)의 리셋 기능
    public void SwingReset()
    {
        swingPossible = false;

    }

    protected void TryAttack() //공격 시도
    {
        if (CheckObject()) //오브젝트 체크
        {
            if(hitInfo.transform.tag == "Rock" && pickAxe.activeInHierarchy) //pickAxe른 든 상태로 바위와 부딪혔을 경우
            {//Rock 클래스 안의 Mining을 호출
                hitInfo.transform.GetComponent<Rock>().Mining(); 
            } 

            else if(hitInfo.transform.tag == "FruitTree" && treeAxe.activeInHierarchy) //treeAxe른 든 상태로 나무와 부딪혔을 경우
            {//FruitTree 클래스 안의 HitFruit 호출  / FineObjectOfType을 통해 얻어와도 된다
                hitInfo.transform.GetComponent<FruitTree>().HitFruit(); 
            }

            else if(hitInfo.transform.tag == "Tree" && treeAxe.activeInHierarchy) //treeAxe른 든 상태로 나무와 부딪혔을 경우
            {//Tree 클래스 안의 Hit 호출  / FineObjectOfType을 통해 얻어와도 된다
                hitInfo.transform.GetComponent<Tree>().Hit(); 
            }
            Debug.Log(hitInfo.transform.name);
        }

    }
}
