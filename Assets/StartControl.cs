using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //터치와 관련된 이벤트가 들어있음

public class StartControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    // Start is called before the first frame update

    [SerializeField] public Camera toControl;

    public void OnPointerDown(PointerEventData eventData)
    {
        CameraTest.isStart = true;
        this.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {

    }
}
