using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFront : MonoBehaviour
{
    public static bool isSummons = false;

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Sommons")
        {
            Debug.Log("소환가능");

            isSummons = true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Sommons")
        {
            Debug.Log("소환불가");

            isSummons = false;
        }
    }



}
