using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IPointerClickHandler
{

    public Item item; // 획득한 아이템
    public int itemCount; // 획득한 아이템 갯수
    public Image itemImage; // 아이템의 이미지

    // 필요한 컴포넌트
    [SerializeField]
    private TextMeshProUGUI text_Count; // 아이템 갯수
    [SerializeField]
    private GameObject go_CountImage; // 획득한 아이템 갯수창 이미지

    private ItemEffectDatabase theItemEffectDatabase;       // ItemEffectDatabase 받아오기

    private void Start()
    {
        theItemEffectDatabase = FindObjectOfType<ItemEffectDatabase>();     // ItemEffectDatabase 할당
    }

    // 이미지의 투명도 조절 
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;      // color 선언
        color.a = _alpha;       // 이미지의 알파값 조절
        itemImage.color = color;    // itemImage의 알파값 바꿔줌
    }

    // 아이템 획득
    public void AddItem(Item _item, int _count = 1)      // 보통 아이템 1개씩 획득하니까 _count = 1, AddItem(_item, 3); -> 아이템 3개 획득
    {
        //
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;      // sprite에 itemImage넣어줌
        if (item.itemType != Item.ItemType.Equipment)        // 아이템 타입이 장비일때는 활성화 시키지 않을것
        {
            go_CountImage.SetActive(true);      // 아이템 들어왔으므로 go_CountImage활성화
            text_Count.text = itemCount.ToString();     // Integer 타입은 text와 호환이 안되므로 ToString으로 형변환
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }

        SetColor(1);        // 아이템이 들어왔으므로
    }

    // 아이템 갯수 조정
    public void SetSlotCount(int _count)
    {
        itemCount += _count;        // -3넣으면 3개가 깎이는것
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)      // 아이템이 없으므로
        {
            ClearSlot();        // 슬롯초기화
        }
    }

    // 슬롯 초기화
    private void ClearSlot()
    {
        item = null;        // 아이템 null
        itemCount = 0;      // itemCount = 0
        itemImage.sprite = null;    // itemImage null로 변환
        SetColor(0);        // 슬롯 투명

        text_Count.text = "0";
        go_CountImage.SetActive(false);

    }

    // 아이템 사용하는 구간
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)     // 좌클릭시
        {
            if (item != null)       // 아이템이 있는지 없는지 확인하고 실행
            {
                theItemEffectDatabase.UseItem(item);        //theItemEffectDatabase에 UseItem 실행
                if (item.itemType == Item.ItemType.Used)        // 소모품일 경우
                {
                    SetSlotCount(-1);       // 아이템 -1씩 소비
                }
            }
        }
    }
}
