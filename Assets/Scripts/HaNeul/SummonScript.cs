using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonScript : MonoBehaviour
{
    //UI창
    [SerializeField] GameObject SummonsGroup; //소환할건지 선택창 보여줄 UI그룹
    [SerializeField] GameObject Joystic; //조이스틱 활성/비활성할 UI그룹

    //마법 이펙트
    [SerializeField] GameObject magicTonado; //45, 7, -3.8

    //소환중인가?
    bool isSummoning = false;
    //시간초
    int count = 0;

    void Update()
    {
        //플레이어가 소환하는 곳을 보고있을때
        if (PlayerFront.isSummons == true)
        {
            //E키를 누르면 선택지 보이기
            if (Input.GetKeyDown(KeyCode.E))
            {
                //소환선택지 UI활성화
                SummonsGroup.SetActive(true);
                //조이스틱 비활성화
                Joystic.SetActive(false);
            }
        }

        if (isSummoning == true)
        {
            count++;

            if(count % 360 == 0)
            {
                //조이스틱 활성화
                Joystic.SetActive(true);

            }
        }

    }

    //소환 Yes
    public void OnSummonYes()
    {
        Debug.Log("소화아아안!");

        //소환 선택지 UI비활성화
        SummonsGroup.SetActive(false);

        OnSummoning();
        isSummoning = true;
    }

    //소환 No
    public void OnSummonNo()
    {
        //소환선택지 UI비활성화
        SummonsGroup.SetActive(false);
        //조이스틱 활성화
        Joystic.SetActive(true);
    }

    //소환중
    public void OnSummoning()
    {
        //소환이펙트 토네이도 생성
       Instantiate(magicTonado, new Vector3(45.0f, 7.0f, -3.8f), Quaternion.Euler(-90.0f, 0.0f, 0.0f));
    }
}
