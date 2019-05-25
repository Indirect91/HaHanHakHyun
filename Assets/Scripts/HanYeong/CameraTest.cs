using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    public Transform playerTr; //플레이어 트랜스폼

    Vector3 originDistance; //초기 거리 저장
    GameObject hitObstacle = null; //플레이어와 카메라 사이 오브젝트

    void Start()
    {
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>(); //플레이어의 위치를 받아올 트랜스폼 담아둠
        originDistance = this.transform.position - playerTr.position; //첫 거리 계산해서 저장
    }

    private void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(playerTr.position); //플레이어 위치를 월드포인트로 
        Ray rayFromCameraCentre = Camera.main.ScreenPointToRay(screenPos); //
        RaycastHit rayHit;
        Debug.DrawRay(rayFromCameraCentre.origin, rayFromCameraCentre.direction * 50, Color.yellow);



        if (Physics.Raycast(rayFromCameraCentre, out rayHit))
        {
           if(rayHit.collider.tag == "Environment")
            {
                hitObstacle = rayHit.collider.gameObject;

                rayHit.collider.GetComponent<MeshRenderer>().material.SetFloat("_Mode", 2);
                rayHit.collider.GetComponent<MeshRenderer>().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                rayHit.collider.GetComponent<MeshRenderer>().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                rayHit.collider.GetComponent<MeshRenderer>().material.SetInt("_ZWrite", 0);
                rayHit.collider.GetComponent<MeshRenderer>().material.DisableKeyword("_ALPHATEST_ON");
                rayHit.collider.GetComponent<MeshRenderer>().material.EnableKeyword("_ALPHABLEND_ON");
                rayHit.collider.GetComponent<MeshRenderer>().material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                rayHit.collider.GetComponent<MeshRenderer>().material.renderQueue = 3000;
            }
            else
            {
                if(hitObstacle !=null)
                {
                    hitObstacle.GetComponent<MeshRenderer>().material.SetFloat("_Mode", 0);
                    hitObstacle.GetComponent<MeshRenderer>().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    hitObstacle.GetComponent<MeshRenderer>().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    hitObstacle.GetComponent<MeshRenderer>().material.SetInt("_ZWrite", 1);
                    hitObstacle.GetComponent<MeshRenderer>().material.DisableKeyword("_ALPHATEST_ON");
                    hitObstacle.GetComponent<MeshRenderer>().material.DisableKeyword("_ALPHABLEND_ON");
                    hitObstacle.GetComponent<MeshRenderer>().material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    hitObstacle.GetComponent<MeshRenderer>().material.renderQueue = -1;

                    hitObstacle = null;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position = playerTr.position + originDistance;
    }
}
