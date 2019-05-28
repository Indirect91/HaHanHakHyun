using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalDamage : MonoBehaviour
{
    

    private float hp = 10.0f;
    private float initHp = 10.0f;

    //현민이 형이 만든 Hpbar프리팹 가져오기 위한 변수.
    public GameObject hpBarPrefab;

    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    
    //부모가 될 Canvas 객체
    public Canvas uiCanvas;

    //생명수치에 따라서 fillAmount 속성을 변경할 Image;
    private Image hpBarImage;

    private bool playerAttacked = false;
    void Start()
    {
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

    private void Update()
    {
        if (PlayerInfo.clickTarget == this.gameObject)
        {
            Attacked(JoystickMove.isAttack);
        }
    }

    void Attacked(bool attacked)
    {
        if (JoystickMove.isAttack == true && playerAttacked == true) return;

        playerAttacked = JoystickMove.isAttack;
       
        if(playerAttacked == true)
        {
            hp -= PlayerInfo.playerAtt1;
            Debug.Log(hp);
        }
      
        if (hp <= 0.0f)
        {
            if (true)
            {
                AnimalAI ai = GetComponent<AnimalAI>();
                ai.SetAnimalType(AnimalAI.AnimalType.Player);
                PlayerInfo.clickTarget.SetLayer(21);
                PlayerInfo.clickTarget = null;

            }
            else
            {
                GetComponent<AnimalAI>().state = AnimalAI.AnimalState.Die;

                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
            }
        }
    }
}
