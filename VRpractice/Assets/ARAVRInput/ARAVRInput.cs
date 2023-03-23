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
}