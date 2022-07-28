using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionController : MonoBehaviour
{

    [SerializeField]
    private float range;    // 습득가능한 최대 거리 == ray 사정거리

    // 아이템 습득 가능하다면 true, default == false
    private bool pickupActivated = false;

    // 충돌체 정보 저장
    private RaycastHit hitInfo;     // 충돌체 정보 저장


    // 특정 레이어를 가진 오브젝트에 대해서만 습득할 수 있게 함
    [SerializeField]
    private LayerMask layerMask;

    // 필요한 컴포넌트
    [SerializeField]
    private TextMeshProUGUI actionText;     // 행동을 보여 줄 텍스트

    [SerializeField]
    private Inventory theInventory;

    public GameObject endImage;     // 보트 아이템을 얻고나면 보여줄 이미지


    private void Start()
    {

    }

    private void Update()
    {
        // 매프레임마다 아이템이 있는지 확인
        CheckAction();
        TryAction();
    }

    // 아이템 먹을 때 확인할 메서드
    private void TryAction()
    {
        // 클릭했을때 아이템이 있는지 없는지 확인하는 메서드
        if (Input.GetMouseButtonDown(0))
        {
            CheckAction();
            CanPickUp();
        }
    }

    // pickupActivated가 true라면 아이템을 주어라
    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (!Inventory.inventoryActivated)
            {
                if (hitInfo.transform != null)      // transform이 null이 아닌경우에
                {
                    // 어떤 아이템을 획득했는지 확인
                    Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "획득했습니다.");
                    // 부딪힌 충돌체 안에 있는 ItemPickUp안의 item을 넣기
                    theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                    // 획득한 아이템 파괴
                    Destroy(hitInfo.transform.gameObject);
                    InfoDisappear();        // 아이템 정보 보여주기
                    if (hitInfo.transform.GetComponent<ItemPickUp>().item.itemName == "Boat")       // itemName이 Boat인 충돌체 일때
                    {
                        endImage.SetActive(true);       // 엔딩 장소로 안내하는 이미지 보여주기
                    }
                }
            }

        }
    }



    private void CheckAction()
    {
        // 레이를 쏴서 플레이어가 바라보는 방향으로 충돌체의 정보를 확인하고 레이의 사정거리를 지정
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
                Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * hitInfo.distance, Color.red);
            }
        }
        else // 아이템 획득하게 되면 정보 비활성화
        {
            InfoDisappear();
        }
    }


    // 아이템 정보가 보이는 메서드
    private void ItemInfoAppear()
    {
        pickupActivated = true;     // pickupActivated 활성화
        actionText.gameObject.SetActive(true);      // actionText 활성화
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "획득" + "</color>";     // 어떤 아이템과 부딪혔는지 정보 알려줌
    }


    // 아이템 정보를 사라지게 하는 메서드
    private void InfoDisappear()
    {
        pickupActivated = false;        // pickupActivated 비활성화
        actionText.gameObject.SetActive(false);     // actionText 비활성화
    }
}