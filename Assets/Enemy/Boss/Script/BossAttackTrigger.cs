using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackTrigger : MonoBehaviour
{
    Player TargetPlayer;
    public Boss boss;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TargetPlayer = other.GetComponent<Player>();
            TargetPlayer.PlayerBeAttack(boss.damege);

            if (TargetPlayer.playerHp <= 0)
            {
                Destroy(gameObject);
            }



        }
    }
}
