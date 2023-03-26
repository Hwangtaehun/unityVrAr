using UnityEngine;
using UnityEngine.Video;

public class VideoFrame : MonoBehaviour
{
    VideoPlayer vp;

    void Start()
    {
        /*현재 오브젝트의 비디오 플레이어 컴포넌트 정보를 가지고 온다.*/
        vp = GetComponent<VideoPlayer>();
        /*자동 재생되는 것을 막는다.*/
        vp.Stop();
    }

    void Update()
    {
        /*S를 누르면 정지하라.*/
        if(Input.GetKeyDown(KeyCode.S))
        {
            vp.Stop();
        }
        if(Input.GetKeyDown("space"))
        {
            /*현재 비디오 플레이어가 플레이 상태인지 확인하라.*/
            if(vp.isPlaying)
            {
                /*플레이(재생)중이라면 일시 정지하라.*/
                vp.Pause();
            }
            else
            {
                /*그렇지 않다면(일시 정지 중 또는 멈춤)플레이(재생)하라*/
                vp.Play();
            }
        }
    }

    /*GazePointerCtrl에서 영상 재생을 컨트롤하기 위한 함수*/
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
