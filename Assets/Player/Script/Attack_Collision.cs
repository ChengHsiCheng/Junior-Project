using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Collision : MonoBehaviour
{
    public EnemyStatusInfo TargetEnemy;
    public Player player;
    float damege;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            TargetEnemy = other.GetComponent<EnemyStatusInfo>();
            if(player.attackHit != 3)
            {
                damege = 10;
            }
            if(player.attackHit == 3)
            {
                damege = 30;
            }
            TargetEnemy.Damege(damege,true);
        }
    }
}
