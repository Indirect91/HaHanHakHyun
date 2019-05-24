using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalTest : MonoBehaviour
{
    SphereCollider coll;
    void Start()
    {
        coll = GetComponent<SphereCollider>();
    }

    //일단 충돌로 만들었는데, 충돌 대신 마우스 클릭되었을때겠지?
    private void OnCollisionEnter(Collision collision)
    {
        //만약 기존 터치상태였던 적이 없다면
        if(PlayerTest.clickedTarget ==null)
        {
            //플레이어가 이제 터치된 상대를 알게됨
            PlayerTest.clickedTarget = this.gameObject;
            //표시하려고 강제로 위로 일단 띄움
            this.transform.Translate(new Vector3(0, 4, 0));
        }
        else//기존 터치된 적이 null이 아니라면, 즉 존재했다면
        {
            //기존 플레이어가 알고있는 게임오브젝트의 속성을 바꾸고(빨간 원을 끄고)
            PlayerTest.clickedTarget.transform.Translate(new Vector3(0, -4, 0));
         
            //대상을 바꿈   
            PlayerTest.clickedTarget = this.gameObject;
            this.transform.Translate(new Vector3(0, 4, 0));
        }
    }









}
