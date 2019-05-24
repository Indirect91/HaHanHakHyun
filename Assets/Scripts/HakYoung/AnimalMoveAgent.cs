using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMoveAgent : MonoBehaviour
{

    //순찰 지점을 저장하기 위한 List 타입 변수 
    public List<Transform> wayPoint;
    //다음 인덱스로 넘어가기 위한 인트형 변수.
    public int nextIdx;

    void Start()
    {
        //하이러키 뷰의 WayPointGroup 게임오브젝트를 추출한다.
        var group = GameObject.Find("BearWayPointGroup");
        //포인트가 있다면.
        if (group != null)
        {

            group.GetComponentsInChildren<Transform>(wayPoint);
            wayPoint.RemoveAt(0);
        }
    }
}
