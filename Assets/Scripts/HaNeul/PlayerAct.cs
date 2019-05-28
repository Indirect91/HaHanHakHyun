using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAct : MonoBehaviour
{
    public static bool isSwim = false;

    /*컴포넌트*/
    Animator anim;
    Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider coll)
    {
        //물에 부딫히면 행동이 수영하는 모션으로 바꿈
        if (coll.tag == "Water")
        {
            isSwim = true;

            anim.SetBool("IsSwim", isSwim);
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        //물에서 나오면 다시 움직임 모션으로
        if (coll.tag == "Water")
        {
            isSwim = false;

            anim.SetBool("IsSwim", isSwim);
        }
    }
}
