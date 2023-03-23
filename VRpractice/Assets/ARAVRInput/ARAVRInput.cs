#define PC
//#define Oculus
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

    //컨트롤러의 특정 버튼을 누르고 있는 동안 true를 반환
    public static bool Get(Button virtualMask, Controller hand = Controller.RTouch)
    {
#if PC
        //virtualMask에 들어온 값을 ButtonTarget 타입으로 변환해 전달한다.
        return Input.GetButton(((ButtonTarget)virtualMask).ToString());
#endif
    }

    //컨트롤러의 특정 버튼을 눌렀을 때 true를 반환
    public static bool GetDown(Button virtualMask, Controller hand = Controller.RTouch)
    {
#if PC
        return Input.GetButtonDown(((ButtonTarget)virtualMask).ToString());
#endif
    }

    //컨트롤러의 특정 버튼을 눌렀다 떼었을 때 true를 반환
    public static bool GetUp(Button virtualMask, Controller hand = Controller.RTouch)
    {
#if PC
        return Input.GetButtonUp(((ButtonTarget)virtualMask).ToString());
#endif
    }

    //컨트롤러의 Axis 입력을 반환
    //axis: Horizontal, Vertical 값을 갖는다.
    public static float GetAxis(string axis, Controller hand = Controller.LTouch)
    {
#if PC
        return Input.GetAxis(axis);
#endif
    }

    //컨트롤러에 진동 호출하기
    public static void PlayVibration(Controller hand)
    {
    }

    //카메라가 바라보는 방향을 기준으로 센터를 잡는다.
    public static void Recenter()
    {
    }

    public static void Recenter(Transform target, Vector3 direction)
    {
        target.forward = target.rotation * direction;
    }

#if PC
    static Vector3 originScale = Vector3.one * 0.02f;
#endif

    public static void DrawCrosshair(Transform crosshair, bool isHand = true, Controller hand = Controller.RTouch)
    {
        Ray ray;

        //컨트롤러의 위치와 방향을 이용해 레이 제작
        if(isHand)
        {
#if PC
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#endif
        }
        else
        {
            //카메라를 기준으로 화면의 정중앙으로 레일르 제작
            ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        }
        //눈에 안 보이는 Plane을 만든다.
        Plane plane = new Plane(Vector3.up, 0);
        float distance = 0;

        //plane을 이용해 ray를 쏜다.
        if(plane.Raycast(ray, out distance))
        {
            //레이의 GetPoint 함수를 이용해 충돌 지점의 위치를 가져온다.
            crosshair.position = ray.GetPoint(distance);
            crosshair.forward = -Camera.main.transform.forward;
            //크로스헤어의 크기를 최소 기본 크기에서 거리에 따라 더 커지도록 한다.
            crosshair.localScale = originScale * Mathf.Max(1, distance);
        }
        else
        {
            crosshair.position = ray.origin + ray.direction * 100;
            crosshair.forward = -Camera.main.transform.forward;
            distance = (crosshair.position - ray.origin).magnitude;
            crosshair.localScale = originScale * Mathf.Max(1, distance);
        }
    }
}