using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAI : MonoBehaviour
{
    //초식동물 육식동물 구분.
    public enum Eating
    {
        weed,
        meat
    }
    //동물의 상태구분.
    public enum AnimalState
    {
        Patrol,
        Trace,
        Attack,
        Die
    }

    //동물의 타입구분.
    public enum AnimalType
    {
        Natural,        //기본상태
        Player          //플레이어 귀속상태.
    }

    //상태를 저장할 변수들.
    public AnimalState state;
    public Eating eating;
    public AnimalType type;


    //플레이어와 동물들의 위치를 저장할 변수.
    private Transform playerTr;
    private Transform animalTr;

    //Animator 를 사용하기 위한 변수
    private Animator animator;

    //동물 공격 스크럽트를 불러오기 위한 변수.
    private AnimalAttack animalAttack;


    //공격과 추적의 사정거리.
    public float attackDist = 4.0f;
    public float traceDist = 10.0f;

    //죽었냐? 살았냐?
    public bool isDie = false;

    //이동을 제어하는 MoveAgent 클래스를 저장할 변수,
    private AnimalMoveAgent moveAgent;

    //코루틴에서 사용할 지연시간 변수라는데 공부하고 추가 주석을 달겠슴.
    //이름만 보면 잠시만 기다리라는거같음.
    private WaitForSeconds ws;

    //애니메이터 컨트롤러에 정의한 파라미터의 해시값을 미리 추출
    private readonly int hashMove = Animator.StringToHash("IsMove");
    private readonly int hashSpeed = Animator.StringToHash("Speed");

    private void Awake()
    {
        //var로 자료형 자동으로 찾고 태크이름이 Player라는 게임오브젝트를 찾아라.
        var player = GameObject.FindGameObjectWithTag("Player");

        //게임오브젝트가 있다면 Transfrom컴포넌트를 가져와라.
        if (player != null)
            playerTr = player.GetComponent<Transform>();

        //자기 자신의 Transfrom컴포넌트로 가져와야지
        animalTr = GetComponent<Transform>();

        //Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();

        //moveAgent 컴포넌트 가져오기
        moveAgent = GetComponent<AnimalMoveAgent>();

        //AnimalAttack 스트럭트 가져오기.
        animalAttack = GetComponent<AnimalAttack>();

        //코루틴의 지연시간을 생성.  0.3초마다 번갈아가면서 실행시키려고 하는건가?
        ws = new WaitForSeconds(0.3f);
    }


    private void OnEnable()
    {
        //CheckState 함수 실행
        StartCoroutine(CheckState());

        //Action함수 실행
        StartCoroutine(Action());
    }

    IEnumerator CheckState()
    {
        //안죽었다면 실행해라.
        while(!isDie)
        {
            //실수로라도 죽었는데 들어왔다?
            if (state == AnimalState.Die)
                //나가라.
                yield break;

            //플레이어랑 동물이랑 거리를 계산. Vector3.Distance보다 sqrMagnitude 로하게되면 훨씬 빠르다고 함.
            //float distance = (playerTr.position - animalTr.position).sqrMagnitude;
            float distance = Vector3.Distance(playerTr.position, animalTr.position);

            //공격반경안에 들어오면 상태를 공격으로 바꾸고
            if (distance <= attackDist)
            {
                state = AnimalState.Attack;
            }
            // 추적반경안에 들어오면 상태를 추적으로 바꾸고,
            else if (distance <= traceDist)
            {
                state = AnimalState.Trace;
            }
            //이도저도 아니면 순찰상태로 바꿔라.
            else
                state = AnimalState.Patrol;
            yield return ws;
        }
    }

    IEnumerator Action()
    {
        while(!isDie)
        {
            //지정한 ws시간만큼 대기하고
            yield return ws;

            
            switch(state)
            {
                case AnimalState.Patrol:
                    //순찰모드를 활성화.
                    animalAttack.isAttack = false;
                    moveAgent.patrolling = true;
                    animator.SetBool(hashMove, true);
                    break;
                case AnimalState.Trace:
                    //플레이어의 위치를 넘겨서 추적모드로 변경.
                    moveAgent.traceTarget = playerTr.position;
                    animator.SetBool(hashMove, true);
                    break;
                case AnimalState.Attack:
                    //순찰 및 추적을 멈춘다.
                    moveAgent.Stop();
                    animator.SetBool(hashMove, false);

                    if(animalAttack.isAttack == false)
                    {
                        animalAttack.isAttack = true;
                    }
                    break;
                case AnimalState.Die:
                    //순찰 및 추적을 멈춘다.
                    animalAttack.isAttack = false;
                    moveAgent.Stop();
                    break;
            }
        }
    }

    void Update()
    {
        //Speed 파라미터에 이동속도를 전달.
        animator.SetFloat(hashSpeed, moveAgent.Speed);
    }
}
