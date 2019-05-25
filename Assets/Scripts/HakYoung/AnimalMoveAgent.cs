using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//RequireComponent를 사용하는 스크립트를 추가하면, 요구되는 컴포너트가 자동으로 해당 게임오브젝트에 추가됩니다.
[RequireComponent(typeof(NavMeshAgent))]
public class AnimalMoveAgent : MonoBehaviour
{

    //순찰 지점을 저장하기 위한 List 타입 변수 
    public List<Transform> wayPoint;
    //다음 인덱스로 넘어가기 위한 인트형 변수.
    public int nextIdx;
    //컴포넌트 저장 변수.
    private NavMeshAgent agent;


    void Start()
    {
        //NavMeshAgent 컴포넌트를 추출후 변수에 저장.
        agent = GetComponent<NavMeshAgent>();
        // 목적지에 가까워질수록 속도를 줄이는 옵션을 비활성화.
        agent.autoBraking = false;

        //하이러키 뷰의 WayPointGroup 게임오브젝트를 추출한다.
        var group = GameObject.Find("BearWayPointGroup");
        //포인트가 있다면.
        if (group != null)
        {

            group.GetComponentsInChildren<Transform>(wayPoint);
            wayPoint.RemoveAt(0);
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

    void Update()
    {
        //NavMeshAgent가 이동하고 있고 목적지에 도착했는지 여부를 계산.
        if(agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
        {
            nextIdx = ++nextIdx % wayPoint.Count;
            MoveWayPoint();
        }
    }
}
