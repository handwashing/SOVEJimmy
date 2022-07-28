using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitTree : MonoBehaviour
{
    [SerializeField]
    private int hp; //나무의 체력

    [SerializeField]
    public GameObject bananaPrefab; //Banana Prefab
    [SerializeField]
    public GameObject bananaPrefabSecond; //Banana Prefab

    [SerializeField]
    private CapsuleCollider capCol; //capsule 콜라이더 -> 파괴후에 비활성화시켜서 없애버릴거임...

    [SerializeField]
    private GameObject go_tree; //일반 나무


    [SerializeField]
    private GameObject go_hit_effect_prefab;    //파편 이펙트

    [SerializeField]
    private float debrisDestroyTime;    //파편 제거 시간

    [SerializeField]
    private float destroyTime;    //나무 제거 시간

    //필요한 사운드 이름
    [SerializeField]
    private string chop_sound; //나무 썰리는 소리
    [SerializeField]
    private string falldown_sound; //나무 떨어지는 소리
    [SerializeField]
    private string logChange_sound; //통나무로 바뀌는 소리

    public bool isClear = false;        // 오브젝트 사라졌는지 확인할 조건

    public void HitFruit()
    {
        SoundManager.instance.PlaySE(chop_sound); //효과음 재생

        var clone = Instantiate(go_hit_effect_prefab, capCol.bounds.center, Quaternion.identity);
        Destroy(clone, debrisDestroyTime); //일정 시간(destroyTime) 후 파편 클론 파괴

        hp--; //hp를 1씩 깎아서...
        if (hp <= 0) //hp가 0이하면 파괴
            FallDownTree(); //나무 쓰러뜨리기 - 체력주기~!
    }

    private void FallDownTree()
    {
        isClear = true;
        SoundManager.instance.PlaySE(falldown_sound);

        //나무가 파괴 되었기에 비활성화하고 사라지게 하기 -> 잔해만 남도록
        capCol.enabled = false;
        Destroy(go_tree);

        TreeDropItem();
    }

    public void TreeDropItem()
    {
        var itemGo = Instantiate<GameObject>(this.bananaPrefabSecond);
        itemGo.transform.position = this.gameObject.transform.position + Vector3.up * 0.5f;
        itemGo.SetActive(true);

        var itemGoSecond = Instantiate<GameObject>(this.bananaPrefab);
        itemGoSecond.transform.position = this.gameObject.transform.position + Vector3.up * 0.6f;
        itemGoSecond.SetActive(true);
    }
}
