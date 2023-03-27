using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletImpact;
    public Transform crosshair;
    ParticleSystem bulletEffect;
    AudioSource bulletAudio;

    void Start()
    {
        //�Ѿ� ȿ�� ��ƼŬ �ý��� ������Ʈ ��������
        bulletEffect = bulletImpact.GetComponent<ParticleSystem>();
        //�Ѿ� ȿ�� ����� �ҽ� ������Ʈ ��������
        bulletAudio = bulletImpact.GetComponent<AudioSource>();
    }

    void Update()
    {
        //ũ�ν���� ǥ��
        ARAVRInput.DrawCrosshair(crosshair);

        //����ڰ� IndextTrigger��ư�� ������
        if(ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger))
        {
            //Ray�� ī�޶��� ��ġ�� ���� �������� �����.
            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
            //Ray�� �浹 ������ �����ϱ� ���� ���� ����
            RaycastHit hitInfo;
            //�÷��̾� ���̾� ������
            int playerLayer = 1 << LayerMask.NameToLayer("Player");
            //Ÿ�� ���̾� ������
            int towerLayer = 1 << LayerMask.NameToLayer("Tower");
            int layerMask = playerLayer | towerLayer;
            //�Ѿ� ����� ���
            bulletAudio.Stop();
            bulletAudio.Play();

            //Ray�� ���. ray�� �ε��� ������ hitInfo�� ����.
            if(Physics.Raycast(ray, out hitInfo, 200, ~layerMask))
            {
                //�Ѿ� ���� ȿ�� ó��
                //�Ѿ� ����Ʈ ����ǰ� ������ ���߰� ���
                bulletAudio.Stop();
                bulletAudio.Play();
                //�ε��� ���� �ٷ� ������ ����Ʈ�� ���̵��� ����
                bulletImpact.position = hitInfo.point;
                //�ε��� ���� �������� �Ѿ� ����Ʈ�� ������ ����
                bulletImpact.forward = hitInfo.normal;
            }
        }
    }
}
