using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{
    [SerializeField] private GameObject craft_base;      // 크래프트창 베이스 UI
    public GameObject axPrefab;     // 도끼프리팹
    public GameObject woodPrefab;       // 판자 프리팹
    public GameObject rockstalkPrefab;      // 곡괭이 프리팹
    public GameObject bonfirePrefab;        // 모닥불 프리팹
    public GameObject boatPrefab;       // 보트 프리팹
    public GameObject paddlePrefab;     // 패들 프리팹
    public GameObject ropePrefab;       // 밧줄 프리팹
    public GameObject clothPrefab;      // 천 프리팹
    public Inventory inventory;     // 인벤토리 선언

    //모닥불 오브젝트 관리
    public int lifetime = 15; //모닥불 지속 시간
    public GameObject moonImg; //밤 상태


    [SerializeField]
    private Transform targetTransform;      // 만들어진 아이템들이 나올 위치

    public void OpenCraft()     // 제작창 열기
    {
        craft_base.SetActive(true);     // 제작창 UI 활성화
    }
    public void CloseCraft()    // 제작창 닫기
    {
        craft_base.SetActive(false);        // 제작창 UI 비활성화
    }

    public void Wood()      // 판자 아이템 만들기 위한 함수
    {
        for (int i = 0; i < inventory.slots.Length; i++)        // 인벤토리의 슬롯의 길이만큼 for문 실행
        {
            if (inventory.slots[i].item != null)        // 슬롯에 아이템이 있고
            {
                if (inventory.slots[i].item.itemName == "Log")      // 그 아이템의 이름이 Log일때
                {
                    if (inventory.slots[i].itemCount >= 3)      // 아이템의 갯수가 필요한 갯수(3) 이상일 때
                    {
                        var itemGo = Instantiate<GameObject>(this.woodPrefab);      // woodPrefab 생성
                        itemGo.transform.position = this.targetTransform.transform.position + Vector3.forward * 0.5f;       // 지정해둔 위치
                        itemGo.SetActive(true);     // 생성된 아이템 활성화

                        inventory.slots[i].SetSlotCount(-3);        // 인벤토리창에서 아이템 차감
                    }
                    else
                    {
                        Debug.Log("나무는 있는데 수량이 부족해!");      // 수량 부족
                    }
                }

            }
            else
            {
                Debug.Log("나무 아이템이 없습니다.");       // 아이템이 없을때
            }
        }
    }

    public void Ax()        // 도끼 만들기 위한 함수
    {
        bool c = false;     // 한번에 다른 조건 확인해야 하므로 bool 줌
        bool d = false;

        for (int i = 0; i < inventory.slots.Length; i++)        // 인벤토리의 슬롯의 길이만큼 for문 실행
        {
            for (int j = 0; j < inventory.slots.Length; j++)        // 인벤토리의 슬롯의 길이만큼 for문 실행 (두번째 아이템)
            {
                if (inventory.slots[i].item != null)         // 슬롯에 아이템이 있고
                {
                    if (inventory.slots[i].item.itemName == "Log" && inventory.slots[i].itemCount >= 1)     // 그 아이템의 이름이 Log면서 아이템의 갯수가 필요한 갯수 이상일 때
                    {
                        c = true;
                        inventory.slots[i].SetSlotCount(-1);        // 인벤토리창에서 아이템 차감
                    }
                    Debug.Log("나무");
                    if (inventory.slots[j].item != null)        // 슬롯에 아이템이 있고
                    {
                        if (inventory.slots[j].item.itemName == "Rock" && inventory.slots[j].itemCount >= 1)    // 그 아이템의 이름이 Rock면서 아이템의 갯수가 필요한 갯수 이상일 때
                        {
                            {
                                d = true;       // 두번째 조건도 트루
                                inventory.slots[j].SetSlotCount(-1);        // 인벤토리창에서 아이템 차감
                            }
                        }

                    }
                }

            }

            if (c && d)     // 두 조건 모두 참이면
            {
                var itemGo = Instantiate<GameObject>(this.axPrefab);        // axPrefab 생성
                itemGo.transform.position = this.targetTransform.transform.position + Vector3.forward * 2f + Vector3.up * 0.5f;     // 지정된 위치
                itemGo.SetActive(true);     // 생성된 아이템 활성화

                c = false;      // 다시 false 줌
                d = false;
            }
        }
    }

        public void Pick()
        {
            bool a = false;
            bool b = false;

            for (int i = 0; i < inventory.slots.Length; i++)
            {
                for (int j = 0; j < inventory.slots.Length; j++)
                {
                    if (inventory.slots[i].item != null)
                    {
                        if (inventory.slots[i].item.itemName == "Log" && inventory.slots[i].itemCount >= 2)
                        {
                            a = true;
                            inventory.slots[i].SetSlotCount(-2);
                        }
                        if (inventory.slots[j].item != null)
                        {
                            if (inventory.slots[j].item.itemName == "Rock" && inventory.slots[j].itemCount >= 2)
                            {
                                b = true;
                                inventory.slots[j].SetSlotCount(-2);
                            }
                        }

                    }
                }

            }

            if (a && b)
            {
                var itemGo = Instantiate<GameObject>(this.rockstalkPrefab);
                itemGo.transform.position = this.targetTransform.transform.position + Vector3.forward * 2f + Vector3.up * 0.9f;
                itemGo.SetActive(true);

                a = false;
                b = false;
            }
        }

        public void BonFire()       // 모닥불 만들기 위한 함수
        {
            bool e = false;     // 조건식
            bool f = false;
            for (int i = 0; i < inventory.slots.Length; i++)        // 인벤토리의 슬롯의 길이만큼 for문 실행
            {
                for (int j = 0; j < inventory.slots.Length; j++)        // 인벤토리의 슬롯의 길이만큼 for문 실행 (두번째 아이템)
                {
                    if (inventory.slots[i].item != null)        // 슬롯에 아이템이 있고
                    {
                        if (inventory.slots[i].item.itemName == "Log" && inventory.slots[i].itemCount >= 3)     // 그 아이템의 이름이 Log면서 아이템의 갯수가 필요한 갯수 이상일 때
                        {
                            e = true;       // 조건식 true
                            inventory.slots[i].SetSlotCount(-3);    // 인벤토리창에서 아이템 차감
                        }
                        if (inventory.slots[j].item != null)        // 슬롯에 아이템이 있고
                        {
                            if (inventory.slots[j].item.itemName == "Rock" && inventory.slots[j].itemCount >= 2)        // 그 아이템의 이름이 Rock이면서 아이템의 갯수가 필요한 갯수 이상일 때
                            {
                                f = true;       // 조건식 true
                                inventory.slots[j].SetSlotCount(-2);      // 인벤토리창에서 아이템 차감  
                            }
                        }

                    }
                }

            }
            if (e && f)     // 두 조건 모두 참이면
            {
                if (moonImg.activeInHierarchy)      // moonImg가 Hierarchy창에서 활성화 됐을때
                {
                    var itemGo = Instantiate<GameObject>(this.bonfirePrefab);        // bonfirePrefab 생성
                    itemGo.transform.position = this.targetTransform.transform.position + Vector3.forward * 2f;     // 지정된 위치
                    itemGo.SetActive(true);     // 생성된 아이템 활성화

                    Destroy(itemGo, lifetime); //일정 시간이 지나면 모닥불 없애기

                    e = false;      // 다시 false로 돌려주기
                    f = false;
                }

            }


        }

        public void Boat()
        {
            bool g = false;
            bool h = false;

            for (int i = 0; i < inventory.slots.Length; i++)
            {
                for (int j = 0; j < inventory.slots.Length; j++)
                {
                    if (inventory.slots[i].item != null)
                    {
                        if (inventory.slots[i].item.itemName == "Woods" && inventory.slots[i].itemCount >= 4)
                        {
                            g = true;
                            inventory.slots[i].SetSlotCount(-4);
                        }
                        if (inventory.slots[j].item != null)
                        {
                            if (inventory.slots[j].item.itemName == "Rope" && inventory.slots[j].itemCount >= 4)
                            {
                                h = true;
                                inventory.slots[j].SetSlotCount(-4);
                            }
                        }

                    }
                }

            }

            if (g && h)
            {
                var itemGo = Instantiate<GameObject>(this.boatPrefab);
                itemGo.transform.position = this.targetTransform.transform.position + Vector3.forward * 5f;
                itemGo.SetActive(true);
                g = false;
                h = false;
            }

        }

        public void Paddle()
        {
            bool k = false;
            bool l = false;

            for (int i = 0; i < inventory.slots.Length; i++)
            {
                for (int j = 0; j < inventory.slots.Length; j++)
                {
                    if (inventory.slots[i].item != null)
                    {
                        if (inventory.slots[i].item.itemName == "Woods" && inventory.slots[i].itemCount >= 2)
                        {
                            k = true;
                            inventory.slots[i].SetSlotCount(-2);
                        }
                        if (inventory.slots[j].item != null)
                        {
                            if (inventory.slots[j].item.itemName == "Rope" && inventory.slots[j].itemCount >= 1)
                            {
                                l = true;
                                inventory.slots[j].SetSlotCount(-1);
                            }
                        }

                    }
                }

            }

            if (k && l)
            {
                var itemGo = Instantiate<GameObject>(this.paddlePrefab);
                itemGo.transform.position = this.targetTransform.transform.position + Vector3.forward * 2f;
                itemGo.SetActive(true);

                k = false;
                l = false;
            }

        }

        public void Rope()
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.slots[i].item != null)
                {
                    if (inventory.slots[i].item.itemName == "Cloth")
                    {
                        if (inventory.slots[i].itemCount >= 2)
                        {
                            var itemGo = Instantiate<GameObject>(this.ropePrefab);
                            itemGo.transform.position = this.targetTransform.transform.position + Vector3.forward * 2f;
                            itemGo.SetActive(true);

                            inventory.slots[i].SetSlotCount(-2);
                        }
                        else
                        {
                            Debug.Log("천은 있는데 수량이 부족해!");
                        }
                    }
                }
                else
                {
                    Debug.Log("천 아이템이 없습니다.");
                }
            }

        }

    }
