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
    public static bool isAttack = false; //공격하는 중인가?

    Vector3 movement; //플레이어 움직임
    Vector3 rotate; //플레이어 회전
    Vector2 value; //플레이어 회전에 쓸 방향값을 다른 함수에서도 쓰려고 빼놓음

    bool isTouch = false; //조이스틱을 터치했는가?

    /*컴포넌트*/
    Rigidbody playerRigidbody;
    Animator playerAnim;
    AudioSource playerAudio;

    //공격용 이펙트
    [SerializeField] GameObject StarBall;
    [SerializeField] GameObject Thunderball;

    //이펙트 소리
    [SerializeField] AudioClip StarBallSD;
    [SerializeField] AudioClip ThunderBallSD;

    void Start()
    {
        playerRigidbody = gamePlayer.GetComponent<Rigidbody>();
        playerAnim = gamePlayer.GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        //반지름 셋팅
        radius = rect_background.rect.width * 0.5f;
    }

    //모든 Physic를 업데이트 한다.
    private void FixedUpdate()
    {
        if(isTouch == true && !playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack01"))
        {
            //플레이어 위치
            playerRigidbody.MovePosition(playerRigidbody.position + movement);
            //플레이어 방향
            Turnning();
        }

        //플레이어 애니메이션 함수
        Animating();
    }
    
    //드래그
    public void OnDrag(PointerEventData eventData)
    {
        //마우스 현재 좌표 - 조이스틱 배경의 넓이
        value = eventData.position - (Vector2)rect_background.position;

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
        
    }

    //손가락 눌렀을때
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

    void Turnning()
    {
        /*플레이어 회전*/
        float angle = Mathf.Atan2(-value.y, value.x) * Mathf.Rad2Deg;

        Quaternion turnRotate = Quaternion.Euler(new Vector3(0, angle + 90, 0));
        playerRigidbody.MoveRotation(turnRotate);
    }
    
    //플레이어 애니메이션 함수
    void Animating()
    {
        //어택중이라면
        if (isAttack == true)
        {
            //플레이어 공격모션이 들어갔을때 다시 false;
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

    //스타볼 나타나게 하는 마법 공격버튼
    public void OnAttackButton_StarBall()
    {
        if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack01"))
        {
            if (PlayerInfo.clickTarget != null && PlayerAct.isSwim == false)
            {
                isAttack = true;

                //마법이 나타날 지점
                Vector3 posUp = new Vector3(0.0f, 2.0f, 0.0f); //좀 위로 옮기려고 넣어봄
                Vector3 pos = PlayerInfo.clickTarget.transform.position + posUp;

                //마법이펙트 오브젝트를 생성 
                //Instantiate:게임오브젝트의 클론을 생성 (복제할 오브젝트, 오브젝트 위치, 회전값)
                Instantiate(StarBall, pos, Quaternion.Euler(0.0f, 0.0f, 0.0f));

                //사운드 발생
                playerAudio.clip = StarBallSD;
                playerAudio.Play();
            }
        }
    }

    //썬더볼 나타나게 하는 마법 공격버튼
    public void OnAttackButton_ThunderBall()
    {
        if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack01"))
        {
            if (PlayerInfo.clickTarget != null && PlayerAct.isSwim == false)
            {
                isAttack = true;

                //마법이 나타날 지점
                Vector3 posUp = new Vector3(0.0f, 2.0f, 0.0f); //좀 위로 옮기려고 넣어봄
                Vector3 pos = PlayerInfo.clickTarget.transform.position + posUp;

                //마법이펙트 오브젝트를 생성 
                //Instantiate:게임오브젝트의 클론을 생성 (복제할 오브젝트, 오브젝트 위치, 회전값)
                Instantiate(Thunderball, pos, Quaternion.Euler(0.0f, 0.0f, 0.0f));

                //사운드 발생
                playerAudio.clip = ThunderBallSD;
                playerAudio.Play();
            }
        }
    }
}
