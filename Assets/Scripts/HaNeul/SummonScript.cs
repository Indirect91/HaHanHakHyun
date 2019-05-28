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
    [SerializeField] GameObject magicShowRed; //45, 5, -3.8

    /*아이템*/
    //음식
    [SerializeField] GameObject apple;
    [SerializeField] GameObject banana;
    [SerializeField] GameObject drinkCan;
    [SerializeField] GameObject hotdog;
    [SerializeField] GameObject roastMeat;

    GameObject showSummons;

    //소환중인가?
    bool isSummoning = false; //소환중인가?
    bool isCheckSummon = false; //소환물이 소환 되었는가?
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

            //무엇이 소환될지 결정되었다면
            if (isCheckSummon == true)
            {
                Debug.Log("올라와라아아아~");

                //특정높이보다 낮으면 올라오게 만들기
                if (showSummons.transform.position.y < 7.0f)
                {
                    showSummons.transform.Translate(Vector3.up * 2.0f * Time.deltaTime);
                }
            }

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
       GameObject tonado = Instantiate(magicTonado, new Vector3(45.0f, 7.0f, -3.8f), Quaternion.Euler(-90.0f, 0.0f, 0.0f));

        //이펙트는 지우기
        Destroy(tonado, 6.0f);

        //랜덤 확률
        if (isCheckSummon == false)
        {
            int randNum = Random.Range(1, 100);

            if (randNum % 10 == 0)
            {
                Debug.Log("고기 소환");
                showSummons = Instantiate(roastMeat, new Vector3(45.0f, 0.0f, -3.8f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
            }
            else
            {
                Debug.Log("바나나 소환");
                showSummons = Instantiate(banana, new Vector3(45.0f, 0.0f, -3.8f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
            }

            //소환물이 뭐가 나올지 결정됨
            isCheckSummon = true;
        }
    }
}
