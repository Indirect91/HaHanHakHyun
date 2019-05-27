using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetTest : MonoBehaviour
{

    GameObject player;
    float distanceToPlayer = 5.0f;
    float fastFollowFix;
    float slowFollowLerp;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        this.transform.LookAt(player.transform);
        if(Vector3.Distance(this.transform.position, player.transform.position)>distanceToPlayer)
        {
          transform.position = Vector3.Lerp(this.transform.position, player.transform.position,0.01f);
           // transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, 5.0f * Time.deltaTime);
        }
        else if (Vector3.Distance(this.transform.position, player.transform.position) > distanceToPlayer)
        { }
        else
        { }
    }
}
