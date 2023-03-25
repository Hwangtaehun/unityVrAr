using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1.������ ������ �������� ���ư��� ��� �Ѵ�. -> �ʿ� �Ӽ�: ���ư� �ӵ�
public class Voxel : MonoBehaviour
{
    //1.������ ���ư� �ӵ� �Ӽ�
    public float speed = 5;
    //������ ������ �ð�
    public float destoryTime = 3.0f;
    //��� �ð�
    float currentTime;

    void Start()
    {
        //2.������ ������ ã�´�.
        Vector3 direction = Random.insideUnitSphere;
        //3.������ �������� ���ư��� �ӵ��� �ش�.
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = direction * speed;
    }

    void Update()
    {
        //���� �ð��� ������ ������ �����ϰ� �ʹ�.
        //1. �ð��� �귯�� �Ѵ�.
        currentTime += Time.deltaTime;
        //2. ���� �ð��� �����ϱ�. ���� ��� �ð��� ���� �ð��� �ʰ��ߴٸ�
        if(currentTime > destoryTime)
        {
            //3. ������ �����ϰ� �ʹ�.
            Destroy(gameObject);
        }
    }
}
