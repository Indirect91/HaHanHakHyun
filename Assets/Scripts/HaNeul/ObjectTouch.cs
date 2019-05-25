using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTouch : MonoBehaviour
{
    /*컴포넌트*/
    Collider col;
    AudioSource audioSource;
    
    void Start()
    {
        col = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //레이캐스터 : 레이저를 쏴서 맞은 오브젝트의 정보를 가져옴
        if(Input.GetMouseButtonDown(0)) //0:좌버튼
        {
            //ScreenPointToRay(마우스 좌표) -> 좌표로 레이저를 쏨
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //레이저를 쏴서 맞은 것에 대한 정보를 담음
            RaycastHit hitInfo;

            //(광선, 맞은 대상의 정보, 사정거리) -> col이 광선에 맞았다면
            if (col.Raycast(ray, out hitInfo, 1000))
            {
                //만약 플레이어 클릭정보가 비어있다면 그대로 넣는다
                if (PlayerInfo.clickTarget == null)
                {
                    //선택한 대상 넣기
                    PlayerInfo.clickTarget = this.gameObject;

                    //체킹 사운드 발생
                    audioSource.Play();
                }
                else if (PlayerInfo.clickTarget != null && PlayerInfo.clickTarget != gameObject)
                //플레이어 클릭정보가 비어있지 않다면
                {
                    //이미 켜져있는 타겟 비활성화
                    PlayerInfo.clickTarget.transform.Find("Mark Canvas").gameObject.SetActive(false);

                    //클릭한 타겟으로 대상 넣음
                    PlayerInfo.clickTarget = this.gameObject;

                    //체킹 사운드 발생
                    audioSource.Play();
                }

                //클릭대상이 무엇인지 확인용
                Debug.Log(PlayerInfo.clickTarget.name);

                //플레이어가 찍은 대상의 마크 활성화
                PlayerInfo.clickTarget.transform.Find("Mark Canvas").gameObject.SetActive(true);

            }

        }
    }

    

}
