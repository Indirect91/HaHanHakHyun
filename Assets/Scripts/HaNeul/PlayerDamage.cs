﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    //HP 수치
    float MaxHp = 25.0f; //최대 hp
    float currHpBar; //현재 hp바
    public static float currHp; //현재 hp기준

    //HP바 이미지
    public Image hpBarImg;
    //HP바 색상
    //readonly Color initColor = new Vector4(0, 1.0f, 0.0f, 0.8f);
    //Color currColor;

    float speed = 10.0f;

    //플레이어 상태
    bool isDie = false;

    /*컴포넌트*/
    Animator playerAnim;
    //[SerializeField] JoystickMove joystickMove;
    [SerializeField] GameObject UICanvas;
    [SerializeField] GameObject PlayerDieUI;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();

        //현재 hp 설정
        currHpBar = PlayerInfo.playerHp;
        currHp = PlayerInfo.playerHp;

        //hp 초기 색상
        //hpBarImg.color = initColor;
        //currColor = initColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            currHp -= 4.0f;
        }

        if (currHp < currHpBar) //Hp바 감소
        {
            currHpBar -= speed * Time.deltaTime;
        }

        if (currHp < 0 && !isDie)
        {
            Debug.Log("죽음");

            Death();
        }

        //Hp바 변경
        DisplayHpBar();
    }

    //hp바 색상 및 크기 변경
    void DisplayHpBar()
    {
        //생명수치 50%일때 초록->노랑
        //if ((currHp / MaxHp) > 0.5f)
        //{
        //    currColor.r = (1 - (currHp / MaxHp)) * 2.0f;
        //}
        //else //생명수치 0%일떄 노랑->빨강
        //{
        //    currColor.g = (currHp / MaxHp) * 2.0f;
        //}

        //색상변경
        //hpBarImg.color = currColor;
        //크기변경
        hpBarImg.fillAmount = (currHpBar / MaxHp);
    }

    void Death()
    {
        isDie = true;

        playerAnim.SetTrigger("Die");

        UICanvas.SetActive(false);
        PlayerDieUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
