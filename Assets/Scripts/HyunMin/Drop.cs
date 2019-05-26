using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour,IDropHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        //현재 트랜스 폼이 가진 자식의 수가 0이면
        if(transform.childCount == 0)
        {
            //드래그한 아이템 트랜스폼의 부모를 현재 트랜스폼으로 바꿔라
            Drag.draggingItem.transform.SetParent(this.transform);
        }
    }
}
