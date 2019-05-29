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
    [SerializeField] GameObject magicShowGreen; //45, 5, -3.8
    [SerializeField] GameObject magicShowPurple; //45, 5, -3.8

    /*아이템*/
    //음식
    [SerializeField] GameObject apple;
    [SerializeField] GameObject banana;
    [SerializeField] GameObject drinkCan;
    [SerializeField] GameObject hotdog;
    [SerializeField] GameObject roastMeat;
    //부가적인 오브젝트
    [SerializeField] GameObject key;
    [SerializeField] GameObject greenGem;
    [SerializeField] GameObject redGem;
    //펫
    [SerializeField] GameObject cat1;
    [SerializeField] GameObject cat2;
    [SerializeField] GameObject cat3;
    [SerializeField] GameObject cat4;

    //선택된 소환물
    GameObject showSummons;

    //소환중인가?
    bool isSummoning = false; //소환중인가?
    bool isCheckSummon = false; //소환물이 소환 되었는가?
    //시간초
    int count = 0;

    void Update()
    {
        //플레이어가 소환하는 곳을 보고있을때
        if (PlayerFront.isSummons == true && isSummoning == false)
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

            //토네이도랑 같이 안나오게 막음
            if (count % 180 == 0)
            {
                //랜덤 확률
                if (isCheckSummon == false)
                {
                    OnShowSummon();
                }
            }

            //무엇이 소환될지 결정되었다면
            if (isCheckSummon == true)
            {
                //특정높이보다 낮으면 올라오게 만들기
                if (showSummons.transform.position.y < 7.0f)
                {
                    showSummons.transform.Translate(Vector3.up * 2.0f * Time.deltaTime);
                }
            }

            if (count % 580 == 0)
            {
                Destroy(showSummons);

                isSummoning = false;
                isCheckSummon = false;
                count = 0;

                //조이스틱 활성화
                Joystic.SetActive(true);
            }
        }

    }

    //소환 Yes
    public void OnSummonYes()
    {
        Debug.Log("소환 전: " + PlayerInfo.ItemListCount());

        int invenSize = PlayerInfo.ItemListCount();

        if (PlayerInfo.ItemListCount() >= 20)
        {
            //소환선택지 UI비활성화
            SummonsGroup.SetActive(false);
            //조이스틱 활성화
            Joystic.SetActive(true);
        }
        //인벤토리 창에 가득차지 않으면 소환가능
        else if (PlayerInfo.ItemListCount() < 20)
        {
            //소환 선택지 UI비활성화
            SummonsGroup.SetActive(false);

            OnSummoning();
            isSummoning = true;
        }
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
       GameObject tonado = Instantiate(magicTonado, new Vector3(42.0f, 7.0f, -94.0f), Quaternion.Euler(-90.0f, 0.0f, 0.0f));

        //이펙트는 지우기
        Destroy(tonado, 6.0f);
    }

    //소환물이 보여지는 것
    public void OnShowSummon()
    {
        int randNum = Random.Range(1, 100);
        int itemQ = 0;

        if (randNum >= 1 && randNum <= 15)
        {
            //사과 소환
            showSummons = Instantiate(apple, new Vector3(42.0f, 0.0f, -94.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
            itemQ = 1;

            PlayerInfo.SaveItemList("Apple", 7, PlayerInfo.ItemType.Food);
        }
        else if (randNum > 15 && randNum <= 30)
        {
            //바나나 소환
            itemQ = 1;
            showSummons = Instantiate(banana, new Vector3(42.0f, 0.0f, -94.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));

            PlayerInfo.SaveItemList("Banana", 10, PlayerInfo.ItemType.Food);
        }
        else if (randNum > 30 && randNum <= 36)
        {
            //음료캔 소환
            itemQ = 2;
            showSummons = Instantiate(drinkCan, new Vector3(42.0f, 0.0f, -94.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));

            PlayerInfo.SaveItemList("DrinkCan", 12, PlayerInfo.ItemType.Food);
        }
        else if (randNum > 36 && randNum <= 56)
        {
            //고기 소환
            itemQ = 1;
            showSummons = Instantiate(roastMeat, new Vector3(42.0f, 0.0f, -94.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));

            PlayerInfo.SaveItemList("RoastMeat", 10, PlayerInfo.ItemType.Food);
        }
        else if (randNum > 56 && randNum <= 66)
        {
            //열쇠 소환
            itemQ = 2;
            showSummons = Instantiate(key, new Vector3(42.0f, 0.0f, -94.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));

            PlayerInfo.SaveItemList("Key", 0, PlayerInfo.ItemType.Key);
        }
        else if (randNum > 66 && randNum <= 73)
        {
            //초록보석 소환
            itemQ = 2;
            showSummons = Instantiate(greenGem, new Vector3(42.0f, 0.0f, -94.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        }
        else if (randNum > 73 && randNum <= 80)
        {
            //빨간보석 소환
            itemQ = 2;
            showSummons = Instantiate(redGem, new Vector3(42.0f, 0.0f, -94.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        }
        else if (randNum > 80 && randNum <= 85)
        {
            if (PlayerInfo.isCat1 == false)
            {
                //고양이1 소환
                itemQ = 3;
                showSummons = Instantiate(cat1, new Vector3(42.0f, 0.0f, -94.0f), Quaternion.Euler(0.0f, -90.0f, 0.0f));
                PlayerInfo.isCat1 = true;
            }
            else
            {
                //사과 소환
                itemQ = 1;
                showSummons = Instantiate(apple, new Vector3(42.0f, 0.0f, -94.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));

                PlayerInfo.SaveItemList("Apple", 7, PlayerInfo.ItemType.Food);
            }
        }
        else if (randNum > 85 && randNum <= 90)
        {
            if (PlayerInfo.isCat2 == false)
            {
                //고양이2 소환
                itemQ = 3;
                showSummons = Instantiate(cat2, new Vector3(42.0f, 0.0f, -94.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
                PlayerInfo.isCat2 = true;
            }
            else
            {
                //바나나 소환
                itemQ = 1;
                showSummons = Instantiate(banana, new Vector3(42.0f, 0.0f, -94.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));

                PlayerInfo.SaveItemList("Banana", 10, PlayerInfo.ItemType.Food);
            }
        }
        else if (randNum > 90 && randNum <= 95)
        {
            if (PlayerInfo.isCat3 == false)
            {
                //고양이3 소환
                itemQ = 3;
                showSummons = Instantiate(cat3, new Vector3(42.0f, 0.0f, -94.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
                PlayerInfo.isCat3 = true;
            }
            else
            {
                //사과 소환
                itemQ = 1;
                showSummons = Instantiate(apple, new Vector3(42.0f, 0.0f, -94.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));

                PlayerInfo.SaveItemList("Apple", 7, PlayerInfo.ItemType.Food);
            }
        }
        else if (randNum > 95 && randNum <= 100)
        {
            if (PlayerInfo.isCat4 == false)
            {
                //고양이4 소환
                itemQ = 3;
                showSummons = Instantiate(cat4, new Vector3(42.0f, 0.0f, -94.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
                PlayerInfo.isCat4 = true;
            }
            else
            {
                //바나나 소환
                itemQ = 1;
                showSummons = Instantiate(banana, new Vector3(42.0f, 0.0f, -94.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));

                PlayerInfo.SaveItemList("Banana", 10, PlayerInfo.ItemType.Food);
            }
        }

        GameObject effectObj;
        if (itemQ == 1)
        {
            effectObj = Instantiate(magicShowRed, new Vector3(42.0f, 5.0f, -94.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));

            Destroy(effectObj, 9.0f);
        }
        else if (itemQ == 2)
        {
            effectObj = Instantiate(magicShowGreen, new Vector3(42.0f, 5.0f, -94.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        }
        else if (itemQ == 3)
        {
            effectObj = Instantiate(magicShowPurple, new Vector3(42.0f, 5.0f, -94.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        }

        Debug.Log("소환: " + PlayerInfo.ItemListCount());

        //소환물이 뭐가 나올지 결정됨
        isCheckSummon = true;
    }
}
