using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBoxTest : MonoBehaviour
{
    SphereCollider aboutToOpen; //열릴 수 있는 지점에 도착여부 판단 구형 콜라이더
    bool isOpen = false;
    Canvas toShow;



    // Start is called before the first frame update
    void Start()
    {
        aboutToOpen = GetComponent<SphereCollider>();
        toShow = GetComponentInChildren<Canvas>();
        isOpen = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
    
        
    }

    private void OnCollisionExit(Collision collision)
    {
        

    }

    private void OnCollisionStay(Collision collision)
    {
        

    }







    // Update is called once per frame
    void Update()
    {
        
    }
}
