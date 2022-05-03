using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_Collision : MonoBehaviour
{
    Player TargetPlayer;
    float damege = 10;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            TargetPlayer = other.GetComponent<Player>();
            TargetPlayer.Damege(damege);
        }
    }
}
