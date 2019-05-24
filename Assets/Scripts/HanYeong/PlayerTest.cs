using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    //퍼블릭 스태틱을 이용하면, 타 클래스에서 이거 건드릴 수 있음!
    public static GameObject clickedTarget = null;
    
    public float speed = 10;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if(clickedTarget!=null)
        {
            this.transform.LookAt(clickedTarget.GetComponent<Transform>().position);
        }


        rb.AddForce(movement * speed);
    }
}
