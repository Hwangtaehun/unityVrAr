using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportStraight : MonoBehaviour
{
    // 텔레포트를 표시할 UI
    public Transform teleportCircleUI;
    // 선을 그릴 라인 렌더러
    LineRenderer lr;
    //최초 텔레포트 UI의 크기
    Vector3 originScale = Vector3.one * 0.02f;

    void Start()
    {
        //시작할 때 비활성화한다.
        teleportCircleUI.gameObject.SetActive(false);
        //라인 렌더러 컴포넌트 얻어오기
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if(ARAVRInput.GetUp(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            lr.enabled = true;
        }
        else if(ARAVRInput.GetUp(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            // 라인 렌더러 비활성화
            lr.enabled = false;
            // 텔레포트 UI 비활성화
            teleportCircleUI.gameObject.SetActive(false);
        }
        else if(ARAVRInput.Get(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            //텔레포트 UI 그리기
            //1. 왼쪽 컨트롤러를 기준으로 Ray를 만든다.
            Ray ray = new Ray(ARAVRInput.LHandPosition, ARAVRInput.LHandDirection);
            RaycastHit hitInfo;
            int layer = 1 << LayerMask.NameToLayer("Terrain");
            //2. Terrain만 Ray 충돌 검출
            if(Physics.Raycast(ray, out hitInfo, 200, layer))
            {
                //부딪힌 지점에 텔레포트 UI표시
                //3. Ray가 부딪힌 지점에 라인 그리기
                lr.SetPosition(0, ray.origin);
                lr.SetPosition(1, hitInfo.point);
                //4. Ray가 부딪힌 지점에 텔레포트 UI 표시
                teleportCircleUI.gameObject.SetActive(true);
                teleportCircleUI.position = hitInfo.point;
                //텔레포트 UI가 위로 누워 있도록 방향 설정
                teleportCircleUI.forward = hitInfo.normal;
                //텔레포트 UI의 크기가 거리에 따라 보정되도록 설정
                teleportCircleUI.localScale = originScale * Mathf.Max(1, hitInfo.distance);
            }
            else
            {
                //Ray 충돌이 발생하지 않으면 선이 Ray 방향으로 그려지도록 처리
                lr.SetPosition(0, ray.origin);
                lr.SetPosition(1, ray.origin + ARAVRInput.LHandDirection * 200);
                //텔레포트 UI는 화면에서 비활성화
                teleportCircleUI.gameObject.SetActive(false);
            }
        }
    }
}
