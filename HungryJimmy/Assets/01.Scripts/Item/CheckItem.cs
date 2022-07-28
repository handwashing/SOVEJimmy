using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItem : MonoBehaviour
{
    public Inventory inventory;     // 인벤토리 
    public GameObject axePrefab;        // 곡괭이 프리팹
    public GameObject treeaxePrefab;        // 돌도끼 프리팹

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckAxe()      // 인벤토리에 곡괭이가 들어있는지 확인할 함수
    {
        for (int i = 0; i < inventory.slots.Length; i++)        // 인벤토리의 슬롯의 길이만큼 for문 실행
        {
            if (inventory.slots[i].item != null)        // 슬롯에 아이템이 있고
            {
                if (inventory.slots[i].item.itemName == "Rockstalk")        // 그 아이템의 이름이 Rockstalk일때
                {
                    axePrefab.SetActive(true);      // 곡괭이 활성화
                }
            }
        }
    }

    public void CheckTreeAxe()      // 인벤토리에 돌도끼가 들어있는지 확인할 함수
    {
        for (int i = 0; i < inventory.slots.Length; i++)        // 인벤토리의 슬롯의 길이만큼 for문 실행
        {
            if (inventory.slots[i].item != null)        // 슬롯에 아이템이 있고
            {
                if (inventory.slots[i].item.itemName == "Ax Stone")        // 그 아이템의 이름이 Ax Stone일때
                {
                    treeaxePrefab.SetActive(true);      // 도끼 활성화
                }
            }
        }
    }

    public void Hand()      // 맨손으로 돌아갈 함수
    {
        treeaxePrefab.SetActive(false);     // 도끼 비활성화
        axePrefab.SetActive(false);     // 곡괭이 비활성화
    }

   
}
