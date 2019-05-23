using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /*플레이어 Public*/
    public float speed = 1.0f; //속도
    public float rotSpeed = 5.0f; //도는 속도
    public bool isMove = false; //움직이는 중인가?
    public bool isRun = false; //뛰는 중인가?

    /*플레이어 Private*/
    Vector3 movement; //움직임
    Vector3 rotate; //회전

    /*컴포넌트*/
    Rigidbody playerRigidBody;
    Animator anim;
    
    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    //모든 Physic를 업데이트 한다.
    private void FixedUpdate()
    {
       //키 입력시 넣어지게
       float h = Input.GetAxisRaw("Horizontal");
       float v = Input.GetAxisRaw("Vertical");

        //움직이는 함수
        Move(v);
        //회전하는 함수
        Turnning(h);

        //애니메이션 함수
        Animating(h, v);
    }

    //움직이는 함수
    void Move(float v)
    {
        //움직임 벡터 셋팅
        //movement.Set(h, 0.0f, v);
        //
        ////↑→ 방향키를 동시에 눌렀을때 √2가 아닌 1로 움직이도록 만들기 위해 단위벡터로 만들어 이동
        //movement = movement.normalized * speed * Time.deltaTime;
        //
        //playerRigidBody.MovePosition(transform.position + movement);

        playerRigidBody.MovePosition(playerRigidBody.position + transform.forward * v * speed * Time.deltaTime);
    }

    //회전하는 함수
    void Turnning(float h)
    {
        //Quaternion == 회전만 다루는 클래스, 회전 값을 정할 수 있음
        //다만 transform과 같이 직접 접근하여 변경할 수 없고 아래와 같이 함수를 이용해 사용가능W

        //Quaternion turnRotation = Quaternion.LookRotation(movement); //-> 벡터가 가지고 있는 방향을 앞으로 보는 회전값을 만들어 줌
        //Quaternion newRotation = Quaternion.Slerp(playerRigidBody.rotation, turnRotation, rotSpeed * Time.deltaTime); //-> 휙휙 바뀌는게 아닌 더 부드럽게 바뀌게 하기위한 Slerp
        //playerRigidBody.MoveRotation(newRotation);

        rotate.Set(0.0f, h, 0.0f);
        rotate = rotate.normalized * rotSpeed;

        //euler
        Quaternion turnRoate = Quaternion.Euler(rotate);
        playerRigidBody.MoveRotation(playerRigidBody.rotation * turnRoate);
    }

    //애니메이션 함수
    void Animating(float h, float v)
    {
        //움직이고 있음
        if (isMove == true)
        {
            //Shift 누르면 뛰기
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 4.0f;
                isRun = true;
            }
            //아니면 걷기
            else
            {
                speed = 1.0f;
                isRun = false;
            }

            anim.SetBool("IsRun", isRun);
        }

        //움직이고 있는지 확인
        isMove = h != 0.0f || v != 0.0f;
        anim.SetBool("IsMove", isMove);
    }
}
