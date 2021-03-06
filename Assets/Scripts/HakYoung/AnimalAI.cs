﻿using System.Collections;
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
        Idle,
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
    public AnimalType animalType;

    //플레이어와 동물들의 위치를 저장할 변수.
    private Transform playerTr;
    private Transform animalTr;

    //Animator 를 사용하기 위한 변수
    private Animator animator;

    //동물 공격 스크럽트를 불러오기 위한 변수.
    private AnimalAttack animalAttack;


    //애니멀의 텍스쳐를 저장할 배열
    public Material[] material;

    //MeshRenderer 컴포넌트를 저장할 배열
    private SkinnedMeshRenderer _renderer;
    private Transform[] children;
    

    //공격과 추적의 사정거리.
    public float attackDist = 4.0f;
    public float traceDist = 10.0f;

    //죽었냐? 살았냐?
    public bool isDie = false;

    //이동을 제어하는 MoveAgent 클래스를 저장할 변수,
    private AnimalMoveAgent moveAgent;

    //공격주기
    int attackCool = 0;

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

        //동물의 타입을 자연상태(야생)로 두고
        animalType = AnimalType.Natural;
    }

    private void Start()
    {

        children = GetComponentsInChildren<Transform>();
        foreach(Transform child in children)
        {
            if(child.gameObject.name =="Bear")
            {
                int randomMaterial = Random.Range(0, material.Length - 1);

                child.GetComponent<SkinnedMeshRenderer>().material = material[randomMaterial];
            }
        }

        animator.SetBool("isPlayers", false);

    }
    void CheckState()
    {
        //안죽었다면 실행해라.
        if(!isDie)
        {
            if (animalType == AnimalType.Natural)
            {
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
            }
            else if(animalType == AnimalType.Player)
            {
                float distance = Vector3.Distance(playerTr.position, animalTr.position);

                if (PlayerInfo.clickTarget == null && distance < 5)
                {
                    state = AnimalState.Idle;
                }
                else if (PlayerInfo.clickTarget == null && distance >= 3)
                {
                    state = AnimalState.Patrol;
                }
                else if(PlayerInfo.clickTarget!=null && Vector3.Distance(PlayerInfo.clickTarget.GetComponent<Transform>().position, animalTr.position) > 3)
                {

                    state = AnimalState.Trace;
                }
                else if(PlayerInfo.clickTarget != null && Vector3.Distance(PlayerInfo.clickTarget.GetComponent<Transform>().position, animalTr.position) <= 3)
                {

                    state = AnimalState.Attack;
                }
                Debug.Log((int)state);
            }
        }
    }

    void Action()
    {
        if(!isDie)
        {
            if (animalType == AnimalType.Natural)
            {
                switch (state)
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
                        transform.LookAt(playerTr.position);
                        animator.SetBool(hashMove, false);
                        attackCool++;
                        if(attackCool %60*60 ==0)
                        {
    
                            PlayerDamage.currHp -= 4;
                            Debug.Log(PlayerDamage.currHp);
                        }
                        if (animalAttack.isAttack == false)
                        {
                            attackCool = -35;
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
            else if(animalType == AnimalType.Player)
            {

                float distance = Vector3.Distance(playerTr.position, animalTr.position);
                switch (this.state)
                {
                    
                    case AnimalState.Patrol:
                        animalAttack.isAttack = false;
                        animalTr.LookAt(playerTr.position);
                        animator.SetBool(hashMove, true);
                        animalTr.position = Vector3.Lerp(animalTr.position,playerTr.position, 0.01f);
                        
                        break;
                    case AnimalState.Idle:
                        animalAttack.isAttack = false;
                        animalTr.LookAt(playerTr.position);
                        animator.SetBool(hashMove, false);
                        
                        break;
                    case AnimalState.Trace:
                        animalAttack.isAttack = false;
                        animalTr.LookAt(PlayerInfo.clickTarget.GetComponent<Transform>().position);
                        animator.SetBool(hashMove, true);

                        animalTr.position = Vector3.Lerp(animalTr.position, PlayerInfo.clickTarget.GetComponent<Transform>().position, 0.01f);
                        break;
                    case AnimalState.Attack:
                        animalTr.LookAt(PlayerInfo.clickTarget.GetComponent<Transform>().position);

                        animator.SetBool(hashMove, false);

                        if (animalAttack.isAttack == false)
                        {
                            animalAttack.isAttack = true;
                        }
                        break;
                    case AnimalState.Die:
                        animalAttack.isAttack = false;
                        animalAttack.isAttack = false;
                        break;
                }
            }
        }
    }

    public AnimalType GetAnimalType() { return animalType; }
    public void SetAnimalType(AnimalType type) { animalType = type; }



    void FixedUpdate()
    {
        //Speed 파라미터에 이동속도를 전달.
        animator.SetFloat(hashSpeed, moveAgent.Speed);


        CheckState();

        //Action함수 실행
        Action();




    }
}

