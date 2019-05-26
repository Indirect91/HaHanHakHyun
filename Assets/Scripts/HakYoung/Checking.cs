using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CougarMove : MonoBehaviour
{

    private SphereCollider col;
    private GameObject player;
    private bool isCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        col = GetComponent<SphereCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        isCheck = true;
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        isCheck = false;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(isCheck);
    }
}
