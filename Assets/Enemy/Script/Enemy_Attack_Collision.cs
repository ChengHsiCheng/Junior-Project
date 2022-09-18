using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_Collision : MonoBehaviour
{
    Player TargetPlayer;
    public EnemyStatusInfo enemy;
    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log(other.name);
        if(other.tag == "Player")
        {
            TargetPlayer = other.GetComponent<Player>();
            TargetPlayer.PlayerBeAttack(enemy.damege);
            
        }
    }
}
