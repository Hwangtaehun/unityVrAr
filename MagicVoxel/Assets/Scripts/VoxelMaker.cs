using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����ڰ� ���콺�� Ŭ���� ������ ������ 1�� ����� �ʹ�. -> �ʿ� �Ӽ�: ���� ����
public class VoxelMaker : MonoBehaviour
{
    //���� ����
    public GameObject voxelFactory;

    void Update()
    {
        //����ڰ� ���콺�� Ŭ���� ������ ������ 1�� ����� �ʹ�.
        //1. ����ڰ� ���콺�� Ŭ���ߴٸ�
        if(Input.GetButtonDown("Fire1"))
        {
            //2. ���콺�� ��ġ�� �ٴ� ���� ��ġ�� �ִٸ�
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo = new RaycastHit();
            if(Physics.Raycast(ray, out hitInfo))
            {
                //3. ���� ���忡�� ������ ������ �Ѵ�.
                GameObject voxel = Instantiate(voxelFactory);
                //4. ������ ��ġ�ϰ� �ʹ�.
                voxel.transform.position = hitInfo.point;
            }
        }
    }
}
