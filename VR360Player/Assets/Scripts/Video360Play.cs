using UnityEngine;
using UnityEngine.Video;//VideoPlayer ����� ����ϱ� ���� ���ӽ����̽�

/*���� �÷��̾ ���� 360 ���Ǿ ������ �������.
 �� ���� ���� �ٸ� ������ ��ü�ϸ� ����Ѵ�.*/
public class Video360Play : MonoBehaviour
{
    /*���� �÷��̾� ������Ʈ*/
    VideoPlayer vp;
    /*����ؾ� �� VR 360 ������ ���� ����*/
    public VideoClip[] vcList; //�ټ��� ���� Ŭ���� �迵�� ���巷 �����Ѵ�.
    int curVCidx; //���� ��� ���� Ŭ�� ��ȣ�� �����Ѵ�.

    void Start()
    {
        /*���� �÷��̾� ������Ʈ�� ������ �޾ƿ´�.*/
        vp = GetComponent<VideoPlayer>();
        vp.clip = vcList[0];
        curVCidx = 0;
        vp.Stop();
    }

    void Update()
    {
        /*��ǻ�Ϳ��� ������ �����ϱ� ���� ���*/
        if(Input.GetKeyDown(KeyCode.LeftBracket)) //���� ���ȣ �Է� �� ���� ����
        {
            //vp.clip = vcList[0];
            SwapVideoClip(false);
        }
        else if(Input.GetKeyDown(KeyCode.RightBracket))
        {
            //vp.clip = vcList[1];
            SwapVideoClip(true);
        }
    }

    /*���ͷ����� ���� �Լ��� �ۺ����� �����Ѵ�.
     �迭�� �ε��� ��ȣ�� �������� �������� ��ü, ����ϱ� ���� �Լ�
    ���� ���� isNext�� true�̸� �������� false�� ���� ���� ���*/
    public void SwapVideoClip(bool isNext)
    {
        /*���� ��� ���� ������ �ѹ��� �������� üũ�Ѵ�.
        ���� ���� ��ȣ�� ���� ���󺸴� �迭���� �ε��� ��ȣ�� 1�� �۴�.
        ���� ���� ��ȣ�� ���� ���󺸴� �迭���� �ε��� ��ȣ�� 1�� ũ��.*/
        int setVCnum = curVCidx; //���� ��� ���� ������ �ѹ��� �Է��Ѵ�.
        vp.Stop();               //���� ��� ���� ���� Ŭ���� �����Ѵ�.

        /*����� ������ ���� ���� ����*/
        if(isNext)
        {
            /*�迭�� ���� ������ ����Ѵ�.
             ����Ʈ ��ü ���̺��� ũ�� Ŭ���� ����Ʈ�� ù ��° ���� �����Ѵ�.*/
            setVCnum = (setVCnum + 1) % vcList.Length;
            
        }
        else
        {
            /*�迭�� ���� ������ ����Ѵ�.*/
            setVCnum = ((setVCnum - 1) + vcList.Length) % vcList.Length;
        }

        vp.clip = vcList[setVCnum]; /*Ŭ���� �����Ѵ�.*/
        vp.Play();                  /*�ٲ� ũ���� ����Ѵ�.*/
        curVCidx = setVCnum;        
    }

    public void SetVideoPlay(int num)
    {
        //���� ��� ���� ��ȣ�� ���޹��� ��ȣ�� �ٸ� ���� ����
        if(curVCidx != null)
        {
            vp.Stop();              //������ �����.
            vp.clip = vcList[num];  //Ŭ���� �����Ѵ�.
            curVCidx = num;         //���� ��� ���� ��ȣ �����Ѵ�.
            vp.Play();              //����Ѵ�.
        }
    }
}
