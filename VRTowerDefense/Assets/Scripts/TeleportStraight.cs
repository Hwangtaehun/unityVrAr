using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportStraight : MonoBehaviour
{
    // �ڷ���Ʈ�� ǥ���� UI
    public Transform teleportCircleUI;
    // ���� �׸� ���� ������
    LineRenderer lr;
    //���� �ڷ���Ʈ UI�� ũ��
    Vector3 originScale = Vector3.one * 0.02f;

    void Start()
    {
        //������ �� ��Ȱ��ȭ�Ѵ�.
        teleportCircleUI.gameObject.SetActive(false);
        //���� ������ ������Ʈ ������
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
            // ���� ������ ��Ȱ��ȭ
            lr.enabled = false;
            // �ڷ���Ʈ UI ��Ȱ��ȭ
            teleportCircleUI.gameObject.SetActive(false);
        }
        else if(ARAVRInput.Get(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            //�ڷ���Ʈ UI �׸���
            //1. ���� ��Ʈ�ѷ��� �������� Ray�� �����.
            Ray ray = new Ray(ARAVRInput.LHandPosition, ARAVRInput.LHandDirection);
            RaycastHit hitInfo;
            int layer = 1 << LayerMask.NameToLayer("Terrain");
            //2. Terrain�� Ray �浹 ����
            if(Physics.Raycast(ray, out hitInfo, 200, layer))
            {
                //�ε��� ������ �ڷ���Ʈ UIǥ��
                //3. Ray�� �ε��� ������ ���� �׸���
                lr.SetPosition(0, ray.origin);
                lr.SetPosition(1, hitInfo.point);
                //4. Ray�� �ε��� ������ �ڷ���Ʈ UI ǥ��
                teleportCircleUI.gameObject.SetActive(true);
                teleportCircleUI.position = hitInfo.point;
                //�ڷ���Ʈ UI�� ���� ���� �ֵ��� ���� ����
                teleportCircleUI.forward = hitInfo.normal;
                //�ڷ���Ʈ UI�� ũ�Ⱑ �Ÿ��� ���� �����ǵ��� ����
                teleportCircleUI.localScale = originScale * Mathf.Max(1, hitInfo.distance);
            }
            else
            {
                //Ray �浹�� �߻����� ������ ���� Ray �������� �׷������� ó��
                lr.SetPosition(0, ray.origin);
                lr.SetPosition(1, ray.origin + ARAVRInput.LHandDirection * 200);
                //�ڷ���Ʈ UI�� ȭ�鿡�� ��Ȱ��ȭ
                teleportCircleUI.gameObject.SetActive(false);
            }
        }
    }
}
