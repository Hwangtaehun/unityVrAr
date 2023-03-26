using UnityEngine;
using UnityEngine.Video;

public class VideoFrame : MonoBehaviour
{
    VideoPlayer vp;

    void Start()
    {
        /*���� ������Ʈ�� ���� �÷��̾� ������Ʈ ������ ������ �´�.*/
        vp = GetComponent<VideoPlayer>();
        /*�ڵ� ����Ǵ� ���� ���´�.*/
        vp.Stop();
    }

    void Update()
    {
        /*S�� ������ �����϶�.*/
        if(Input.GetKeyDown(KeyCode.S))
        {
            vp.Stop();
        }
        if(Input.GetKeyDown("space"))
        {
            /*���� ���� �÷��̾ �÷��� �������� Ȯ���϶�.*/
            if(vp.isPlaying)
            {
                /*�÷���(���)���̶�� �Ͻ� �����϶�.*/
                vp.Pause();
            }
            else
            {
                /*�׷��� �ʴٸ�(�Ͻ� ���� �� �Ǵ� ����)�÷���(���)�϶�*/
                vp.Play();
            }
        }
    }

    /*GazePointerCtrl���� ���� ����� ��Ʈ���ϱ� ���� �Լ�*/
    public void CheckVideoFrame(bool Checker)
    {
        if (Checker)
        {
            if (!vp.isPlaying)
            {
                vp.Play();
            }
            else
            {
                vp.Stop();
            }
        }
    }
}
