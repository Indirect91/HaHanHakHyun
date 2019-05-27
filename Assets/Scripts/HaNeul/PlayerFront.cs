using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFront : MonoBehaviour
{
    private void OnTriggerStay(Collider coll)
    {
        Debug.Log("Stay?");

        if (coll.tag == "Sommons")
        {
            Debug.Log("Stay 소환!");
        }

        if (coll.gameObject.layer == LayerMask.NameToLayer("Sommons"))
        {
            Debug.Log("Stay L 소환!");
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        Debug.Log("Enter?");

        if (coll.tag == "Sommons")
        {
            Debug.Log("Enter 소환!");
        }

        if (coll.gameObject.layer == LayerMask.NameToLayer("Sommons"))
        {
            Debug.Log("Enter L 소환!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Sommons")
        {
            Debug.Log("Stay 소환!");
        }
    }


}
