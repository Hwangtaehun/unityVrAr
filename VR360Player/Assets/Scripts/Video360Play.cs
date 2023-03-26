using UnityEngine;
using UnityEngine.Video;//VideoPlayer 기능을 사용하기 위한 네임스페이스

/*비디오 플레이어를 통해 360 스피어에 영상을 재생하자.
 두 가지 서로 다른 영샹을 교체하며 재생한다.*/
public class Video360Play : MonoBehaviour
{
    /*비디오 플레이어 컴포넌트*/
    VideoPlayer vp;
    /*재생해야 할 VR 360 영상을 위한 설정*/
    public VideoClip[] vcList; //다수의 비디오 클립을 배영로 만드렁 관리한다.
    int curVCidx; //현재 재생 중인 클립 번호를 저장한다.

    void Start()
    {
        /*비디오 플레이어 컴포넌트의 정보를 받아온다.*/
        vp = GetComponent<VideoPlayer>();
        vp.clip = vcList[0];
        curVCidx = 0;
        vp.Stop();
    }

    void Update()
    {
        /*컴퓨터에서 영상을 변경하기 위한 기능*/
        if(Input.GetKeyDown(KeyCode.LeftBracket)) //왼쪽 대괄호 입력 시 이전 영상
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

    /*인터랙션을 위해 함수를 퍼블릭으로 선언한다.
     배열의 인덱스 번호를 기준으로 영상으로 교체, 재생하기 위한 함수
    인자 값이 isNext가 true이면 다음영상 false면 이전 영상 재생*/
    public void SwapVideoClip(bool isNext)
    {
        /*현재 재생 중인 여상의 넘버를 기준으로 체크한다.
        이전 영상 번호는 현재 영상보다 배열에서 인덱스 번호가 1이 작다.
        다음 영상 번호는 현재 영상보다 배열에서 인덱스 번호가 1이 크다.*/
        int setVCnum = curVCidx; //현재 재생 중인 영상의 넘버를 입력한다.
        vp.Stop();               //현재 재생 중인 비디오 클립을 중지한다.

        /*재생될 영상을 고르기 위한 과정*/
        if(isNext)
        {
            /*배열의 다음 영상을 재생한다.
             리스트 전체 길이보다 크면 클립을 리스트의 첫 번째 영상 지정한다.*/
            setVCnum = (setVCnum + 1) % vcList.Length;
            
        }
        else
        {
            /*배열의 이전 영상을 재생한다.*/
            setVCnum = ((setVCnum - 1) + vcList.Length) % vcList.Length;
        }

        vp.clip = vcList[setVCnum]; /*클립을 변경한다.*/
        vp.Play();                  /*바뀐 크립을 재생한다.*/
        curVCidx = setVCnum;        
    }

    public void SetVideoPlay(int num)
    {
        //현재 재생 중인 번호가 전달받은 번호와 다를 때만 실행
        if(curVCidx != null)
        {
            vp.Stop();              //영상을 멈춘다.
            vp.clip = vcList[num];  //클립을 변경한다.
            curVCidx = num;         //현재 재생 중인 번호 수정한다.
            vp.Play();              //재생한다.
        }
    }
}
