using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public Transform petTransform;
    //펫 장착?할 슬롯
    private Transform equipPetTransform;
    //펫 리스트(인벤토리)
    private Transform petListTransform;
    private CanvasGroup itemCanvasGroup;

    public static GameObject draggingItem = null;

    // Start is called before the first frame update
    void Start()
    {
        petTransform = GetComponent<Transform>();

        //PlayerStatus란 게임오브젝트를 찾아 Transform이라는 컴포넌트를 찾아 저장
        equipPetTransform = GameObject.Find("PlayerStatus").GetComponent<Transform>();
        //PetInven이란 게임오브젝트를 찾아 Transform이라는 컴포넌트를 찾아 저장
        //petListTransform = GameObject.FindGameObjectsWithTag("UIDrag");
        petListTransform = GameObject.Find("PetInfo").GetComponent<Transform>();

         itemCanvasGroup = GetComponent<CanvasGroup>();
    }

    //드래그 이벤트
    public void OnDrag(PointerEventData eventData)
    {
        //이벤트 발생시 아이템을 마우스 커서 위치로 이동시키기
        //for (int i = 0; i < petTransform.Length; i++)
        //{
        //    petTransform[i].position = Input.mousePosition;
        //}
        petTransform.position = Input.mousePosition;
    }

    //드래그 시작하면 한번만 호출되는 이벤트
    public void OnBeginDrag(PointerEventData eventData)
    {
        //현재 게임오브젝트의 부모를 PlayerStatus로 변경
        this.transform.SetParent(equipPetTransform);
        //드래그 시작시 선택된 아이템 데이터 저장
        draggingItem = this.gameObject;

        //드래그가 시작되면 다른 UI이벤트를 받지 않도록 설정
        itemCanvasGroup.blocksRaycasts = false;
    }

    //드래그 종료시 한번만 실행
    public void OnEndDrag(PointerEventData eventData)
    {
        //드래그 종료시 null로 바꿔준다.
        draggingItem = null;
        itemCanvasGroup.blocksRaycasts = true;

        //Pet장착 슬롯에 드래그 하지 않았을 때 다시 PetList로 되돌리기
        if (petTransform.parent == equipPetTransform)
        {
             petTransform.SetParent(petListTransform.transform);
             petTransform.transform.localPosition = new Vector3(31, 10, 0);
        }
        
        
    }
}
