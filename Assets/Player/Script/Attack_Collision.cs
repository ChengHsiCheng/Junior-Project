using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Collision : MonoBehaviour
{
    Enemy01 TargetEnemy;
    public Player player;
    float damege;
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            TargetEnemy = other.GetComponent<Enemy01>();
            TargetEnemy.state = 2;
            if(player.attackHit != 3)
            {
                damege = 10;
            }
            if(player.attackHit == 3)
            {
                damege = 30;
            }
            TargetEnemy.Damege(damege);
        }
    }
}
