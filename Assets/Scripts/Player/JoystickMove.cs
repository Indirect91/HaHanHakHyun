using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //터치와 관련된 이벤트가 들어있음

//IPointerDownHandler: 터치 or 마우스 클릭 시작했을 때 
//IPointerUpHandler : 터치 or 마우스 클릭을 뗐을 때
//IDragHandler : 드래그 했을 때
public class JoystickMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    //조이스틱 백그라운드는 앵커프리셋이 중심인 것을 써야한다.

    //캔버스와 캔버스의 child를 보면 일반 transform이 아닌 rect transform이다.
    //조이스틱의 범위 제한을 위해
    [SerializeField] RectTransform rect_background; //조이스틱 배경UI
    [SerializeField] RectTransform rect_Joystick; //조이스틱 UI

    //원형 크기의 정보 저장
    float radius; //반지름

    /*플레이어*/
    [SerializeField] GameObject gamePlayer; //플레이어 게임오브젝트
    [SerializeField] float moveSpeed; //플레이어 움직이는 속도
    [SerializeField] float rotSpeed; //플레이어 도는 속도

    public bool isMove = false; //움직이는 중인가?
    public bool isRun = false; //뛰는 중인가?
    public bool isAttack = false; //공격하는 중인가?

    Vector3 movement; //플레이어 움직임
    Vector3 rotate; //플레이어 회전

    bool isTouch = false; //조이스틱을 터치했는가?

    /*컴포넌트*/
    Rigidbody playerRigidbody;
    Animator playerAnim;

    void Start()
    {
        playerRigidbody = gamePlayer.GetComponent<Rigidbody>();
        playerAnim = gamePlayer.GetComponent<Animator>();

        //반지름 셋팅
        radius = rect_background.rect.width * 0.5f;
    }

    //모든 Physic를 업데이트 한다.
    private void FixedUpdate()
    {
        if(isTouch == true)
        {
            playerRigidbody.MovePosition(playerRigidbody.position + movement);
        }

        //회전하는 함수
        //turnning();
        //플레이어 애니메이션 함수
        Animating();
    }

    //케이디의 이동방법
    //void Update()
    //{
    //    if(isTouch == true)
    //    {
    //        gamePlayer.transform.position += movement;
    //    }
    //    
    //}

    //드래그
    public void OnDrag(PointerEventData eventData)
    {
        //마우스 현재 좌표 - 조이스틱 배경의 넓이
        Vector2 value = eventData.position - (Vector2)rect_background.position;

        //ClampMagnitude : 어떤 값을 가두는 것
        //(값, 가둘 범위) : 값은 가둘 범위를 넘지않게 가두는 것
        value = Vector2.ClampMagnitude(value, radius);
        //localPosition : 부모 객체에 대해서 상대적인 좌표
        rect_Joystick.localPosition = value;

        //거리차에따라 속도 다르게
        float distance = Vector2.Distance(rect_background.position, rect_Joystick.position) / radius;

        if (isMove == true)
        {
            //거리차 속도에 따라 움직임 다르게
            if (distance >= 0.4f) isRun = true;
            else isRun = false;
        }

        //방향값만 남게 만들기
        value = value.normalized;
        movement = new Vector3(value.x * moveSpeed * distance * Time.deltaTime, 0.0f, value.y * moveSpeed * distance * Time.deltaTime);

        //Quaternion deltaRotation = Quaternion.AngleAxis(value.x, transform.up) * Quaternion.AngleAxis(value.y, Vector3.right);



        //회전
        //rotate.Set(0.0f, value.x, 0.0f);
        float angle = Mathf.Atan2(-value.y, value.x) * Mathf.Rad2Deg;
        Debug.Log(angle+90);
        //Quaternion deltaRotation = Quaternion.AngleAxis(value.x, transform.up);
        
        Quaternion turnRotate = Quaternion.Euler(new Vector3(0,angle+90,0));
        playerRigidbody.MoveRotation(turnRotate);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;

        isMove = true;
    }

    //손가락 떼는 부분
    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false;
        isRun = false;
        isMove = false;

        //원위치로 돌아가게 만들기
        rect_Joystick.localPosition = Vector3.zero;
        movement = Vector3.zero;
    }

    //회전하는 함수
    void turnning()
    {
        //Quaternion == 회전만 다루는 클래스, 회전 값을 정할 수 있음
        //다만 transform과 같이 직접 접근하여 변경할 수 없고 아래와 같이 함수를 이용해 사용가능W

        //Quaternion turnRotation = Quaternion.LookRotation(movement); //-> 벡터가 가지고 있는 방향을 앞으로 보는 회전값을 만들어 줌
        //Quaternion newRotation = Quaternion.Slerp(playerRigidBody.rotation, turnRotation, rotSpeed * Time.deltaTime); //-> 휙휙 바뀌는게 아닌 더 부드럽게 바뀌게 하기위한 Slerp
        //playerRigidBody.MoveRotation(newRotation);

        rotate.Set(0.0f, movement.y, 0.0f);
        rotate = rotate.normalized * rotSpeed;

        Quaternion turnRotate = Quaternion.Euler(rotate);
        playerRigidbody.MoveRotation(playerRigidbody.rotation * turnRotate);
    }

    //플레이어 애니메이션 함수
    void Animating()
    {
        if (isAttack == true)
        {
            if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack01"))
            {
                isAttack = false;
            }
        }

        //공격하고 있는가 애니메이션
        playerAnim.SetBool("IsAttack", isAttack);

        //뛰고있는가 애니메이션
        playerAnim.SetBool("IsRun", isRun);

        //움직이는가 애니메이션
        playerAnim.SetBool("IsMove", isMove);
    }

    public void OnAttackButton()
    {
        isAttack = true;
    }
}
