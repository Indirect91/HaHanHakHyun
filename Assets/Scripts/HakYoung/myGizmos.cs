using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myGizmos : MonoBehaviour
{
    /* 아직 안쓰는것. */
    public enum Type { NORMAL, WAYPOINT}
    private const string wayPointFile = "Animal";
    public Type type = Type.NORMAL;
    /* 아직 안쓰는것. */

    //색상이랑 크기 퍼블릭으로 생성해서 유니티에서 만질 수 있음.
    public Color _color = Color.yellow;
    public float _radius = 0.1f;

    private void OnDrawGizmos()
    {
        //기즈모에 담아라.
        Gizmos.color = _color;
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
