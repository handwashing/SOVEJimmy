using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public string itemName;    // 아이템의 이름 (키값으로 사용) 
    [Tooltip("Stamina, HUNGRY, THIRSTY 만 가능합니다")]     // 툴팁
    public string[] part;     // 효과가 적용될 부분
    public int[] num;     // 효과가 적용될 수치

}

public class ItemEffectDatabase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] itemEffects;       // 아이템효과 배열

    // 필요한 컴포넌트
    [SerializeField]
    private StatusController thePlayerStatus;       // StatusController 받아오기

    // 변수의 상수화
    private const string STAMINA = "Stamina", HUNGRY = "HUNGRY", THIRSTY = "THIRSTY";



    public void UseItem(Item _item)     // 넘어오는 파라미터로 아이템 넣어줌
    {
        if (_item.itemType == Item.ItemType.Used)       // ItemType이 Used일때
        {
            for (int x = 0; x < itemEffects.Length; x++)        // 배열 itemEffects의 길이만큼
            {
                if (itemEffects[x].itemName == _item.itemName)      // itemEffects의 [x]번째의 itemName과 넘어온 파라미터값의 itemName비교 
                {
                    for (int y = 0; y < itemEffects[x].part.Length; y++)        //itemEffects[x].part의 길이만큼 반복돌림
                    {
                        switch (itemEffects[x].part[y])     // itemEffects[x]의 part의 [y]번째
                        {
                            case STAMINA:
                                thePlayerStatus.IncreaseStamina(itemEffects[x].num[y]);     //IncreaseStamina의 itemEffects[x]번째의 num[y]만큼 회복
                                break;
                            case HUNGRY:
                                thePlayerStatus.IncreaseHungry(itemEffects[x].num[y]);      //IncreaseHungry의 itemEffects[x]번째의 num[y]만큼 회복
                                break;
                            case THIRSTY:
                                thePlayerStatus.IncreaseThirsty(itemEffects[x].num[y]);     //IncreaseThirsty의 itemEffects[x]번째의 num[y]만큼 회복
                                break;
                            default:
                                Debug.Log("잘못된 Status 부위 HUNGRY, THIRSTY, STAMINA만 가능합니다");      // 모든 조건에 걸리지 않으면 띄워줌
                                break;
                        }
                        Debug.Log(_item.itemName + "을 사용했습니다.");
                    }
                    return;
                }
            }
            Debug.Log("ItemEffectDatabase에 일치하는 itemNmae 없습니다");
        }
    }

}
