using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    //Canvas를 렌더링하는 카메라
    private Camera uiCamera;
    //ui용 최상위 캔버스
    private Canvas canvas;
    //부모 RectTransfrom  컴포넌트
    private RectTransform rectParent;
    //자신 RectTransform 컴포넌트
    private RectTransform rectHp;

    //HpBar 이미지의 위치 조절할 오프셋
    [HideInInspector] public Vector3 offset = Vector3.zero;
    //추적할 대상의 Transform 컴포넌트
    [HideInInspector] public Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        //
        canvas = GetComponentInParent<Canvas>();
        //인게임은 로컬 좌표지만 ui는 월드 좌표에 존재하기 때문에 월드 카메라로 보여주기 위함?
        uiCamera = canvas.worldCamera;
        //최상위 캔버스 렉트트랜스폼 컴포넌트 추출
        rectParent = canvas.GetComponent<RectTransform>();
        rectHp = this.gameObject.GetComponent<RectTransform>();

    }

    // Update is called once per frame
    //업데이트 다음에 실행되는 LateUpdate함수 
    void LateUpdate()
    {
        //월드 좌표를 스크린 좌표로 변경한다.
        //동물들의 위치를 스크린좌표로 변경하여 동물들에게 HpBar를 달아 주기위함?
        var screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position + offset);
        //2D좌표계는 
        if(screenPosition.z < 0.0f)
        {
            screenPosition *= -1.0f;
        }

        //
        var localPosition = Vector2.zero;

        //스크린좌표를 월드 좌표로 변환
        //Canvas RectTransform을 위에서 변경시킨 스크린좌표를 받아 로컬좌표로 변경하여 localPosition에 넣어서 반환시킨다.?
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPosition, uiCamera, out localPosition);
       
        //반환 받은 
        rectHp.localPosition = localPosition;
    }
}
