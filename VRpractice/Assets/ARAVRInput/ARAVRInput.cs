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

    // ���� ��Ʈ�ѷ�
    static Transform lHand;

    // ���� ��ϵ� ���� ��Ʈ�ѷ��� ã�� ��ȯ
    public static Transform LHand
    {
        get
        {
            if(lHand == null)
            {
#if PC
                //LHand��� �̸����� ���� ������Ʈ�� �����.
                GameObject handObj = new GameObject("LHand");
                //������� ��ü�� Ʈ�������� lHand�� �Ҵ�
                lHand = handObj.transform;
                //��Ʈ�ѷ��� ī�޶��� �ڽ� ��ü�� ���
                lHand.parent = Camera.main.transform;
#endif
            }
            return lHand;
        }
    }

    //������ ��Ʈ�ѷ�
    static Transform rHand;

    //���� ��ϵ� ������ ��Ʈ�ѷ��� ã�� ��ȯ
    public static Transform RHand
    {
        get
        {
            //���� rHand�� ���� ���� ���
            if(rHand == null)
            {
#if PC
                //RHand �̸����� ���� ������Ʈ�� �����.
                GameObject handObj = new GameObject("RHand");
                //������� ��ü�� Ʈ�������� rHand�� �Ҵ�
                rHand = handObj.transform;
                //��Ʈ�ѷ��� ī�޶��� �ڽ� ��ü�� ���
                rHand.parent = Camera.main.transform;
#endif
            }
            return rHand;
        }
    }

    //������ ��Ʈ�ѷ��� ��ġ ������
    public static Vector3 RHandPosition
    {
        get
        {
#if PC
            //���콺�� ��ũ�� ��ǥ ������
            Vector3 pos = Input.mousePosition;
            //z���� 0.7m�� ����
            pos.z = 0.7f;
            //��ũ�� ��ǥ�� ���� ��ǥ�� ��ȯ
            pos = Camera.main.ScreenToWorldPoint(pos);
            RHand.position = pos;
            return pos;
#endif
        }
    }

    //������ ��Ʈ�ѷ��� ���� ������
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

    //��Ʈ�ѷ��� Ư�� ��ư�� ������ �ִ� ���� true�� ��ȯ
    public static bool Get(Button virtualMask, Controller hand = Controller.RTouch)
    {
#if PC
        //virtualMask�� ���� ���� ButtonTarget Ÿ������ ��ȯ�� �����Ѵ�.
        return Input.GetButton(((ButtonTarget)virtualMask).ToString());
#endif
    }

    //��Ʈ�ѷ��� Ư�� ��ư�� ������ �� true�� ��ȯ
    public static bool GetDown(Button virtualMask, Controller hand = Controller.RTouch)
    {
#if PC
        return Input.GetButtonDown(((ButtonTarget)virtualMask).ToString());
#endif
    }

    //��Ʈ�ѷ��� Ư�� ��ư�� ������ ������ �� true�� ��ȯ
    public static bool GetUp(Button virtualMask, Controller hand = Controller.RTouch)
    {
#if PC
        return Input.GetButtonUp(((ButtonTarget)virtualMask).ToString());
#endif
    }

    //��Ʈ�ѷ��� Axis �Է��� ��ȯ
    //axis: Horizontal, Vertical ���� ���´�.
    public static float GetAxis(string axis, Controller hand = Controller.LTouch)
    {
#if PC
        return Input.GetAxis(axis);
#endif
    }

    //��Ʈ�ѷ��� ���� ȣ���ϱ�
    public static void PlayVibration(Controller hand)
    {
    }

    //ī�޶� �ٶ󺸴� ������ �������� ���͸� ��´�.
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

        //��Ʈ�ѷ��� ��ġ�� ������ �̿��� ���� ����
        if(isHand)
        {
#if PC
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#endif
        }
        else
        {
            //ī�޶� �������� ȭ���� ���߾����� ���ϸ� ����
            ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        }
        //���� �� ���̴� Plane�� �����.
        Plane plane = new Plane(Vector3.up, 0);
        float distance = 0;

        //plane�� �̿��� ray�� ���.
        if(plane.Raycast(ray, out distance))
        {
            //������ GetPoint �Լ��� �̿��� �浹 ������ ��ġ�� �����´�.
            crosshair.position = ray.GetPoint(distance);
            crosshair.forward = -Camera.main.transform.forward;
            //ũ�ν������ ũ�⸦ �ּ� �⺻ ũ�⿡�� �Ÿ��� ���� �� Ŀ������ �Ѵ�.
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