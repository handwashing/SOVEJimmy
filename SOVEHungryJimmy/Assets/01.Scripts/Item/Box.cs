using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject box;      // 처음 정보를 받아오기전에 보여줄 빈박스
    public GameObject logBox;       // 분류결과가 나무로 나오면 보여줄 나무박스
    public GameObject clothBox;     // 분류결과가 옷으로 나오면 보여줄 옷박스

    public GetInferenceFromModel getInferenceFromModel;     // AI모델과 연결

    void OnMouseDown()      // 마우스 클릭하면
    {
        Find();     // Find()실행
    }

    IEnumerator LogBoxChange()      // Log 아이템을 가진 박스 코루틴
    {
        box.SetActive(false);       // 박스 오브젝트 비활성화
        logBox.SetActive(true);     // Log 아이템 박스 활성화
        yield return new WaitForSeconds(3f * Time.deltaTime);       // 지연시간
        logBox.SetActive(false);
    }
    IEnumerator ClothBoxChange()        // Cloth 아이템을 가진 박스 코루틴
    {
        box.SetActive(false);       // 박스 오브젝트 비활성화
        clothBox.SetActive(true);       // 천 아이템 박스 활성화
        yield return new WaitForSeconds(3f * Time.deltaTime);       // 지연시간
        clothBox.SetActive(false);
    }

    public void Find()
    {
      
        if (getInferenceFromModel.prediction.predictedValue == 1)        // 모델의 분류값이 1이면
        {
            Debug.Log("Logbox");
            box.SetActive(true);        // 빈박스 활성화
            StartCoroutine(LogBoxChange());     // Log박스 바꿔주는 코루틴 실행
        }
        if (getInferenceFromModel.prediction.predictedValue == 0)        // 모델의 분류값이 0이면
        {
            Debug.Log("Clothbox");
            box.SetActive(true);        // 빈박스 활성화
            StartCoroutine(ClothBoxChange());       // Cloth박스 바꿔주는 코루틴 실행
        }
    }

}
