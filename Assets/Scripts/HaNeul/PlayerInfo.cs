using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    //퍼블릭 스태틱을 이용하면 타 클래스에서 건드릴 수 있게된다
    public static GameObject clickTarget = null;
    
    //게임오브젝트가 들고있던 마크 캔버스 off
    //public void ObjMarkCanvasOff()
    //{
    //    clickTarget.GetComponentInChildren<Canvas>().gameObject.SetActive(false);
    //}

}
