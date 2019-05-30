using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    public Transform playerTr; //플레이어 트랜스폼
    static public GameObject showTogether; //줌인할 대상
    public static bool isStart = false;
    bool stillMoving = true;
    Vector3 originDistance; //초기 거리 저장
    Vector3 startDistance;
    GameObject hitObstacle = null; //플레이어와 카메라 사이 오브젝트

    public static bool rotateRight = false;
    public static bool rotateLeft = false;
    float rotateRightAmount =0;
    float rotateLeftAmount = 0;



    void Start()
    {
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>(); //플레이어의 위치를 받아올 트랜스폼 담아둠
        originDistance = this.transform.position - playerTr.position; //첫 거리 계산해서 저장
        startDistance = 3 * originDistance;
    }

    private void Update()
    {

           if(isStart)
        { 
            Vector3 screenPos = Camera.main.WorldToScreenPoint(playerTr.position); //플레이어 위치를 월드포인트로 
            Ray rayFromCameraCentre = Camera.main.ScreenPointToRay(screenPos); //
            RaycastHit rayHit;
            Debug.DrawRay(rayFromCameraCentre.origin, rayFromCameraCentre.direction * 50, Color.yellow);



            if (Physics.Raycast(rayFromCameraCentre, out rayHit))
            {
                if (rayHit.collider.tag == "Environment")
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
                    Debug.Log("Asd");
                }
                else
                {
                    if (hitObstacle != null)
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
    }

    private void FixedUpdate()
    {
        if (!isStart)
        {
            transform.RotateAround(playerTr.transform.position, Vector3.up, 20 * Time.deltaTime);
            startDistance = this.transform.position - playerTr.position; //첫 거리 계산해서 저장
            transform.position = playerTr.position + startDistance;
        }

        else if (isStart && stillMoving)
        {
            transform.LookAt(playerTr.position);
            transform.position = Vector3.Lerp(transform.position, playerTr.position + originDistance,0.02f);
            if(Vector3.Distance(transform.position, playerTr.position + originDistance)<1)
            {
                stillMoving = false;
            }
        }
        else
        {


            transform.LookAt(playerTr);

            rotateLeft = false;
            rotateRight = false;

            if (Input.GetKey(KeyCode.O))
            {
                rotateLeft = true;
            }
            else if (Input.GetKey(KeyCode.P))
            {
                rotateRight = true;
            }


            if (rotateLeft)
            {
                transform.RotateAround(playerTr.transform.position, Vector3.up, 20 * Time.deltaTime);
                originDistance = this.transform.position - playerTr.position; //첫 거리 계산해서 저장
                                                                              // transform.Translate(Vector3.left *3* Time.deltaTime);
                                                                              // originDistance = this.transform.position - playerTr.position; //첫 거리 계산해서 저장

            }
            else if (rotateRight)
            {
                transform.RotateAround(playerTr.transform.position, Vector3.up, -20 * Time.deltaTime);
                originDistance = this.transform.position - playerTr.position; //첫 거리 계산해서 저장
                                                                              //transform.Translate(Vector3.right *3* Time.deltaTime);
                                                                              //originDistance = this.transform.position - playerTr.position; //첫 거리 계산해서 저장
            }
            else
            {
                float toCompare = Input.GetAxis("Mouse ScrollWheel");
                if (toCompare < 0)
                {
                    originDistance = originDistance - transform.forward;
                }
                else if (toCompare > 0)
                {
                    originDistance = originDistance + transform.forward;
                }


                transform.position = playerTr.position + originDistance;
            }
        }
    }
}
