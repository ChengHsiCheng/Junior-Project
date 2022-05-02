using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Collision : MonoBehaviour
{
    Enemy TargetEnemy;
    public Player player;
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            TargetEnemy = other.GetComponent<Enemy>();
            TargetEnemy.Hp -= 10;
            TargetEnemy.state = 2;
            if(player.attackHit == 3)
            {
                TargetEnemy.Hp -= 20;
            }
        }
    }
}
