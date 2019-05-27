using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAround : MonoBehaviour
{
    public void OnLeft(bool isLeft)
    {
        //체크 = 왼쪽 / 언체크 = 오른쪽
        isLeft = !isLeft;
    }
}
