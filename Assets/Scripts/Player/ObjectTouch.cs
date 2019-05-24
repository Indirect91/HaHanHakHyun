using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTouch : MonoBehaviour
{
    /*바닥 마크 이미지*/
    public Image markImg; //마크 이미지
    public float imgAlpha; //마크 이미지
    public Color markColor = new Color(1.0f, 1.0f, 1.0f, 1.0f); //알파값만 사용하려고 만듦

    public static bool isCheck = false; //오브젝트가 체크 되었는가

    /*컴포넌트*/
    private Collider col;
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
            if (col.Raycast(ray, out hitInfo, 1000) && isCheck == false)
            {
                isCheck = true;

                Debug.Log("캡슐 마킹");
                markImg.color = markColor;

                //체킹 사운드 발생
                audioSource.Play();

                //만약 플레이어 클릭정보가 비어있다면 그대로 넣는다
                //if (PlayerInfo.clickTarget == null)
                //{
                //    isCheck = true;
                //
                //    Debug.Log("캡슐 마킹");
                //    markImg.color = markColor;
                //
                //    //체킹 사운드 발생
                //    audioSource.Play();
                //}
                //else //플레이어 클릭정보가 비어있지 않다면
                //{
                //
                //}
            }

        }
    }

}
