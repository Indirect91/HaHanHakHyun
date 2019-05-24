using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTouch : MonoBehaviour
{
    //바닥 이미지
    public Image markImg; //마크 이미지
    public float imgAlpha; //마크 이미지

    /*컴포넌트*/
    private Collider col;

    void Start()
    {
        col = GetComponent<Collider>();
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

            //광선, 맞은 대상의 정보, 사정거리
            if(col.Raycast(ray, out hitInfo, 1000))
            {
                
            }
        }


    }
}
