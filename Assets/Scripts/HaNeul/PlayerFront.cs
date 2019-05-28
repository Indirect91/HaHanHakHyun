using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFront : MonoBehaviour
{
    //소환가능한가?
    public static bool isSummons = false;

    //플레이어의 앞이 부딪힘
    private void OnTriggerEnter(Collider coll)
    {
        //플레이어가 소환진을 보고있음
        if (coll.tag == "Sommons")
        {
            Debug.Log("소환가능");

            isSummons = true;
        }
    }

    //플레이어의 앞이 더이상 부딪히지 않음
    private void OnTriggerExit(Collider coll)
    {
        //플레이어가 소환진을 보고있지 않음
        if (coll.tag == "Sommons")
        {
            Debug.Log("소환불가");

            isSummons = false;
        }
    }



}
