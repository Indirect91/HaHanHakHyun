using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Transform[] bearPoints;          //곰 생성 포인트를 담아놓기 위한 Transfrom배열.
    public Transform[] rabbitPoints;        //토끼 생성 포인트를 담아놓기 위한 Transfrom배열.
    public Transform[] cougarPoints;        //쿠거 생성 포인트를 담아놓기 위한 Transfrom배열.

    public Transform[] randomChestPoints;

    public Material[] bearMaterial;


    [SerializeField]                       //생성해놓지 않은 프리팹의 베어를 가져오기 위한 시리얼라이즈 필드
    public GameObject[] bears;              //베어를 담아놓기 위한 배열.
    [SerializeField] public GameObject[] rabbits;             //토끼를 담아놓기 위한 배열.
    [SerializeField] public GameObject[] cougars;            //쿠거를 담아놓기 위한 배열.

    [SerializeField] public GameObject randomChest;

    public float createTime = 4.0f;         //생성속도 4초당 1마리
    public int maxAnimal = 10;              //최대 생성될 수 있는 마릿수
    public bool isGameOver = false;         //게임종료시 추가생산을 막기위한 불변수.

    public int maxBox = 100;


    void Start()
    {
        bearPoints = GameObject.Find("BearSpawnPointGroup").GetComponentsInChildren<Transform>();       //생성포인트를 불러와서 배열에 담습니다.
        rabbitPoints = GameObject.Find("RabbitSpawnPointGroup").GetComponentsInChildren<Transform>();
        cougarPoints = GameObject.Find("CougarSpawnPointGroup").GetComponentsInChildren<Transform>();

        randomChestPoints = GameObject.Find("BoxSpawnPointGroup").GetComponentsInChildren<Transform>();

        if(randomChestPoints.Length > 0)
        {
            StartCoroutine(this.CreateRandomChest());
        }

        if (bearPoints.Length > 0)                  //생성포인트가 있다면
        {
            StartCoroutine(this.CreateBear());    //함수를 실행해서 베어를 생산합니다.
        }

        if(rabbitPoints.Length > 0)
        {
            StartCoroutine(this.CreateRabbit());
        }


    }

    IEnumerator CreateRandomChest()
    {
        while (true)
        {
            // 1 / 10000
            int boxCount = GameObject.FindGameObjectsWithTag("RandomBox").Length;
            if (boxCount < maxBox)
            {
                if (true)
                {
                    yield return new WaitForSeconds(createTime);

                    int idx = Random.Range(0, randomChestPoints.Length-1);
                    Instantiate(randomChest, randomChestPoints[idx].position, randomChestPoints[idx].rotation);

                    Debug.Log("박스 생성되는 중.....");
                }
            }
            else
                yield return null;
        }
    }

    IEnumerator CreateBear()          
    {
        //죽지 않았다면
        while (!isGameOver)
        {
            //현재 몇마리가 있는지 체크하고
            int bearCount = (int)GameObject.FindGameObjectsWithTag("Bear").Length;

            //맥스치보다 작다면
            if (bearCount < maxAnimal)
            {
                // 
                yield return new WaitForSeconds(createTime);
                
                //생성포인트 기존에 깔아놓은 장소중에서 랜덤으로 생성시키기 위한 인덱스값.
                int idx = Random.Range(1, bearPoints.Length);
                //곰도 종류별로 랜덤으로 생성시키기 위한 인덱스값
                int bearIdx = Random.Range(0, bears.Length - 1);
                //위의 정보들로 생성해라.
                Instantiate(bears[bearIdx], bearPoints[idx].position, bearPoints[idx].rotation);

                
            }
            //없다면 다음생성시간을 없에서 생성을 막아라.
            else
                yield return null;
        }
    }
    IEnumerator CreateRabbit()
    {
        //죽지 않았다면
        while (!isGameOver)
        {
            //현재 몇마리가 있는지 체크하고
            int ribbitCount = (int)GameObject.FindGameObjectsWithTag("Rabbit").Length;

            //맥스치보다 작다면
            if (ribbitCount < maxAnimal)
            {
                // 
                yield return new WaitForSeconds(createTime);

                //생성포인트 기존에 깔아놓은 장소중에서 랜덤으로 생성시키기 위한 인덱스값.
                int idx = Random.Range(1, rabbitPoints.Length);
                //토끼도 종류별로 랜덤으로 생성시키기 위한 인덱스값
                int rabbitsIdx = Random.Range(0, rabbits.Length - 1);
                //위의 정보들로 생성해라.
                Instantiate(rabbits[rabbitsIdx], rabbitPoints[idx].position, rabbitPoints[idx].rotation);
            }
            //없다면 다음생성시간을 없에서 생성을 막아라.
            else
                yield return null;
        }
    }

    IEnumerator CreateCougar()
    {
        //죽지 않았다면
        while (!isGameOver)
        {
            //현재 몇마리가 있는지 체크하고
            int cougarCount = (int)GameObject.FindGameObjectsWithTag("Cougar").Length;

            //맥스치보다 작다면
            if (cougarCount < maxAnimal)
            {
                // 
                yield return new WaitForSeconds(createTime);

                //생성포인트 기존에 깔아놓은 장소중에서 랜덤으로 생성시키기 위한 인덱스값.
                int idx = Random.Range(1, cougarPoints.Length);
                //토끼도 종류별로 랜덤으로 생성시키기 위한 인덱스값
                int cougarIdx = Random.Range(0, cougars.Length - 1);
                //위의 정보들로 생성해라.
                Instantiate(cougars[cougarIdx], cougarPoints[idx].position, cougarPoints[idx].rotation);
            }
            //없다면 다음생성시간을 없에서 생성을 막아라.
            else
                yield return null;
        }
    }

    //private void Update()
    //{
    //    if (Random.Range(1, 10000) == 648)
    //    {
    //        int idx = Random.Range(1, randomChestPoints.Length);
    //
    //        Instantiate(randomChest, randomChestPoints[idx].position, randomChestPoints[idx].rotation);
    //    }
    //}
}
