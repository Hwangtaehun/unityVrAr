using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//사용자가 마우스를 클릭한 지점에 복셀을 1개 만들고 싶다. -> 필요 속성: 복셀 공장
public class VoxelMaker : MonoBehaviour
{
    //복셀 공장
    public GameObject voxelFactory;

    void Update()
    {
        //사용자가 마우스를 클릭한 지점에 복셀을 1개 만들고 싶다.
        //1. 사용자가 마우스를 클릭했다면
        if(Input.GetButtonDown("Fire1"))
        {
            //2. 마우스의 위치가 바닥 위에 위치해 있다면
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo = new RaycastHit();
            if(Physics.Raycast(ray, out hitInfo))
            {
                //3. 복셀 공장에서 복셀을 만들어야 한다.
                GameObject voxel = Instantiate(voxelFactory);
                //4. 복셀을 배치하고 싶다.
                voxel.transform.position = hitInfo.point;
            }
        }
    }
}
