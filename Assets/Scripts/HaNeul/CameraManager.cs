using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform playerTr; //플레이어

    Vector3 originDistance;

    void Start()
    {
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        originDistance = this.transform.position - playerTr.position;
    }

    private void FixedUpdate()
    {
        transform.position = playerTr.position + originDistance;
    }
}
