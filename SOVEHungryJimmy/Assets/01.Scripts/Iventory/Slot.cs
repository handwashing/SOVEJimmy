using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot: MonoBehaviour, IPointerClickHandler
{
    public Item item; // 획득한 아이템
    public int itemCount; // 획득한 아이템 갯수
    public Image itemImage; // 아이템의 이미지

    [SerializeField] private bool isQuickSlot;      // 퀵슬롯 여부 판단
    [SerializeField] private int quickSlotNumber;       // 퀵슬롯 번호

    // 필요한 컴포넌트
    [SerializeField]
    private Text text_Count; //

    [SerializeField]
    private GameObject go_CountImage; //

    private WeaponManager theWeaponManager;
    [SerializeField]
    private RectTransform baseRect;       // 인벤토리 영역
    [SerializeField]
    private RectTransform quickSlotBaseRect;        // 퀵슬롯의 영역
    private InputNumber theInputNumber;
    private ItemEffectDatabase theItemEffectDatabase;

    private void Start() 
    {
        theInputNumber = FindObjectOfType<InputNumber>();
        theItemEffectDatabase = FindObjectOfType<ItemEffectDatabase>();    
    }

    // 이미지의 투명도 조절 
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // 아이템 획득
    public void AddItem(Item _item, int _count =1)
    {
        //
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;
        if(item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }
    
        SetColor(1);
    }

    public int GetQuickSlotNumber()
    {
        return quickSlotNumber;
    }

    // 아이템 갯수 조정
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if(itemCount <= 0)
        {
            ClearSlot();
        }
    }

    // 슬롯 초기화
    private void ClearSlot()
    {
        item = null;
        itemCount =0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);

    }

    // 아이템 사용하는 구간
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                theItemEffectDatabase.UseItem(item);
                if (item.itemType == Item.ItemType.Used)        // ItemType이 Used일때만
                {
                    SetSlotCount(-1);       // 아이템 -1씩 해줌
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null && Inventory.inventoryActivated)       // item이 null이 아니고 Inventory.inventoryActivated가 true일때
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);

            DragSlot.instance.transform.position = eventData.position;        // DragSlot 위치값으로 조정
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)       // item이 null이 아닐 때
        {
            transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그 했는데 인벤토리 창이나 퀵슬롯 영역을 벗어난 경우
        if (!((DragSlot.instance.transform.localPosition.x > baseRect.rect.xMin && DragSlot.instance.transform.localPosition.x < baseRect.rect.xMax
        && DragSlot.instance.transform.localPosition.y > baseRect.rect.yMin && DragSlot.instance.transform.localPosition.y < baseRect.rect.yMax)
        || (DragSlot.instance.transform.localPosition.x > quickSlotBaseRect.rect.xMin && DragSlot.instance.transform.localPosition.x < quickSlotBaseRect.rect.xMax
        && DragSlot.instance.transform.localPosition.y > quickSlotBaseRect.transform.localPosition.y - quickSlotBaseRect.rect.yMax && DragSlot.instance.transform.localPosition.y < quickSlotBaseRect.transform.localPosition.y - quickSlotBaseRect.rect.yMin)))
        {
            if (DragSlot.instance.dragSlot != null)     // Slot이 null이 아닐때만 실행
            {
                theInputNumber.Call();
            }
        }
        else
        {
            DragSlot.instance.SetColor(0);      // 이미지 투명한 상태로 만들어줌
            DragSlot.instance.dragSlot = null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
            if (isQuickSlot)        // 인벤토리에서 퀵슬롯으로 혹은 퀵슬롯에서 퀵슬롯으로
            {
                theItemEffectDatabase.isActivatedQuickSlot(quickSlotNumber);       // 활성화된 퀵슬롯이면 교체작업
            }
            else        // 인벤토리 -> 인벤토리, 퀵슬롯 -> 인벤토리 경우
            {
                if (DragSlot.instance.dragSlot.isQuickSlot)     // 퀵슬롯 -> 인벤토리
                {
                    theItemEffectDatabase.isActivatedQuickSlot(DragSlot.instance.dragSlot.quickSlotNumber);        // 활성화된 퀵슬롯이면 교체작업
                }
            }
        }
    }

    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;
        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if (_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }

    // 마우스가 슬롯에 들어갈 때 발동
    public void OnPointerExit(PointerEventData eventData)
    {
        if (item != null)       // item이 null이 아닐때 실행하도록
        {
            theItemEffectDatabase.ShowToolTip(item, transform.position);    // theItemEffectDatabase에서 ShowToolTip호출
        }
    }

    // 마우스가 슬롯에서 빠져나갈 때 발동
    public void OnPointerEnter(PointerEventData eventData)
    {
        theItemEffectDatabase.HideToolTip();        // theItemEffectDatabase에서 HideToolTip호출
    }
}
