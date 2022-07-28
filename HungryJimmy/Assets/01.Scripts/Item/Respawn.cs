using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private float logTime;     // 나무 리스폰 시간
    public GameObject newLogPrefab;     // 생성될 나무 프리팹
    [SerializeField] Transform logTransform;        // 나무 생성될 위치

    [SerializeField] private float fruitTime;       // 바나나 나무 리스폰 시간
    public GameObject newFruitPrefab;       // 생성될 바나나 나무 프리팹
    [SerializeField] Transform fruitTransform;      // 바나나 나무 생성될 위치


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SearchLog();
        SearchFruit();
    }

    IEnumerator RespawnLogTree()        // 나무 리스폰 코루틴
    {
        yield return new WaitForSeconds(logTime);       // logTime만큼 지연시간 줌
        var itemGo = Instantiate<GameObject>(this.newLogPrefab);        // newLogPrefab 생성
        itemGo.transform.position = this.logTransform.transform.position;       // 지정된 위치에
        itemGo.SetActive(true);     // 생성된 아이템 활성화
    }

    IEnumerator RespawnFruitTree()      //  바나나 나무 리스폰 코루틴
    {
        yield return new WaitForSeconds(fruitTime);     // fruitTime만큼 지연시간 줌
        var itemGo = Instantiate<GameObject>(this.newFruitPrefab);      // newFruitPrefab 생성
        itemGo.transform.position = this.fruitTransform.transform.position;     // 지정된 위치
        itemGo.SetActive(true);      // 생성된 아이템 활성화
    }

    private void SearchLog()        // 나무 찾는 함수
    {
        if (GameObject.FindGameObjectWithTag("Tree") != null)       // TAG가 Tree인 게임오브젝트가 null이 아니고
        {
            if (GameObject.FindGameObjectWithTag("Tree").GetComponent<Tree>().isClear == true)      // 그 오브젝트에서 Tree 컴포넌트 읽어오고 isClear가 true일때
            {
                StartCoroutine(RespawnLogTree());       // RespawnLogTree 코루틴 실행
            }
        }

    }
    
    private void SearchFruit()      // 바나나 나무 찾는 함수
    {
        if (GameObject.FindGameObjectWithTag("FruitTree") != null)      // TAG가 FruitTree인 게임오브젝트가 null이 아니고
        {
            if (GameObject.FindGameObjectWithTag("FruitTree").GetComponent<FruitTree>().isClear == true)    // 그 오브젝트에서 FruitTree 컴포넌트 읽어오고 isClear가 true일때
            {
                StartCoroutine(RespawnFruitTree());     // RespawnFruitTree 코루틴 실행
            }
        }

    }

}
