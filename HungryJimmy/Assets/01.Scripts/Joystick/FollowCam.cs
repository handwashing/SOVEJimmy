using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 5f, -6f);      // 카메라의 위치
    private float smoothTime = 0.15f;       // 이동을 따라갈 속도
    // SmoothDamp -> 카메라 이동을 부드럽게 해주기 위해 사용됨   
    private Vector3 velocity = Vector3.zero;        

    [SerializeField] private Transform target;      // 목표위치 여기서는 플레이어

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + offset;      // 목표위치
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);     // 현재위치, 목표위치, 현재속도, 이동속도  
    }
}
