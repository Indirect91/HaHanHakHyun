using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBoxTest : MonoBehaviour
{
    bool isOpen = false;        //한번이라도 열렸는지 여부
    bool isAboutToOpen = false; //열리기 직전인 상태인지
    GameObject toShow;          //캔버스 onOff
    Animator boxAnim;           //박스 에니메이터


    void Start()
    {
        toShow = transform.Find("PressFPanel").gameObject;
        toShow.SetActive(false);
        boxAnim = GetComponentInChildren<Animator>();
        isOpen = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isOpen == false && collision.gameObject.tag == "Player")
        {
            toShow.SetActive(true);
            isAboutToOpen = true;
            toShow.transform.LookAt(Camera.main.transform);
            boxAnim.SetBool("isEncounter", true);

        }
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (isOpen == false && collision.gameObject.tag == "Player")
    //    {
    //        isAboutToOpen = true;
    //    }
    //}

    private void OnCollisionExit(Collision collision)
    {
        if (isOpen == false && collision.gameObject.tag == "Player")
        {
            toShow.SetActive(false);
            isAboutToOpen = false;
            boxAnim.SetBool("isEncounter", false);
        }
    }

    void Update()
    {
        if (isAboutToOpen && isOpen==false && Input.GetKey(KeyCode.F))
        {
            boxAnim.SetTrigger("trigOpen");
            toShow.SetActive(false);
            isOpen = true;
        }
    }
}
