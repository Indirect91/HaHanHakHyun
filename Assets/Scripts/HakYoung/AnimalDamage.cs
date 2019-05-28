using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalDamage : MonoBehaviour
{


    private float hp = 100.0f;
    private float initHp = 100.0f;

    //현민이 형이 만든 Hpbar프리팹 가져오기 위한 변수.
    public GameObject hpBarPrefab;

    //생명 게이지의 위치를 보정해줄 오프셋
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    
    //부모가 될 Canvas 객체
    public Canvas uiCanvas;

    //생명수치에 따라서 fillAmount 속성을 변경할 Image;
    private Image hpBarImage;

    //나중에 하늘이가 공격했을시에 무언가 호출해주는걸 만들어 주면 그걸 통해서 데미지가 깎여야 하니까 만든 변수
    private GameObject player;

    void Start()
    {
        //플레이어 불러오기.
        player = GameObject.FindGameObjectWithTag("Player");

        //함수실행
        SetHpBar();
    }


    void SetHpBar()
    {
        //UI Canvas 불러오고
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();

        //캔버스 아래에다가 하위객체로 Hp바를 생성하고
        GameObject hpbar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);

        //fillAmount 속성을 변경할 이미지를 추출한다.
        hpBarImage = hpbar.GetComponentsInChildren<Image>()[1];

        //HpBar를 불러온다음에
        var _hpBar = hpbar.GetComponent<HpBar>();

        //체력바의 위치를 애니멀 자체의 위치로 옮겨주고
        _hpBar.targetTransform = this.gameObject.transform;

        //오프셋도 바꿔준다.
        _hpBar.offset = hpBarOffset;
    }
  
}
