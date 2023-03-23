#define PC
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class ARAVRInput
{
#if PC
    public enum ButtonTarget
    {
        Fire1,
        Fire2,
        Fire3,
        Jump,
    }
#endif
    public enum Button
    {
#if PC
        One = ButtonTarget.Fire1,
        Two = ButtonTarget.Jump,
        Thumbstick = ButtonTarget.Fire1,
        IndexTrigger = ButtonTarget.Fire3,
        HandTrigger = ButtonTarget.Fire2
#endif
    }

    public enum Controller
    {
#if PC
        LTouch,
        RTouch
#endif
    }

    // 왼쪽 컨트롤러
    static Transform lHand;

    // 씬에 등록된 왼쪽 컨트롤러를 찾아 반환
    public static Transform LHand
    {
        get
        {
            if(lHand == null)
            {
#if PC
                //LHand라는 이름으로 게임 오브젝트를 만든다.
                GameObject handObj = new GameObject("LHand");
                //만들어진 객체의 트랜스폼을 lHand에 할당
                lHand = handObj.transform;
                //컨트롤러를 카메라의 자식 객체로 등록
                lHand.parent = Camera.main.transform;
#endif
            }
            return lHand;
        }
    }

    //오른쪽 컨트롤러
    static Transform rHand;

    //씬에 등록된 오른쪽 컨트롤러를 찾아 반환
    public static Transform RHand
    {
        get
        {
            //만약 rHand에 값이 없을 경우
            if(rHand == null)
            {
#if PC
                //RHand 이름으로 게임 오브젝트를 만든다.
                GameObject handObj = new GameObject("RHand");
                //만들어진 객체의 트랜스폼을 rHand에 할당
                rHand = handObj.transform;
                //컨트롤러를 카메라의 자식 객체로 등록
                rHand.parent = Camera.main.transform;
#endif
            }
            return rHand;
        }
    }

    //오른쪽 컨트롤러의 위치 얻어오기
    public static Vector3 RHandPosition
    {
        get
        {
#if PC
            //마우스의 스크린 좌표 얻어오기
            Vector3 pos = Input.mousePosition;
            //z값은 0.7m로 설정
            pos.z = 0.7f;
            //스크린 좌표를 월드 좌표로 변환
            pos = Camera.main.ScreenToWorldPoint(pos);
            RHand.position = pos;
            return pos;
#endif
        }
    }

    //오른쪽 컨트롤러의 방향 얻어오기
    public static Vector3 RHandDirection
    {
        get
        {
#if PC
            Vector3 direction = RHandPosition - Camera.main.transform.position;
            RHand.forward = direction;
            return direction;
#endif
        }
    }
}