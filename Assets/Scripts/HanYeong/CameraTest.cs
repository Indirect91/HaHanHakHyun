using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    public Transform playerTr; //플레이어

    Vector3 originDistance; //초기 거리

    void Start()
    {
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>(); //플레이어의 위치를 받아올 트랜스폼 담아둠
        originDistance = this.transform.position - playerTr.position; //첫 거리는 
    }

    private void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(playerTr.position);
        Ray rayFromCameraCentre = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit rayHit;
        Debug.DrawRay(rayFromCameraCentre.origin, rayFromCameraCentre.direction * 50, Color.yellow);



        if (Physics.Raycast(rayFromCameraCentre, out rayHit))
        {
           if(rayHit.collider.tag == "Environment")
            {
                rayHit.collider.GetComponent<MeshRenderer>().material.SetFloat("_Mode", 3);
                 rayHit.collider.GetComponent<MeshRenderer>().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                 rayHit.collider.GetComponent<MeshRenderer>().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                 rayHit.collider.GetComponent<MeshRenderer>().material.SetInt("_ZWrite", 0);
                 rayHit.collider.GetComponent<MeshRenderer>().material.DisableKeyword("_ALPHATEST_ON");
                 rayHit.collider.GetComponent<MeshRenderer>().material.EnableKeyword("_ALPHABLEND_ON");
                 rayHit.collider.GetComponent<MeshRenderer>().material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                rayHit.collider.GetComponent<MeshRenderer>().material.renderQueue = 3000;
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position = playerTr.position + originDistance;
    }
}
