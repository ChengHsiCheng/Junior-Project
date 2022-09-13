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

            Instantiate(player.hitEffecis, other.transform.position, Quaternion.identity);

            if(player.comboStep != 3)
            {
                damege = 1;
            }
            if(player.comboStep == 3)
            {
                damege = 3;
                
            }
            TargetEnemy.BeAttacked(damege);
        }
    }
}
