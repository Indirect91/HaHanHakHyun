using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    float MaxHp;
    float currHp;



    // Start is called before the first frame update
    void Start()
    {
        MaxHp = PlayerInfo.playerHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
