using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//RequireComponent를 사용하는 스크립트를 추가하면, 요구되는 컴포너트가 자동으로 해당 게임오브젝트에 추가됩니다.
[RequireComponent(typeof(NavMeshAgent))]
public class AnimalMoveAgent : MonoBehaviour
{

    //순찰 지점을 저장하기 위한 List 타입 변수. 
    public List<Transform> wayPoint;
    //다음 인덱스로 넘어가기 위한 인트형 변수.
    public int nextIdx;
    //컴포넌트 저장 변수.
    private NavMeshAgent agent;


    //순찰상태나 추적상태일경우의 속도차이를 주기위한 변수.
    //readonly 를 사용하게 되면 읽기전용으로 바뀌어 뒤에서 값을 변경 할 수가 없다.
    private readonly float patrolSpeed = 1.0f;
    private readonly float traceSpeed = 5.0f;

    //회전할 때의 속도를 조절하는 계수
    private float damping = 1.0f;

    //동물 캐릭터의 Transfrom 컴포넌트를 저장할 변수.
    private Transform animalTr;

    //플래이어 캐릭터의 트랜스폼 컴포넌트를 저장할 변수.
    private Transform playerTr;

    //순찰상태인지 아닌지를 판단하기 위한 변수.
    private bool isPatrolling;
    //isPatrolling 의 겟셋.
    public bool patrolling
    {
        get { return isPatrolling; }
        set
        {
            
            isPatrolling = value;
            
            //순찰사태라면
            if(isPatrolling)
            {
                //그에맞는 속도로 이동해라.
                agent.speed = patrolSpeed;

                damping = 1.0f;

                MoveWayPoint();
            }
        }
    }
    private Vector3 _traceTarget;
    public Vector3 traceTarget
    {
        get { return _traceTarget; }
        set
        {
            _traceTarget = value;
            agent.speed = traceSpeed;

            damping = 7.0f;

            TraceTarget(_traceTarget);
        }
    }
    public float Speed
    {
        get { return agent.velocity.magnitude; }
    }
    void Start()
    {
        //동물 Transfrom 컴포넌트 추출 후 변수에 저장.
        animalTr = GetComponent<Transform>();

        //플레이어 트랜스폼 컴포넌트를 추출 후  변수에 저장.
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //NavMeshAgent 컴포넌트를 추출후 변수에 저장.
        agent = GetComponent<NavMeshAgent>();
        // 목적지에 가까워질수록 속도를 줄이는 옵션을 비활성화.
        agent.autoBraking = false;
        //처음에 기본속도는 순찰상태의 속도로 시작.
        agent.speed = patrolSpeed;


        //자동회전을 막는다.
        agent.updateRotation = false;

        //하이러키 뷰의 WayPointGroup 게임오브젝트를 추출한다.
        var group = GameObject.Find("BearWayPointGroup");
        //포인트가 있다면.
        if (group != null)
        {
            //wayPointGroup 하위의 모든 객채들의 Trancefrom을 꺼내서 wayPoint에 추가하고 
            group.GetComponentsInChildren<Transform>(wayPoint);
            //배열의 첫번쨰 항목을 지워라.
            wayPoint.RemoveAt(0);

            nextIdx = Random.Range(0, wayPoint.Count);
        }
        MoveWayPoint();
    }

    void MoveWayPoint()
    {
        //최단거리 경로 계산이 끝나지 않았으면 다음을 수행하지 않음.
        if (agent.isPathStale) return;

        //다음목적지를 wayPoint 배열에서 추출한 위치로 다음 목적지를 지정.
        agent.destination = wayPoint[nextIdx].position;

        //내비게이션 기능을 활성화해서 이동을 삭제함.
        agent.isStopped = false;
    }

    void TraceTarget(Vector3 pos)
    {
        //경로가 최적이 아니거나 유용하지 않다면 빠져나와라.
        if (agent.isPathStale) return;

        //여기까지 왔다면 최적의 경로거나 유용하다는 뜻이니

        //목적지를 잡아주고
        agent.destination = pos;

        //경로를 따라서 이동을 시작하게 해준다.
        agent.isStopped = false;
    }

    public void Stop()
    {
        //isStopped 를 켜서 이동을 중지시키고
        agent.isStopped = true;

        //바로 정지시기키 위해서 속도를 0으로  잡아버리고
        agent.velocity = Vector3.zero;

        //순찰을 정지해라.
        isPatrolling = false;
    }

    void Update()
    {

        //상태 겟셋을 알기 위해서 넣은 에너미 변수
        AnimalAI ai = GetComponent<AnimalAI>();
        if (ai.GetAnimalType() == AnimalAI.AnimalType.Natural)
        {
            //적 캐릭터가 이동중일 때만 회전한다.
            if (agent.isStopped == false)
            {
                //NavMeshAgent가 가야할 방향 백터를 쿼터니언 각도로 변환
                Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity);

                //보간함수를 사용하여 점진적을 회전시킵니다.
                animalTr.rotation = Quaternion.Slerp(animalTr.rotation
                    , rot, Time.deltaTime * damping);
            }

            //순찰상태가 아니라면 바로 빠져나와라.
            if (!isPatrolling) return;

            //NavMeshAgent가 이동하고 있고 목적지에 도착했는지 여부를 계산.
            if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
            {
                nextIdx = Random.Range(0, wayPoint.Count);
                MoveWayPoint();
            }
        }
    }
}
